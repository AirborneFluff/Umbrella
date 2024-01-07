import { PaginationParams } from '../utilities/pagination';

export interface PagedSearchParams extends PaginationParams {
  searchTerm?: string
}
