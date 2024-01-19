import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingScreenComponent } from './landing-screen/landing-screen.component';
import { LoginComponent } from './login/login.component';
import { RegisterOrganisationComponent } from './register-organisation/register-organisation.component';
import { preventUnsavedChangesGuard } from '../../core/guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  {
    path: '',
    component: LandingScreenComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    canDeactivate: [preventUnsavedChangesGuard],
    component: RegisterOrganisationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LandingRoutingModule { }
