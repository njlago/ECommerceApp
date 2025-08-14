import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
  private apiUrl = 'http://localhost:5146/api/auth'; // Adjust if needed

  constructor(private http: HttpClient) {}

  register(user: { fullName: string; email: string; passwordHash: string }): Observable<string> {
  const payload = { ...user, role: 'customer' };
  return this.http.post(`${this.apiUrl}/register`, payload, { responseType: 'text' });
}

}
