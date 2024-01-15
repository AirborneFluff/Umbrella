import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'orb-search',
  templateUrl: './orb-search.component.html',
  styleUrls: ['./orb-search.component.scss']
})
export class OrbSearchComponent implements OnInit {
  @ViewChild('input') inputElement!: ElementRef;
  @Output() onSearch = new EventEmitter<string>();
  @Input() alwaysOpen: boolean = false;
  protected _expanded = false;

  public ngOnInit() {
    if (this.alwaysOpen) this.expand();
  }

  public expand() {
    this._expanded = true;
    setTimeout(()=> this.inputElement.nativeElement.focus(),0);
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

    this.emitValue(this.inputElement.nativeElement.value)
  }

  emitValue(value: string) {
    this.onSearch.emit(value);
  }
}
