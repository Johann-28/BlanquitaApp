import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductoComboDTO } from '../dtos/producto-combo-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductoComboService {
  private dataUrl = 'ProductoCombo/'

  constructor(private http:HttpClient) { }

  public postProductoCombo(productoComboDTO:ProductoComboDTO):Observable<ProductoComboDTO>{
    return this.http.post<ProductoComboDTO>(this.dataUrl,productoComboDTO);
  }
}
