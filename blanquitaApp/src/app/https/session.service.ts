import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { GeneralConstant } from '../Utils/general-constants';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  constructor(private http: HttpClient) {}

  public obtenerIdUsuarioSesion(): number | null {
    const token = this.getToken();
    const decodedToken = this.getDecodedAccessToken(token);
    return token
      ? decodedToken[GeneralConstant.CONFIG.claims.nameIdentifier]
      : null;
  }
  public obtenerClaveRolUsuarioSesion(): string | null {
    const token = this.getToken();
    const decodedToken = this.getDecodedAccessToken(token);
    return token ? decodedToken[GeneralConstant.CONFIG.claims.role] : null;
  }
  public obtenerNombreUsuarioSesion(): string | null {
    const token = this.getToken();
    const decodedToken = this.getDecodedAccessToken(token);
    return token ? decodedToken[GeneralConstant.CONFIG.claims.name] : null;
  }

  public esAdministrador() {
    const esAdmin =
      this.obtenerClaveRolUsuarioSesion() ===
      GeneralConstant.CLAVE_USUARIO_ADMINISTRADOR;
    return esAdmin;
  }

  getDecodedAccessToken(token: string | null): any {
    if (token === null) {
      return null;
    }

    try {
      const decodedToken = jwtDecode(token);

      return decodedToken;
    } catch (Error) {
      return null;
    }
  }

  private getToken(): string | null {
    return localStorage.getItem('jwt') || sessionStorage.getItem('jwt');
  }
}
