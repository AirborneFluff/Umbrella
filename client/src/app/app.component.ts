import { Component } from '@angular/core';
import { AccountService } from './core/services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(protected account: AccountService) {
  }
}
