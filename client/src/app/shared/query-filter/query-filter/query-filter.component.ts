import { Component, EventEmitter, Output } from '@angular/core';
import { FilterOption } from '../filter-option';

import jsonConfig from '../example.query-filter.json'
import { HttpParams } from '@angular/common/http';
import { QueryFilter } from "../query-filter";

@Component({
  selector: 'app-query-filter',
  templateUrl: './query-filter.component.html',
  styleUrls: ['./query-filter.component.scss']
})
export class QueryFilterComponent {
  @Output() params: EventEmitter<HttpParams> = new EventEmitter<HttpParams>();
  filter!: QueryFilter;

  constructor() {
    this.filter = new QueryFilter(JSON.stringify(jsonConfig));
  }

  handleOptionClick(option: FilterOption) {
    if (option.children) {
      this.filter.navigateDown(option);
      return;
    }

    this.toggleParamActive(option);
  }

  private toggleParamActive(option: FilterOption) {
    if (!option.parameter) return;
    option.parameter.active = !option.parameter.active;
  }
}
