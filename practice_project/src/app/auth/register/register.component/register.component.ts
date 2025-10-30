import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  loading = signal(false);
  error = signal('');
  success = signal('');
  registerForm!: FormGroup; // declare only
  private router = inject(Router);

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.registerForm = this.fb.group({
      displayName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    this.loading.set(true);
    this.error.set('');
    this.success.set('');

    const { email, password, displayName } = this.registerForm.value;

    this.authService.register(email!, password!, displayName!).subscribe({
      next: (res) => {
        localStorage.setItem('accessToken', res.accessToken);
        this.success.set('Registration successful!');
        this.loading.set(false);
        this.router.navigate(['/dashboard']); 
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.error.set(err.error?.message || 'Failed to register. Please try again.');
        this.loading.set(false);
      }
    });
  }
}
