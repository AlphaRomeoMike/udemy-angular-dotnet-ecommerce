import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/interfaces/iuser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  api = environment.url
  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient,
    private router: Router) { }

  /**
   * # Login
   * ---
   * @description Logs in the user, sets the user source to the current user
   * @param {any} input
   * @returns Observable<IUser>
   */
  login(input: any) {
    return this.http.post<IUser>(this.api + 'account/login', input)
      .pipe(
        map(user => {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        })
      )
  }

  /**
   * # Register
   * ---
   * @description Registers, then logs the user in
   * @param {any} values
   * @returns Observable<IUser>
   */
  register(values: any) {
    return this.http.post<IUser>(this.api + 'account/register', values)
      .pipe(
        map(user => {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        })
      )
  }

  /**
   * # Logout
   */
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(input: string) {
    this.http.get<boolean>(this.api + 'account/emailexists?email=' + input);
  }

  loadCurrentUser(token: string) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IUser>(`${this.api}account/CurrentUser`, { headers })
      .pipe(
        map(
          user => {
            localStorage.setItem('token', user.token);
            this.currentUserSource.next(user);
          }
        ))
  }
}
