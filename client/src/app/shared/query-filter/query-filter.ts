import {
  BehaviorSubject,
  map, Observable,
  shareReplay, startWith
} from 'rxjs';
import { HttpParams } from "@angular/common/http";
import { QueryOption, QueryParameter } from './filter-option';

export class QueryFilter {
  private readonly rootOptions: QueryParameter[] = [];
  private parameterIndex: number = -1;
  private activeOptions: [QueryParameter, QueryOption][] = [];

  navigationPath$ = new BehaviorSubject<number>(this.parameterIndex);
  navigationOptions$: Observable<QueryOption[]> = this.navigationPath$.pipe(
    map(index => {
      if (index == -1) return this.rootOptions;
      return this.rootOptions[index].options
    }),
    startWith(this.rootOptions),
    shareReplay(1)
  )

  constructor(options: QueryParameter[]) {
    this.rootOptions = options;
  }

  get httpParameters(): HttpParams {
    return this.activeOptions.reduce(
      (acc, [param, option]) => acc.append(param.value, option.value),
      new HttpParams()
    );
  }

  get currentParameter(): QueryParameter {
    return this.rootOptions[this.parameterIndex]
  }

  navigateUp() {
    this.parameterIndex = -1;
    this.navigationPath$.next(this.parameterIndex);
  }

  navigateDown(option: QueryParameter) {
    this.parameterIndex = this.rootOptions.indexOf(option);
    this.navigationPath$.next(this.parameterIndex);
  }

  getActiveOptions(parameter: QueryParameter): QueryOption[] {
    return this.activeOptions
      .filter(item => item[0] == parameter)
      .map(item => item[1]);
  }

  isOptionEnabled(parameter: QueryParameter, option: QueryOption) {
    if (parameter.options.indexOf(option) == -1) return null;
    return this.activeOptions
      .findIndex(item => item[0].value == parameter.value && item[1].value == option.value) >= 0;
  }

  toggleOption(parameter: QueryParameter, option: QueryOption) {
    const enabled = this.isOptionEnabled(parameter, option);
    if (enabled === null) return;

    if (enabled) return this.removeParameterValue(parameter, option);
    this.setParameterValue(parameter, option);
  }

  removeParameterValue(parameter: QueryParameter, option: QueryOption) {
    if (parameter.options.indexOf(option) == -1) return;

    const index = this.activeOptions.findIndex(item => item[0].value == parameter.value);
    if (index >= 0) this.activeOptions.splice(index, 1);
  }

  setParameterValue(parameter: QueryParameter, option: QueryOption) {
    if (parameter.options.indexOf(option) == -1) return;

    if (!parameter.allowMultiple) {
      const index = this.activeOptions.findIndex(item => item[0].value == parameter.value);
      if (index >= 0) this.activeOptions.splice(index, 1);
    }

    this.activeOptions.push([parameter, option]);
  }
}
