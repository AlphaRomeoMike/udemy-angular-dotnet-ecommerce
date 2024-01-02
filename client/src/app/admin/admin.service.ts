import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/interfaces/iuser';
import { ReplaySubject, map } from 'rxjs';
import { IAdmin } from '../shared/interfaces/iadmin';
import { ILogin } from '../shared/interfaces/iaccount';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  api = environment.admin_url;
  private currentAdminSource = new ReplaySubject<IAdmin | null>(1);
  currentAdmin$ = this.currentAdminSource.asObservable();


  constructor(private http: HttpClient, private router: Router) { }

  login(login: ILogin) {
    return this.http.post<IAdmin>(this.api + 'admin/login', login).pipe(
      map((admin) => {
        localStorage.setItem('token', admin.token);
        this.currentAdminSource.next(admin);
        return admin;
      })
    )
  }
}
