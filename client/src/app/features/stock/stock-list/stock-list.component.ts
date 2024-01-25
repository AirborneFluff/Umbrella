import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { StockService } from '../../../core/services/stock.service';
import {
  debounceTime,
  map,
  Observable,
  shareReplay,
  Subject,
  Subscription,
  switchMap,
  take,
  tap
} from 'rxjs';
import { Pagination, PaginationParams } from '../../../core/utilities/pagination';
import { StockItem } from '../../../core/models/stock-item';
import { BreakpointStream } from '../../../core/streams/breakpoint-stream';
import { Breakpoints } from '../../../core/definitions/breakpoints';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { OrbSearchComponent } from '../../../shared/orb-search/orb-search.component';
import {
  QueryFilterSheetComponent
} from '../../../shared/query-filter/query-filter-sheet/query-filter-sheet.component';
import { HttpParams } from '@angular/common/http';
import { FilterService } from '../../../shared/query-filter/services/filter.service';

const PAGE_SIZE = 50;

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss']
})
export class StockListComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild(OrbSearchComponent) searchBar!: OrbSearchComponent;
  stockItems: StockItem[] | undefined;
  subscriptions = new Subscription();
  filters: HttpParams = new HttpParams();
  paginationParams: PaginationParams = {
    pageNumber: 1,
    pageSize: PAGE_SIZE
  };

  searchUpdates$!: Observable<string>;

  constructor(private stockApi: StockService,
              private breakpoint$: BreakpointStream,
              private bottomSheet: MatBottomSheet,
              private queryFilter: FilterService) {
  }

  ngOnInit(): void {
    this.subscriptions.add(this.dataStream$.subscribe(data => {
      if (!this.stockItems) this.stockItems = [];
      this.stockItems.push(...data);
    }))
    this.triggerApi$.next(undefined);
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  ngAfterViewInit() {
    this.searchUpdates$ = this.searchBar.onSearch.pipe(
      debounceTime(500));

    this.subscriptions.add(
      this.searchUpdates$.subscribe(val => this.updateSearch(val)));
  }

  showCompactFilters$ = this.breakpoint$.pipe(
    map(state => {
      return !state.breakpoints[Breakpoints.xl];
    })
  )

  private triggerApi$ = new Subject();
  private pageStream$ = this.triggerApi$.pipe(
    switchMap(() => this.stockApi.getPaginatedList(this.paginationParams, this.filters)),
    tap(result => {
      if (result.pagination.currentPage == 1) this.clearList();
    }),
    shareReplay(1));

  private dataStream$ = this.pageStream$.pipe(map(result => result.result), shareReplay(1));
  private pagination$ = this.pageStream$.pipe(map(result => result.pagination), shareReplay(1));

  lastPage$ = this.pagination$.pipe(map(page => page.totalPages == page.currentPage), shareReplay(1));

  loadNextPage() {
    let pagination: Pagination;
    this.pagination$.pipe(take(1)).subscribe(page => pagination = page);

    if (pagination!.totalPages == this.paginationParams.pageNumber) return;

    this.paginationParams.pageNumber += 1;
    this.triggerApi$.next(undefined);
  }

  updateSearch(searchTerm: string) {
    this.filters = new HttpParams().set('searchTerm', searchTerm);
    this.queryFilter.clearFilter('stockItem').subscribe();
    this.triggerApi$.next(undefined);
    this.clearList();
  }

  private clearList() {
    this.stockItems = undefined;
  }

  openFilters() {
    this.searchBar.collapse();
    this.bottomSheet.open(QueryFilterSheetComponent, {
      panelClass: "h-4/5",
      data: 'stockItem'
    }).afterDismissed().subscribe(val => this.applyFilter(val));
  }

  applyFilter(event: HttpParams) {
    this.filters = event;
    this.paginationParams.pageNumber = 1;
    this.triggerApi$.next(undefined);
    this.clearList();
  }
}
