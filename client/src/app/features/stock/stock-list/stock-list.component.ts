import { Component, OnDestroy, OnInit } from '@angular/core';
import { StockService } from '../services/stock.service';
import { map, Subject, Subscription, switchMap, take } from 'rxjs';
import { PaginationParams } from '../../../core/utilities/pagination';
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

    this.searchParams$.next({
      pageNumber: 1,
      pageSize: 50
    })
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private searchParams$ = new Subject<PaginationParams>();

  private pageStream$ = this.searchParams$.pipe(switchMap(params => this.stockApi.getPaginatedList(params)));
  private dataStream$ = this.pageStream$.pipe(map(result => result.result));
  pagination$ = this.pageStream$.pipe(map(result => result.pagination));

  loadNextPage() {
    let params: PaginationParams;
    this.searchParams$.pipe(take(1)).subscribe(search => params = search);

    this.searchParams$.next({
      pageSize: params!.pageSize,
      pageNumber: params!.pageNumber
    })
  }
}
