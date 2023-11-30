export class GeneralConstant {
  public static TOKEN_KEY = 'token';
  public static CLAVE_USUARIO_TRABAJADOR = '001';
  public static CLAVE_USUARIO_ADMINISTRADOR = '002';

  public static readonly CONFIG = {
    claims: {
      name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
      nameIdentifier:
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
      role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
    },
  };
}
