import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { RegisterOrganisationParams } from '../models/register-organisation-params';

@Injectable({
  providedIn: 'root'
})
export class OrganisationService {
  private baseUrl = environment.apiUrl + 'organisations';

  constructor(private http: HttpClient) { }

  public createOrganisation(params: RegisterOrganisationParams) {
    return this.http.post(this.baseUrl, params);
  }
}
