import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AdminService } from 'src/app/admin/admin.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(
    private adminService: AdminService,
    private router: Router
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.adminService.currentAdmin$.pipe(
      map(auth => {
        if (auth) return true;
        else {
          this.router.navigate(['/admin/login'], { queryParams: { returnUrl: state.url } });
          return false;
        }
      })
    )
  }

}
