import { CanDeactivateFn } from '@angular/router';
import { Observable } from 'rxjs';

export interface ComponentCanDeactivate {
  canDeactivate: () => boolean | Observable<boolean>
}

export const preventUnsavedChangesGuard: CanDeactivateFn<ComponentCanDeactivate> = (component) => {
  return component.canDeactivate() ?
    true :
    confirm('WARNING: You have unsaved changes. Press Cancel to go back and save these changes, or OK to lose these changes.');
};
