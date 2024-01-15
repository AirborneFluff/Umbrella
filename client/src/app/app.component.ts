import { Component } from '@angular/core';
import { AuthService } from './auth/services/auth.service';
import { TEST_EMAIL, TEST_PASSWORD } from './developer.secrets';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private auth: AuthService) {
    this.login();
  }

  login() {
    this.auth.login({email: TEST_EMAIL, password: TEST_PASSWORD}).subscribe();
  }

  logout() {
    this.auth.logout().subscribe();
  }

  getUser() {
    this.auth.getUser().subscribe(value => console.log(value))
  }
}
