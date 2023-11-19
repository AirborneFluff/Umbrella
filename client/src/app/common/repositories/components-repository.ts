import { BaseRepository } from './base-repository';
import { Observable } from 'rxjs';
import { ProductComponent } from '../data-models';

export class ComponentsRepository extends BaseRepository {

  public getById(id: number) : Observable<ProductComponent> {
    return this.http.get<ProductComponent>(this.baseUrl + `Components/${id}`)
  }
}
