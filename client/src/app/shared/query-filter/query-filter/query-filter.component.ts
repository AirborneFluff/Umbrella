import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FilterOption, FilterParam } from '../filter-option';
import { FilterBuilder } from '../filter-builder';

@Component({
  selector: 'app-query-filter',
  templateUrl: './query-filter.component.html',
  styleUrls: ['./query-filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class QueryFilterComponent {
  rootOptions: FilterOption[] = [];
  navigationPath: number[] = [];
  currentOptions: FilterOption[] = [];

  get navigationDepth(): number {
    return this.navigationPath.length
  }

  constructor() {
    let builder = new FilterBuilder();
    builder.addParameter("Below Buffer", "lowBuffer");
    let options1: FilterOption[] = [
      { title: 'Below Buffer1', parameter: { param: 'lowBuffer', active: false}, children: undefined},
      { title: 'Low Stock1', parameter: { param: 'lowStock', active: false}, children: undefined}
    ]
    let options2: FilterOption[] = [
      { title: 'Below Buffer2', parameter: { param: 'lowBuffer', active: false}, children: undefined},
      { title: 'Low Stock2', parameter: { param: 'lowStock', active: false}, children: undefined}
    ]
    let options3: FilterOption[] = [
      { title: 'Below Buffer3', parameter: { param: 'lowBuffer', active: false}, children: undefined},
      { title: 'Low Stock3', parameter: { param: 'lowStock', active: false}, children: undefined}
    ]
    let group1: FilterOption = {
      children: options1,
      parameter: undefined,
      title: 'Category 1'
    }
    let group2: FilterOption = {
      children: options2,
      parameter: undefined,
      title: 'Category 2'
    }
    let group3: FilterOption = {
      children: options3,
      parameter: undefined,
      title: 'Category 3'
    }
    let parentGroup: FilterOption = {
      children: [group1, group2, group3],
      parameter: undefined,
      title: 'Lots of Categories'
    }
    builder.addOption(parentGroup);

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
