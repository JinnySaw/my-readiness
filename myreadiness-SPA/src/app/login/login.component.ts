import { Component, OnInit, OnDestroy, Renderer2 } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../utils/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  model: any = {};
  isExistSubordinate: boolean;
  public loginForm: FormGroup;
  constructor(
    private renderer: Renderer2,
    private toastrService: ToastrService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.renderer.addClass(document.body, 'login-page');
    this.loginForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    });
  }

  logIn() {
    if (this.loginForm.valid) {
      this.model = Object.assign({}, this.loginForm.value);
      this.authService.login(this.model).subscribe(next => {
        this.toastrService.success('Logged in successfully');
      }, error => {
        this.toastrService.error('Error', error);
        console.log(error);
      }, () => {
        this.router.navigate(['/']);
      });
    } else {
      this.toastrService.error('Error', 'Fill the form');
    }
  }

  ngOnDestroy() {
    this.renderer.removeClass(document.body, 'login-page');
  }

}
