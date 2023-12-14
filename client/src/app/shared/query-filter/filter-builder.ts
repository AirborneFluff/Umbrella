import { FilterOption } from './filter-option';

export class FilterBuilder {
  private options: FilterOption[] = [];

  public constructor() {
  }

  public build(): FilterOption[] {
    this.options.sort((a, b) => !!b.children ? 1 : -1)
    return this.options;
  }

  public addOption(option: FilterOption) {
    this.options.push(option);
  }

  public addParameter(title: string, param: string) {
    this.options.push({
      children: undefined,
      title: title,
      parameter: {
        param,
        active: false
      }
    })
  }
}
