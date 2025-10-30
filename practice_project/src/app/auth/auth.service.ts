import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  refreshTokenExpiresAt: string;
  loggedInUser: {
    id: string;
    email: string;
    displayName: string;
    isAdmin: boolean;
  };
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7158/api/Auth';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }),
    withCredentials: true // Enable credentials for CORS
  };

  constructor(private http: HttpClient) {}

  register(email: string, password: string, displayName: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, {
      email,
      password,
      displayName
    }, this.httpOptions);
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, {
      email,
      password
    }, this.httpOptions);
  }
}
