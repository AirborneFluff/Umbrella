import { Component } from '@angular/core';
import { AccountService } from './core/services/account.service';
import { TEST_EMAIL, TEST_PASSWORD } from './developer.secrets';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(protected account: AccountService) {
    account.login({email: TEST_EMAIL, password: TEST_PASSWORD}).subscribe();
  }
}
