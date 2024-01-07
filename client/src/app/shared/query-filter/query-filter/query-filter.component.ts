import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FilterOption } from '../filter-option';
import { HttpParams } from '@angular/common/http';
import { QueryFilter } from "../query-filter";
import { animate, keyframes, style, transition, trigger } from '@angular/animations';
import { map, Observable, pairwise, startWith, take } from 'rxjs';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { FilterDefinition } from '../filter-definition';
import { FilterService } from '../services/filter.service';

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
  @Input() options: FilterOption[] = [];
  @Output() params: EventEmitter<HttpParams> = new EventEmitter<HttpParams>();
  @Output() onClose = new EventEmitter();

  filter!: QueryFilter;
  animationDirection: 'down' | 'up' | 'none' = 'none'
  previousOptions$!: Observable<FilterOption[]>;

  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) public entityName: FilterDefinition, private bottomSheetRef: MatBottomSheetRef, filterService: FilterService) {
    if (!entityName) return;

    filterService.getFilter(this.entityName).pipe(take(1)).subscribe(options => {
      this.options = options;
      this.filter = new QueryFilter(this.options);
    })
  }

  ngOnInit() {
    this.filter = new QueryFilter(this.options);
    this.previousOptions$ = this.filter.navigationOptions$.pipe(
      startWith([]),
      pairwise(),
      map(([previous, _]) => previous)
    )
  }

  emitValue() {
    let value = this.filter.buildHttpParams();
    this.onClose.emit(value)
    this.bottomSheetRef.dismiss(value);
  }

  close() {
    this.bottomSheetRef.dismiss();
  }

  handleOptionClick(option: FilterOption) {
    if (option.children.length > 0) {
      this.pageForward(option);
      return;
    }

    this.toggleParamActive(option);
  }

  private animate(direction: 'down' | 'up') {
    this.animationDirection = direction;
    setTimeout(() => this.animationDirection = 'none', ANIMATION_DURATION);
  }

  pageBack() {
    this.filter.navigateUp();
    this.animate('up');
  }

  private pageForward(option: FilterOption) {
    this.filter.navigateDown(option);
    this.animate('down');
  }

  private toggleParamActive(option: FilterOption) {
    if (!option.parameter) return;
    option.parameter.active = !option.parameter.active;
  }
}
