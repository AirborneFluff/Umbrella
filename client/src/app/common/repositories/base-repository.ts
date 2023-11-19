import { HttpClient } from '@angular/common/http';
import { CONFIG } from '../../app.config';

export abstract class BaseRepository {
  readonly baseUrl = CONFIG.apiUrl;
  constructor(protected http: HttpClient) {}
}
