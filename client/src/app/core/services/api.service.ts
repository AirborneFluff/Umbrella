import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ComponentsRepository } from '../../common/repositories/components-repository';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public components: ComponentsRepository = new ComponentsRepository(this.http);

  constructor(private http: HttpClient) { }
}
