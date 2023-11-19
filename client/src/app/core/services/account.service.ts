import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CONFIG } from '../../app.config';
import { LoginParams } from '../../common/data-models';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  readonly baseUrl = CONFIG.apiUrl;

  constructor(private http: HttpClient) { }

  public login(login: LoginParams) {
    return this.http.post(this.baseUrl + "Account/login", login);
  }
  public logout() {
    return this.http.post(this.baseUrl + "Account/logout", {});
  }
}
