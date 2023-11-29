import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CorteCajaListadoDTO } from '../dtos/corte-caja-listado-dto';
import { Observable } from 'rxjs';
import { ObtenerListadoFormDTO } from '../dtos/obtener-listado-form-dto';
import { ObtenerListadoSumaDTO } from '../dtos/obtener-listado-suma-dto';
import { CorteCajaFormDTO } from '../dtos/corte-caja-form-dto';

@Injectable({
  providedIn: 'root'
})
export class CorteCajaService {
  private dataUrl = 'CorteCaja/'

  constructor(private http:HttpClient) { }

  public getObtenerListado(obtenerListadoFormDTO:ObtenerListadoFormDTO) : Observable<CorteCajaListadoDTO[]>{
    return this.http.post<CorteCajaListadoDTO[]>(this.dataUrl+'ObtenerListado', obtenerListadoFormDTO)
  }

  public ObtenerListadoSuma(obtenerListadoFormDTO:ObtenerListadoFormDTO) : Observable<ObtenerListadoSumaDTO>{
    return this.http.post<ObtenerListadoSumaDTO>(this.dataUrl+'ObtenerListadoSuma',obtenerListadoFormDTO);
  }

  public postCorteCaja(corteCaja:CorteCajaFormDTO): Observable<CorteCajaFormDTO>{
    return this.http.post<CorteCajaFormDTO>(this.dataUrl,corteCaja)
  }

}
