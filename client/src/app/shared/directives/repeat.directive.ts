import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[repeat]'
})
export class RepeatDirective {
  @Input() set repeat(amount: number) {
    for (let i = 0; i < amount; i++) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }
  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
  ) {}

}
