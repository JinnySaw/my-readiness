import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule, TabsModule, ModalModule, PaginationModule, BsDatepickerModule, TimepickerModule } from 'ngx-bootstrap';
import { DataTableModule } from 'angular-6-datatable';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from 'src/app/login/login.component';
import { MainComponent } from 'src/app/main/main.component';
import { HeaderComponent } from 'src/app/main/header/header.component';
import { FooterComponent } from 'src/app/main/footer/footer.component';
import { MenuSidebarComponent } from 'src/app/main/menu-sidebar/menu-sidebar.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlankComponent } from 'src/app/pages/blank/blank.component';
import { DashboardComponent } from 'src/app/pages/dashboard/dashboard.component';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { UserListResolver } from './utils/_resolvers/user-list.resolver';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { ErrorInterceptorProvider } from './utils/_services/error.interceptor';
import { HasRoleDirective } from './utils/_directives/hasRole.directive';
import { RegisterComponent } from 'src/app/admin/register/register.component';
import { ResetPasswordComponent } from 'src/app/admin/reset-password/reset-password.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    MenuSidebarComponent,
    ProfileComponent,
    BlankComponent,
    DashboardComponent,
    RolesModalComponent,
    UserManagementComponent,
    HasRoleDirective,
    RegisterComponent,
    ResetPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    DataTableModule,
    ToastrModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    // JwtModule.forRoot({
    //   config: {
    //     // tslint:disable-next-line:object-literal-shorthand
    //     tokenGetter: tokenGetter,
    //     whitelistedDomains: ['localhost:4200'],
    //     blacklistedRoutes: ['localhost/api/auth', 'localhost:5000/api/auth']
    //   }
    // }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['10.0.1.4', 'localhost', 'localhost:5000'],
        blacklistedRoutes: ['10.0.1.4/api/auth', 'localhost:5000/api/auth']
      }
    }),
    PaginationModule.forRoot(),
  ],
  providers: [
    UserListResolver,
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    RolesModalComponent,
    ResetPasswordComponent
  ]
})
export class AppModule { }
