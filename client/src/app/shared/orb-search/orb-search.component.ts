import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'orb-search',
  templateUrl: './orb-search.component.html',
  styleUrls: ['./orb-search.component.scss']
})
export class OrbSearchComponent {
  @ViewChild('input') inputElement!: ElementRef;
  @Output() onSearch = new EventEmitter<string>();
  protected _expanded = false;

  public expand() {
    this._expanded = true;
    setTimeout(()=>{ // this will make the execution after the above boolean has changed
      this.inputElement.nativeElement.focus();
    },0);
  }

  public collapse() {
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
    this.collapse();
    if (!value) return;
    this.onSearch.emit(value);
  }
}
