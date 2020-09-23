import { Component, OnInit, Output } from '@angular/core';
import { AuthService } from '../../utils/_services/auth.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from '../../utils/_models/user';
import { Router } from '@angular/router';
import { AdminService } from '../../utils/_services/admin.service';
import { Location } from '@angular/common';
import { Employee } from 'src/app/utils/_models/employee';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from 'src/app/utils/_services/employee.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: User;
  users: User[];
  employees: Employee[];
  registerForm: FormGroup;

  selectedValue: string;
  emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';

  constructor(
    private authService: AuthService, private router: Router,
    private toastrService: ToastrService, private fb: FormBuilder,
    private employeeService: EmployeeService,
    private adminService: AdminService,
    private location: Location) { }

  ngOnInit() {
    this.getEmployees();
    this.createRegisterForm();
    this.getUsersWithRoles();
  }
  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      console.log(error);
    });
  }
  createRegisterForm() {
    this.registerForm = this.fb.group({
      empid: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.pattern(this.emailPattern)]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch' : true };
  }

  register() {
    if (this.registerForm.valid) {

      // clone the registeForm.value ans assign
      this.user = Object.assign({}, this.registerForm.value);

      this.authService.register(this.user).subscribe(() => {
        this.toastrService.success('Register successful');
        location.reload();
      }, error => {
        this.toastrService.error(error);
      }, () => {
        // this.authService.login(this.user).subscribe(() => {
        //   this.router.navigate(['/members']);
        // });
      });
    } else {
      console.log('register');
    }
  }

  getEmployees() {
    this.employeeService.getEmployeesNotInUsers().subscribe(res => {
      this.employees = res;
    });
  }

}
