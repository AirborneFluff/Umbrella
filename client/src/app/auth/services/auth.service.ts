import { Injectable } from '@angular/core';
import { CONFIG } from '../../app.config';
import { HttpClient } from '@angular/common/http';
import { LoginParams } from '../models/login-params';
import { UserClaim } from '../models/user-claim';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = CONFIG.apiUrl;

  constructor(private http: HttpClient) { }

  public login(login: LoginParams) {
    //TODO We need some response from the server. IdentityUserDto perhaps
    return this.http.post(this.baseUrl + "Account/login", login);
  }

  public logout() {
    return this.http.post(this.baseUrl + "Account/logout", {});
  }

  public getUser() {
    //TODO This should be IdentityUser response object containing claims
    return this.http.get<UserClaim[]>(this.baseUrl + "Account", {});
  }

  public isSignedIn() {
    return this.getUser().pipe(
      map(claims => {
        return claims.length > 0
      })
    )
  }
}
