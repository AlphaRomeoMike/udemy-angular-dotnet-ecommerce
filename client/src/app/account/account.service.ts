import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { AbstractControl, AsyncValidatorFn } from '@angular/forms'
import { Router } from '@angular/router'
import { BehaviorSubject, map, Observable, of, ReplaySubject } from 'rxjs'
import { environment } from 'src/environments/environment'
import { IAddress, IUser } from '../shared/interfaces/iuser'

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  api = environment.url
  private currentUserSource = new ReplaySubject<IUser | null>(1)
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient, private router: Router) {}

  /**
   * # Login
   * ---
   * @description Logs in the user, sets the user source to the current user
   * @param {any} input
   * @returns Observable<IUser>
   */
  login(input: any) {
    return this.http.post<IUser>(this.api + 'account/login', input).pipe(
      map((user) => {
        localStorage.setItem('token', user.token)
        this.currentUserSource.next(user)
        return user
      }),
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
    return this.http.post<IUser>(this.api + 'account/register', values).pipe(
      map((user) => {
        localStorage.setItem('token', user.token)
        this.currentUserSource.next(user)
        return user
      }),
    )
  }

  /**
   * # Logout
   */
  logout() {
    localStorage.removeItem('token')
    this.currentUserSource.next(null)
    this.router.navigateByUrl('/')
  }

  /**
   * # Check Email Exists
   * ---
   * @description Checks if user email exists
   * @param {string} input
   * @return {Observable<boolean>}
   */
  checkEmailExists(input: string) {
    return this.http.get<boolean>(
      this.api + 'account/emailexists?email=' + input,
    )
  }

  loadCurrentUser(token: string | null) {
    if (token == null) {
      this.currentUserSource.next(null)
      return of(null)
    }
    let headers = new HttpHeaders()
    headers = headers.set('Authorization', `Bearer ${token}`)

    return this.http
      .get<IUser>(`${this.api}account/CurrentUser`, { headers })
      .pipe(
        map((user) => {
          if (user) {
            localStorage.setItem('token', user.token)
            this.currentUserSource.next(user)
            return user
          } else {
            return null
          }
        }),
      )
  }

  getUserAddress() {
    return this.http.get<IAddress>(this.api + 'account/address')
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.api + 'account/address', address)
  }
}
