import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { StorageService } from '../../../core/services/storage.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  loading = signal(false);
  error = signal('');
  success = signal('');
  loginForm!: FormGroup; // declare first
  private router = inject(Router);

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.loading.set(true);
    this.error.set('');
    this.success.set('');

    const { email, password } = this.loginForm.value;

    this.authService.login(email!, password!).subscribe({
      next: (res) => {
        localStorage.setItem('accessToken', res.accessToken);
        console.log(res);
        console.log(res.loggedInUser);
        localStorage.setItem('user',JSON.stringify(res.loggedInUser));
        this.success.set('Login successful!');
        this.loading.set(false);
        this.router.navigate(['/dashboard']); // Navigate after success
      },
      error: (err) => {
        console.error('Login error:', err);
        this.error.set(err.error?.message || 'Invalid credentials');
        this.loading.set(false);
      }
    });
  }
}
