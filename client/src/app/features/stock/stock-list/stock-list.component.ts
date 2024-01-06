import { Component, OnDestroy, OnInit } from '@angular/core';
import { StockService } from '../services/stock.service';
import { BehaviorSubject, map, shareReplay, Subscription, switchMap, take } from 'rxjs';
import { Pagination, PaginationParams } from '../../../core/utilities/pagination';
import { StockItem } from '../../../core/models/stock-item';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss']
})
export class StockListComponent implements OnInit, OnDestroy {
  stockItems: StockItem[] = [];
  subscriptions = new Subscription();

  constructor(private stockApi: StockService) {
  }

  ngOnInit(): void {
    this.subscriptions.add(this.dataStream$.subscribe(data => {
      this.stockItems.push(...data);
    }))
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private searchParams$ = new BehaviorSubject<PaginationParams>({
    pageNumber: 1,
    pageSize: 50
  });

  private pageStream$ = this.searchParams$.pipe(switchMap(params => this.stockApi.getPaginatedList(params)), shareReplay(1));
  private dataStream$ = this.pageStream$.pipe(map(result => result.result), shareReplay(1));
  pagination$ = this.pageStream$.pipe(map(result => result.pagination), shareReplay(1));

  loadNextPage() {
    let params: PaginationParams;
    let pagination: Pagination;
    this.pagination$.pipe(take(1)).subscribe(page => pagination = page);
    this.searchParams$.pipe(take(1)).subscribe(search => params = search);

    params!.pageNumber = pagination!.currentPage + 1;
    this.searchParams$.next(params!);
  }
}
