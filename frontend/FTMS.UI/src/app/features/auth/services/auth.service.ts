import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from '../../../core/services/api';
import { ApiResponse } from '../../../core/models/api-response.model';

import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = inject(ApiService);

  login(request: LoginRequest): Observable<ApiResponse<LoginResponse>> {
    return this.api.post<ApiResponse<LoginResponse>>(
      '/api/users/login',
      request
    );
  }

  logout(): void {
    localStorage.removeItem('bearerToken');
  }

  saveToken(token: string): void {
    localStorage.setItem('bearerToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('bearerToken');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }
}