import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDTO, ProductTypeDTO } from '../DTOs/products.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private dataUrl = 'Producto/';

  constructor(private http: HttpClient) { }

  public getApiData() : Observable<ProductDTO[]> {
    return this.http.get<ProductDTO[]>(this.dataUrl);
  }

  public postProduct(product: ProductDTO): Observable<ProductDTO>{
    return this.http.post<ProductDTO>(this.dataUrl, product)
  }

  public putProduct(id: number, product: ProductDTO): Observable<ProductDTO>{
    const url = `${this.dataUrl}${id}`;
    return this.http.put<ProductDTO>(url, product)
  }

  public deleteProduct(id: number): Observable<ProductDTO>{
    const url = `${this.dataUrl}${id}`;
    return this.http.delete<ProductDTO>(url)
  }
}
