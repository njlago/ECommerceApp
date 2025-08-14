import { Component } from '@angular/core';

import { AuthService } from '../../services/auth.service';

import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  Email= '';
  PasswordHash= '' ;
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    console.log({Email: this.Email, PasswordHash: this.PasswordHash});
    this.authService.login({Email: this.Email, PasswordHash: this.PasswordHash}).subscribe({
      next: (res: any) => {
        this.authService.saveToken(res.token);
        this.router.navigate(['/public/catalog']);
      },
      error: (err: any) => this.errorMessage = err.error?.error || 'Login failed'
    });
  }
}