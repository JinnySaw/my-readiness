import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './utils/_models/user';
import { AuthService } from './utils/_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'myreadiness-SPA';
  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token && user) {
      if (this.jwtHelper.isTokenExpired(token)) {
        this.authService.logout();
      } else {
        this.authService.currentUser = user;
        this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      }
    }
    }
}
