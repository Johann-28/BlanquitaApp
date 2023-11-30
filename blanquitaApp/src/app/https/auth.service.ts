import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { LoginRequestDto } from '../dtos/login-request-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  private dataUrl = 'login/';

  login(credentials: LoginRequestDto) {
    return this.http
      .post(this.dataUrl + 'authenticate', credentials)
      .pipe(
        tap((response: any) => localStorage.setItem('jwt', response.token))
      );
  }

  logout() {
    localStorage.removeItem('jwt');
  }

  isLoggedIn() {
    return !!localStorage.getItem('jwt');
  }
}