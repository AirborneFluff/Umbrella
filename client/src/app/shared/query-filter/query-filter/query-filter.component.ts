import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { QueryFilter } from "../query-filter";
import { animate, keyframes, style, transition, trigger } from '@angular/animations';
import { map, Observable, pairwise, startWith } from 'rxjs';
import { FilterDefinition } from '../filter-definition';
import { FilterService } from '../services/filter.service';
import { QueryOption, QueryParameter } from '../filter-option';

const ANIMATION_DURATION = 200;

@Component({
  selector: 'app-query-filter',
  templateUrl: './query-filter.component.html',
  styleUrls: ['./query-filter.component.scss'],
  animations: [
    trigger('fadeOut', [
      transition('none=>down', [
        animate(ANIMATION_DURATION, keyframes([
          style({ opacity: 1, display: "block" }),
          style({ opacity: 0, transform: "translateX(-15%)" }),
          style({ opacity: 0, transform: "translateX(-30%)" })
        ])),
      ]),
      transition('none=>up', [
        animate(ANIMATION_DURATION, keyframes([
          style({ opacity: 1, display: "block" }),
          style({ opacity: 0, transform: "translateX(15%)" }),
          style({ opacity: 0, transform: "translateX(30%)" })
        ])),
      ]),
    ]),
    trigger('fadeIn', [
      transition('none=>down', [
        animate(ANIMATION_DURATION, keyframes([
          style({ opacity: 0, transform: "translateX(60%)"}),
          style({ opacity: 1, transform: "translateX(0)" })
        ])),
      ]),
      transition('none=>up', [
        animate(ANIMATION_DURATION, keyframes([
          style({ opacity: 0, transform: "translateX(-60%)"}),
          style({ opacity: 1, transform: "translateX(0)" })
        ])),
      ])
    ]),
  ],
})
export class QueryFilterComponent implements OnInit {
  options: QueryParameter[] = [];
  @Input() entityName!: FilterDefinition;
  @Input() compact = true;
  @Output() params: EventEmitter<HttpParams> = new EventEmitter<HttpParams>();
  @Output() onClose = new EventEmitter();

  animationDirection: 'down' | 'up' | 'none' = 'none'

  filter!: QueryFilter;
  previousOptions$!: Observable<QueryOption[]>;

  constructor(private filterService: FilterService) {}

  ngOnInit() {
    this.filterService.getFilterInstance(this.entityName)
      .subscribe(filter => this.setFilter(filter));
  }

  private setFilter(filter: QueryFilter) {
    this.filter = filter
    this.previousOptions$ = this.filter.navigationOptions$.pipe(
      startWith([]),
      pairwise(),
      map(([previous, _]) => previous)
    )
  }

  emitValue() {
    this.params.emit(this.filter.httpParameters)
  }

  clearFilters() {
    this.filter.clearFilters();
    this.params.emit(new HttpParams());
  }

  handleOptionClick(option: QueryOption | QueryParameter) {
    if ('options' in option) {
      this.pageForward(option)
      return;
    }

    this.toggleParamActive(option)
  }

  isOptionEnabled(option: QueryOption) {
    if (!this.filter.currentParameter) return false;
    return this.filter.isOptionEnabled(this.filter.currentParameter, option);
  }

  private animate(direction: 'down' | 'up') {
    this.animationDirection = direction;
    setTimeout(() => this.animationDirection = 'none', ANIMATION_DURATION);
  }

  pageBack() {
    this.filter.navigateUp();
    this.animate('up');
  }

  private pageForward(option: QueryParameter) {
    this.filter.navigateDown(option);
    this.animate('down');
  }

  private toggleParamActive(option: QueryOption) {
    const parameter = this.filter.currentParameter;
    this.filter.toggleOption(parameter, option);
  }
}
