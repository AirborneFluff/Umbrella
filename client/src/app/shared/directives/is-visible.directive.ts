import { AfterViewInit, Directive, ElementRef, EventEmitter, Input, Output } from '@angular/core';

@Directive({
  selector: '[isVisible]'
})
export class IsVisibleDirective implements AfterViewInit {
  @Input() bottomOffset: number = 0;
  @Output() visible = new EventEmitter<boolean>();
  @Output() onBecomeVisible = new EventEmitter();
  @Output() onBecomeInvisible = new EventEmitter();
  constructor(private el: ElementRef) {}

  ngAfterViewInit() {
    const observedElement = this.el.nativeElement;

    const observer = new IntersectionObserver(([entry]) => {
      this.visible.emit(entry.isIntersecting);
      entry.isIntersecting ? this.onBecomeVisible.emit() : this.onBecomeInvisible.emit();
    }, { threshold: 0, rootMargin: `0px 0px ${this.bottomOffset}px 0px` })
    observer.observe(observedElement)
  }
}
