import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TipoProductoService {

  constructor(private http: HttpClient) { }

  public getApiData() : Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5139/api/TipoProducto');
  }
}
