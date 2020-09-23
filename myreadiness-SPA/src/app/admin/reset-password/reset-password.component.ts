import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ResetPassword } from '../../utils/_models/resetpassword';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthService } from '../../utils/_services/auth.service';
import { Router } from '@angular/router';
import { AdminService } from '../../utils/_services/admin.service';
import { User } from '../../utils/_models/user';
import { BsModalRef } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/utils/_services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  @Output() updateSelectedRoles = new EventEmitter();
  user: User;
  resetPasswordForm: FormGroup;
  resetPassword: ResetPassword;

  constructor(
    public bsModalRef: BsModalRef, private authService: AuthService, private userService: UserService,
    private toastrService: ToastrService, private fb: FormBuilder, private router: Router,
    private adminService: AdminService) { }

  ngOnInit() {
    this.validateResetPasswordForm();
  }
  validateResetPasswordForm() {
    this.resetPasswordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword').value === g.get('confirmPassword').value ? null : {'mismatch' : true };
  }

  saveReset() {
    if (this.resetPasswordForm.valid) {
      this.resetPassword = Object.assign({}, this.resetPasswordForm.value);
      this.resetPassword.userId = this.user.id;
      this.userService.resetPassword(this.resetPassword).subscribe(() => {
        this.toastrService.success('Reset Password successful');
        this.bsModalRef.hide();
      }, error => {
        this.toastrService.error(error);
      });
    } else {
      console.log('password changed');
    }
  }

  cancel() {
    if (!this.bsModalRef) {
      return;
    }
    this.bsModalRef.hide();
    this.bsModalRef = null;
  }
}
