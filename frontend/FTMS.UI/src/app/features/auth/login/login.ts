import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  errorMessage = '';

  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });


  submit(): void {

    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(
      this.loginForm.value as {
        username: string;
        password: string;
      }
    )
      .subscribe({
        next: (res) => {
          const data = res.data;
          this.authService.saveToken(data.token);
          localStorage.setItem('username', data.username);
          localStorage.setItem('role', data.role);
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Invalid.');
        }
      });
  }
}