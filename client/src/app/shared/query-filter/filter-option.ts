export interface FilterOption {
  title: string,
  children: FilterOption[] | undefined,
  parameter: FilterParam | undefined
}

export interface FilterParam {
  param: string,
  active: boolean
}
