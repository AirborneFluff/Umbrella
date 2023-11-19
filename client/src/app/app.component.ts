import { Component } from '@angular/core';
import { ApiService } from './core/services/api.service';
import { AuthService } from './auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private api: ApiService, private auth: AuthService) {
  }

  login() {
    this.auth.login({email: "email", password: "password"}).subscribe();
  }

  logout() {
    this.auth.logout().subscribe();
  }

  getUser() {
    this.auth.getUser().subscribe(value => console.log(value))
  }

  getId() {
    this.api.components.getById(1).subscribe(value => console.log(value));
  }
}
