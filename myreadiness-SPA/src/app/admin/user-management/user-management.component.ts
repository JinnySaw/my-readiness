import { Component, OnInit } from '@angular/core';
import { User } from '../../utils/_models/user';
import { AdminService } from '../../utils/_services/admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { Pagination, PaginatedResult } from '../../utils/_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { ResetPasswordComponent } from 'src/app/admin/reset-password/reset-password.component';
import { EmployeeService } from 'src/app/utils/_services/employee.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  users: User[];
  bsModalRef: BsModalRef;
  pagination: Pagination;
  userParams: any = {};
  employeename: any;

  constructor(
    private adminService: AdminService, private employeeService: EmployeeService,
    private modalService: BsModalService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });
  }

  resetFilters() {
    this.userParams.EmpID = null;
    this.employeename = null;
    this.getUsersWithRoles();
  }

  employeeValueChange(event) {
    this.employeeService.getEmployeeByEmployeeName(this.employeename).subscribe((res) => {
      this.userParams.EmpID = res.id;
    });
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRolesPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
    }, error => {
      // console.log(error);
    });
  }

  resetPasswordModal(user: User) {
    const initialState = {
      user
    };
    this.bsModalRef = this.modalService.show(ResetPasswordComponent, {initialState});
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
        }, error => {
          console.log(error);
        });
      }
    });
  }

  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name: 'Admin', value: 'Admin'},
      {name: 'Moderator', value: 'Moderator'},
      {name: 'Member', value: 'Member'},
      {name: 'VIP', value: 'VIP'},
    ];

    // tslint:disable-next-line:prefer-for-of
    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      // tslint:disable-next-line:prefer-for-of
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getUsersWithRoles();
  }
}
