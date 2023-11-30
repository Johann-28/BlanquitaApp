import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductoComboDTO } from '../dtos/producto-combo-dto';
import { Observable } from 'rxjs';
import { DetalleOrdenDTO } from '../dtos/detalle-orden-dto';

@Injectable({
  providedIn: 'root'
})
export class ProductoComboService {
  private dataUrl = 'ProductoCombo/'

  constructor(private http:HttpClient) { }

  public postProductoCombo(productoComboDTO:ProductoComboDTO):Observable<ProductoComboDTO>{
    return this.http.post<ProductoComboDTO>(this.dataUrl,productoComboDTO);
  }

  public getProductosPorCombo(idCombo:number):Observable<DetalleOrdenDTO[]>{
    return this.http.get<DetalleOrdenDTO[]>(`${this.dataUrl}${idCombo}`)
  }

  deletePorComboyProducto(productoComboDTO:ProductoComboDTO):Observable<void>{
    return this.http.delete<void>(`${this.dataUrl}PorComboyProducto`,productoComboDTO)
  }
}
