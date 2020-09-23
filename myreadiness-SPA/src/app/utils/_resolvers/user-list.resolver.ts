import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/user';
import { AdminService } from '../_services/admin.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class UserListResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 15;
    constructor(
        private adminService: AdminService,
        private router: Router, private toastrService: ToastrService) {}
    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.adminService.getUsersWithRolesPaging(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.toastrService.error('Problem retrieving your data');
                // this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
