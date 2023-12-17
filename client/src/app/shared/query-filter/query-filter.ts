import { FilterOption } from "./filter-option";
import {
  BehaviorSubject,
  map,
  shareReplay, startWith,
  take,
} from "rxjs";
import {HttpParams} from "@angular/common/http";

export class QueryFilter {
  private readonly rootOptions: FilterOption[] = [];
  private _navigationPath: number[] = [];

  navigationPath$ = new BehaviorSubject<number[]>(this._navigationPath);
  navigationOptions$ = this.navigationPath$.pipe(
    map(path => {
      return this.getOptionsByPath(this.rootOptions, path);
    }),
    startWith(this.rootOptions),
    shareReplay(1)
  )
  navigationDepth$ = this.navigationPath$.pipe(
    map(path => path.length),
    startWith(0)
  )

  constructor(options: FilterOption[]) {
    this.rootOptions = options;
  }

  buildHttpParams(): HttpParams {
    let params: HttpParams = new HttpParams();

    const activeOptions = this.getActiveOptions({
      parameter: undefined, title: '',
      children: this.rootOptions
    });

    for (const key in activeOptions) {
      const parameter = activeOptions[key].parameter
      if (!parameter) continue;

      params = params.append(parameter.param, true);
    }

    return params;
  }

  navigateUp() {
    this._navigationPath.pop();
    this.navigationPath$.next(this._navigationPath);
  }

  navigateDown(option: FilterOption) {
    if (!option.children) return;

    let currentOptions;
    this.navigationOptions$.pipe(take(1)).subscribe(val => currentOptions = val);

    const index = currentOptions!.indexOf(option);
    if (index == -1) return;

    this._navigationPath.push(index);
    this.navigationPath$.next(this._navigationPath);
  }

  private getOptionsByPath(obj: FilterOption[], path: number[]): FilterOption[] {
    if (!path.length) {
      return obj;
    }
    const pathClone = Object.create(path);
    const index = pathClone.shift();
    if (index == undefined) return obj;

    const children = obj[index].children;
    if (!!children) {
      return this.getOptionsByPath(children, pathClone);
    }
    return obj;
  }

  getActiveOptions(option: FilterOption): FilterOption[] {
    if (!option.children) return [];

    let params = option.children.flatMap(x => {
      if (x.children) return this.getActiveOptions(x);
      return x;
    })

    params = params.filter(x => (x.parameter!.active));

    return params;
  }
}
