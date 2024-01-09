export interface QueryParameter extends QueryOption {
  allowMultiple: boolean,
  options: QueryOption[]
}

export interface QueryOption {
  displayValue: string,
  value: string
}
