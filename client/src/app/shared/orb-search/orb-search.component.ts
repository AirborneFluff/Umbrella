import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { map, startWith, take } from 'rxjs';

@Component({
  selector: 'orb-search',
  templateUrl: './orb-search.component.html',
  styleUrls: ['./orb-search.component.scss']
})
export class OrbSearchComponent implements OnInit {
  @ViewChild('input') inputElement!: ElementRef;
  @Output() onSearch = new EventEmitter<string | undefined>();
  @Input() alwaysOpen: boolean = false;
  protected _expanded = false;

  private hasEmitted$ = this.onSearch.pipe(
    startWith(false),
    map(() => true)
  )

  public ngOnInit() {
    if (this.alwaysOpen)
      this._expanded = true;
  }

  public expand() {
    this._expanded = true;

    this.hasEmitted$.pipe(take(1)).subscribe(emitted => {
      console.log(emitted)
      if (emitted) this.emitValue(undefined)
    })

    setTimeout(() => {
      this.inputElement.nativeElement.focus()
    },0);
  }

  public collapse() {
    if (this.alwaysOpen) return;
    this._expanded = false;
  }

  buttonClick() {
    if (!this._expanded) {
      this.expand();
      return;
    }
  }

  emitValue(value: string | undefined) {
    if (value) {
      this.onSearch.emit(value);
      return;
    }
    this.onSearch.emit(undefined);
  }
}
