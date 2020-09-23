import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/utils/_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  // systemSetting: SystemSetting[];

  constructor(
    private http: HttpClient,
    private toastrService: ToastrService,
    private routerService: Router,
    private jwtHelperService: JwtHelperService
    // ,
    // private notificationsService: NotificationsService
  ) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.clear();
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          if (user.user.empID > 0 ) {
            localStorage.setItem('empname', user.user.employeeName);
            localStorage.setItem('empid', user.user.empID.toString());
          } else {
            localStorage.setItem('empname', this.decodedToken.unique_name);
          }
        }
      })
    );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    // set current user's role
    localStorage.setItem('roles', userRoles.toString());
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('empid');
    this.toastrService.success('Logged out!');
    this.routerService.navigate(['/home']);
  }

  // SystemSetting(): Observable<SystemSetting[]> {
  //     return this.http.get<SystemSetting[]>(this.baseUrl + 'activesystemsettings');
  // }
}
