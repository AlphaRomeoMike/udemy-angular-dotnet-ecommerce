import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/interfaces/iuser';
import { ReplaySubject, map } from 'rxjs';
import { IAdmin, ILogin } from '../shared/interfaces/iadmin';
import { IGenericResponse } from '../shared/interfaces/igenericresponse';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  api = environment.admin_url;
  private currentAdminSource = new ReplaySubject<IAdmin | null>(1);
  currentAdmin$ = this.currentAdminSource.asObservable();


  constructor(private http: HttpClient, private router: Router) { }

  login(login: ILogin) {    
    return this.http.post<IGenericResponse<IAdmin>>(this.api + 'admin/login', login).pipe(
      map((admin) => {
        console.log(admin);
        localStorage.setItem('token', admin.data.token);
        this.currentAdminSource.next(admin.data);
        this,this.router.navigateByUrl('admin/home')
        return admin;
      })
    )
  }
}
