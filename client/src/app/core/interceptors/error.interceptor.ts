import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: HotToastService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next
      .handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error) {
            if (error.status === 400) {
              if (error.error.errors) {
                throw error.error;
              }
              this.toastr.error(`${ error.error.message } - ${ error.status }`, { theme: 'snackbar', position: 'top-right' });
            }
            if (error.status === 401) {
              this.toastr.error('Unauthorized', { theme: 'snackbar', position: 'top-right' })
            }
            if (error.status === 404) {
              this.router.navigateByUrl('/not-found');
              this.toastr.error('Resource not found', { theme: 'snackbar', position: 'top-right' })
            }
            if (error.status === 500) {
              const extras: NavigationExtras = {
                state: {
                  error: error.error
                }
              }
              this.router.navigateByUrl('/server-error', extras);
              this.toastr.error('Something went wrong', { theme: 'snackbar', position: 'top-right' })
            }
          }
          return throwError(() => new Error(error.message));
        })
      );
  }
}
