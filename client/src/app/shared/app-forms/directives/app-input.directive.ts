import { AfterViewInit, Directive, ElementRef, OnDestroy, Renderer2 } from '@angular/core';
import { NgControl } from '@angular/forms';
import { distinctUntilChanged, map, Observable, of, Subscription } from 'rxjs';

@Directive({
  selector: '[appInput]'
})
export class AppInputDirective implements AfterViewInit, OnDestroy {
  classes: string[] = [
    "focus:outline-none",
    "focus:blur-0",
    "min-w-0",
    "w-full",
    "h-12",
    "rounded-lg",
    "px-3",
    "bg-item-light",
    "border"
  ]

  errorClasses: string[] = [
    "border-2",
    "border-red-400"
  ]

  private subscriptions = new Subscription();
  valid$: Observable<boolean | null> = of(null);

  constructor(private el: ElementRef, private renderer: Renderer2, private control: NgControl) {
    for (const key in this.classes) {
      renderer.addClass(el.nativeElement, this.classes[key])
    }
  }

  ngAfterViewInit(): void {
    this.valid$ = this.control.valueChanges!.pipe(
      map(() => this.control.valid),
      distinctUntilChanged()
    )
    this.subscriptions.add(
      this.valid$.subscribe(valid => valid ? this.clearErrorClasses() : this.addErrorClasses()));
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private addErrorClasses() {
    for (const key in this.errorClasses) {
      this.renderer.addClass(this.el.nativeElement, this.errorClasses[key])
    }
  }

  private clearErrorClasses() {
    for (const key in this.errorClasses) {
      this.renderer.removeClass(this.el.nativeElement, this.errorClasses[key])
    }
  }
}
