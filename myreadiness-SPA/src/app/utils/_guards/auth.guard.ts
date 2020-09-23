import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router, private toastrService: ToastrService) {}

    canActivate(next: ActivatedRouteSnapshot): boolean {
        const roles = next.firstChild.data['roles'] as Array<string>;

        if (roles) {
            const match = this.authService.roleMatch(roles);
            if (match) {
                return true;
            } else {
                this.router.navigate(['members']);
                this.toastrService.error('You are not authorised to access this area');
            }
            if (this.authService.loggedIn()) {
                return true;
            }
        }

        if (this.authService.loggedIn()) {
            return true;
        }
        // this.notificationsService.error('You shall not pass!!');
        this.router.navigate(['/login']);
        return false;
    }
}
