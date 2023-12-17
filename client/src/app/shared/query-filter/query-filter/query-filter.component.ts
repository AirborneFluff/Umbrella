import { Component, EventEmitter, Output } from '@angular/core';
import { FilterOption } from '../filter-option';
import { FilterBuilder } from '../filter-builder';

import jsonConfig from '../example.query-filter.json'
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-query-filter',
  templateUrl: './query-filter.component.html',
  styleUrls: ['./query-filter.component.scss']
})
export class QueryFilterComponent {
  @Output() params: EventEmitter<HttpParams> = new EventEmitter<HttpParams>();
  rootOptions: FilterOption[] = [];
  navigationPath: number[] = [];
  currentOptions: FilterOption[] = [];

  get navigationDepth(): number {
    return this.navigationPath.length
  }

  constructor() {
    let builder = new FilterBuilder(JSON.stringify(jsonConfig));

    this.rootOptions = builder.build();
    this.currentOptions = this.rootOptions;
  }

  handleOptionClick(option: FilterOption) {
    if (option.children) {
      this.navigateDown(option);
      return;
    }

    this.toggleParamActive(option);
  }

  private navigateDown(option: FilterOption) {
    if (!option.children) return;

    const index = this.currentOptions.indexOf(option);
    if (index != -1) {
      this.navigationPath.push(index);
      this.currentOptions = this.getOptionsByPath(this.rootOptions, this.navigationPath)
    }
  }

  private toggleParamActive(option: FilterOption) {
    if (!option.parameter) return;
    option.parameter.active = !option.parameter.active;
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

  navigateUp() {
    this.navigationPath.pop();
    this.currentOptions = this.getOptionsByPath(this.rootOptions, this.navigationPath)
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
