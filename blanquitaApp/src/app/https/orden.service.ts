import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ComboDTO } from '../dtos/combo-dto';
import { OrdenFormDTO } from '../dtos/orden-form-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrdenService {
  private dataUrl = 'Orden'

  constructor(private http: HttpClient) { }

  public postCombo(orden:OrdenFormDTO): Observable<void>{
    return this.http.post<void>(this.dataUrl,orden);
  }
}
