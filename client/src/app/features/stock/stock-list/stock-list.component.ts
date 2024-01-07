import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { StockService } from '../services/stock.service';
import { BehaviorSubject, map, shareReplay, Subscription, switchMap, take, tap } from 'rxjs';
import { Pagination } from '../../../core/utilities/pagination';
import { StockItem } from '../../../core/models/stock-item';
import { BreakpointStream } from '../../../core/streams/breakpoint-stream';
import { Breakpoints } from '../../../core/definitions/breakpoints';
import { PagedSearchParams } from '../../../core/params/paged-search-params';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { QueryFilterComponent } from '../../../shared/query-filter/query-filter/query-filter.component';
import { OrbSearchComponent } from '../../../shared/orb-search/orb-search.component';
import { FilterService } from '../../../shared/query-filter/services/filter.service';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss']
})
export class StockListComponent implements OnInit, OnDestroy {
  @ViewChild('searchBar') searchBar!: OrbSearchComponent;
  stockItems: StockItem[] = [];
  subscriptions = new Subscription();
  pageSize = 50;

  constructor(private stockApi: StockService, private breakpoint$: BreakpointStream, private bottomSheet: MatBottomSheet, private filter: FilterService) {
  }

  ngOnInit(): void {
    this.subscriptions.add(this.dataStream$.subscribe(data => {
      this.stockItems.push(...data);
    }))
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  showCompactFilters$ = this.breakpoint$.pipe(
    map(state => {
      return !state.breakpoints[Breakpoints.xl];
    })
  )

  private searchParams$ = new BehaviorSubject<PagedSearchParams>({
    pageNumber: 1,
    pageSize: this.pageSize
  });

  private pageStream$ = this.searchParams$.pipe(switchMap(params => this.stockApi.getPaginatedList(params)), shareReplay(1));
  private dataStream$ = this.pageStream$.pipe(map(result => result.result), shareReplay(1));
  private pagination$ = this.pageStream$.pipe(map(result => result.pagination), shareReplay(1));

  loadNextPage() {
    let pagination: Pagination;
    this.pagination$.pipe(take(1)).subscribe(page => pagination = page);

    if (pagination!.totalPages == pagination!.currentPage) return;

    let params: PagedSearchParams;
    this.searchParams$.pipe(take(1)).subscribe(search => params = search);

    params!.pageNumber = pagination!.currentPage + 1;
    this.searchParams$.next(params!);
  }

  updateSearch(searchTerm: string) {
    this.searchParams$.next({
      searchTerm: searchTerm, pageNumber: 1, pageSize: this.pageSize
    });

    this.searchParams$.pipe(
      take(1),
      tap(() => this.clearList())
    ).subscribe();
  }

  private clearList() {
    this.stockItems = [];
  }

  openFilters() {
    this.bottomSheet.open(QueryFilterComponent, {
      panelClass: "h-4/5",
      data: 'stockItem'
    });
    this.searchBar.collapse();
  }
}
