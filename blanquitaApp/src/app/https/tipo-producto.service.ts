import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductTypeDTO } from '../DTOs/products.dto';

@Injectable({
  providedIn: 'root'
})
export class TipoProductoService {

  private dataUrl = 'TipoProducto/';

  constructor(private http: HttpClient) { }

  public getApiData() : Observable<any[]> {
    return this.http.get<any[]>(this.dataUrl);
  }

  public postProductType(productType: ProductTypeDTO): Observable<ProductTypeDTO>{
    return this.http.post<ProductTypeDTO>(this.dataUrl,productType)
  }

  public putProductType(id: number, productType: ProductTypeDTO): Observable<ProductTypeDTO>{
    const url = `${this.dataUrl}${id}`;
    return this.http.put<ProductTypeDTO>(url, productType)
  }

  public deleteProductType(id: number): Observable<ProductTypeDTO>{
    const url = `${this.dataUrl}${id}`;
    return this.http.delete<ProductTypeDTO>(url)
  }
}
