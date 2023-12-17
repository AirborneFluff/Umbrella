import { FilterOption } from './filter-option';

export class FilterBuilder {
  private readonly options: FilterOption[] = [];

  public constructor(configJson?: string, config?: FilterOption[]) {
    if (configJson) {
      this.options = JSON.parse(configJson) as FilterOption[];
      return;
    }

    if (config) {
      this.options = config;
      return;
    }
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
