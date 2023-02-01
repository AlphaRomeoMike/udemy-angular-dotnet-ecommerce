import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr/public_api';
import { HotToastService } from '@ngneat/hot-toast/public-api';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: HotToastService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).
      pipe(
        catchError((error: HttpErrorResponse) => {
          if (error) {
            if (error.status == 404) {
              this.router.navigateByUrl('/not-found');
              this.toastr.error('Resource not found', { theme: 'snackbar', position: 'top-right' })
            }
            if (error.status === 500) {
              this.router.navigateByUrl('/server-error');
            }
          }
          return throwError(() => new Error(error.message));
        })
      );
  }
}
