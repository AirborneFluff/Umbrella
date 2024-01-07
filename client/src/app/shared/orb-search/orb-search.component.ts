import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'orb-search',
  templateUrl: './orb-search.component.html',
  styleUrls: ['./orb-search.component.scss']
})
export class OrbSearchComponent {
  @ViewChild('input') inputElement!: ElementRef;
  @Output() searchTerm = new EventEmitter<string>();
  protected _expanded = false;

  public expand() {
    this._expanded = true;
  }

  public collapse() {
    this._expanded = false;
  }

  buttonClick() {
    if (!this._expanded) {
      this.expand();
      return;
    }

    this.emitValue(this.inputElement.nativeElement.value);
  }

  emitValue(value: string) {
    this.searchTerm.emit(value);
    this.collapse();
  }
}
