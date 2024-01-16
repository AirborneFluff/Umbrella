import { Component } from '@angular/core';
import { AccountService } from '../../../core/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-screen',
  templateUrl: './landing-screen.component.html',
  styleUrls: ['./landing-screen.component.scss']
})
export class LandingScreenComponent {

  constructor(private account: AccountService, private router: Router) {
  }

  demoLogin() {
    this.account.login({
      email: 'demo',
      password: 'Demologin@1'
    }).subscribe({
      next: () => this.router.navigateByUrl('/app'),
      error: e => console.log(e)
    })
  }

}
