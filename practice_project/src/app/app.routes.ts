import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component/login.component';
import { RegisterComponent } from './auth/register/register.component/register.component';
import { DashboardComponent } from './dashboard.component/dashboard.component';
import { AddProductComponent } from './products/add-product/add-product.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { 
    path: 'dashboard', 
    component: DashboardComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'products/add', 
    component: AddProductComponent,
    canActivate: [authGuard],
    data: { roles: ['admin'] }
  },
  { path: '**', redirectTo: 'dashboard' }
];
