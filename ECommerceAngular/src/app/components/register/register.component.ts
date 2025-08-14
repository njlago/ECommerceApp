import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Catalog } from "../catalog.component";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule, Catalog],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {
  registerData = { fullName: '', email: '', passwordHash: '',  role: 'Customer'};
  errorMessage = '';

  constructor(private userService: UserService) {}

  onRegister() {
    this.userService.register(this.registerData).subscribe({
      next: () => alert('Registration successful!'),
      error: (err: any) => this.errorMessage = err.error?.error || 'Registration failed'
    });
  }
}