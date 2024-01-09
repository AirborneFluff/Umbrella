export interface FilterOption {
  title: string,
  children: FilterOption[],
  parameter: FilterParam | undefined
}

export interface FilterParam {
  param: string,
  active: boolean
}
