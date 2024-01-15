import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appInputGhost]'
})
export class AppInputGhostDirective {
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

  constructor(el: ElementRef, renderer: Renderer2) {
    for (const key in this.classes) {
      renderer.addClass(el.nativeElement, this.classes[key])
    }
  }

}
