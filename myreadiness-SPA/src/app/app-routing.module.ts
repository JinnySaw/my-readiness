import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from 'src/app/main/main.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { BlankComponent } from 'src/app/pages/blank/blank.component';
import { DashboardComponent } from 'src/app/pages/dashboard/dashboard.component';
import { LoginComponent } from 'src/app/login/login.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { UserListResolver } from './utils/_resolvers/user-list.resolver';
import { AuthGuard } from './utils/_guards/auth.guard';
import { RegisterComponent } from 'src/app/admin/register/register.component';


const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'users',
        component: UserManagementComponent,
        resolve: {users: UserListResolver},
        data: {roles: ['Admin']}
      },
      {
        path: 'register',
        component: RegisterComponent,
        resolve: {users: UserListResolver},
        data: {roles: ['Admin']}
    },
      {
        path: 'profile',
        component: ProfileComponent
      },
      {
        path: 'blank',
        component: BlankComponent
      },
      {
        path: '',
        component: DashboardComponent
      }
    ]
  },
  // { path: 'login', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
