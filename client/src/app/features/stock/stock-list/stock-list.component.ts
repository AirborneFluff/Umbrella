import { Component, OnInit } from '@angular/core';
import { StockService } from '../services/stock.service';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss']
})
export class StockListComponent implements OnInit {
  constructor(private stockApi: StockService) {
  }

  ngOnInit(): void {
    this.stockApi.getPaginatedList({
      pageNumber: 1,
      pageSize: 20
    }).subscribe(val => console.log(val))
  }
}
