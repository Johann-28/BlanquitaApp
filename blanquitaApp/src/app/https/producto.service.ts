import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductoDTO } from '../dtos/producto-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private dataUrl = 'Producto/'

  constructor(private http:HttpClient) { }

  public getProductos() :Observable<ProductoDTO[]> {
    return this.http.get<ProductoDTO[]>(this.dataUrl)
  }
}
