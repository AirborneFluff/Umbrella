import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingRoutingModule } from './landing-routing.module';
import { LandingScreenComponent } from './landing-screen/landing-screen.component';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../../shared/shared.module';


@NgModule({
  declarations: [
    LandingScreenComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    LandingRoutingModule,
    SharedModule
  ]
})
export class LandingModule { }
