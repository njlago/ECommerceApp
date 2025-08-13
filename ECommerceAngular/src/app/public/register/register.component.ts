import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {
  registerData = { fullName: '', email: '', password: '' };
  errorMessage = '';

  constructor(private authService: AuthService) {}

  onRegister() {
    this.authService.register(this.registerData).subscribe({
      next: () => alert('Registration successful!'),
      error: (err: any) => this.errorMessage = err.error?.error || 'Registration failed'
    });
  }
}