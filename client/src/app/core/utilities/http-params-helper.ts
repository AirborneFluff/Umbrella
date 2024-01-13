import { HttpParams } from '@angular/common/http';


export function mergeParams(params1: HttpParams, params2: HttpParams) {
  const keys = params2.keys();
  const params = keys.flatMap(key => params2.getAll(key)!.map(value => [key, value]));
  params.forEach(([param, option]) => {
    params1 = params1.append(param, option)
  });

  return params1;
}
