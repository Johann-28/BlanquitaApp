import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { LoginRequestDto } from '../dtos/login-request-dto';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient , private router : Router) {}
  private dataUrl = 'login/';

  login(credentials: LoginRequestDto , recuerdame : boolean) {
    return this.http
      .post(this.dataUrl + 'authenticate', credentials)
      .pipe(
        tap((response: any) =>{
          if(recuerdame){
            localStorage.setItem('jwt', response.token);
          }else{
            sessionStorage.setItem('jwt', response.token);
          }
          this.router.navigate(['tabs']);
        } )
      );
  }

  logout() {
    localStorage.removeItem('jwt');
    sessionStorage.removeItem('jwt');
    this.router.navigate(['login']);
  }

  isLoggedIn() {
    return !!localStorage.getItem('jwt') || !!sessionStorage.getItem('jwt');
  }
}
