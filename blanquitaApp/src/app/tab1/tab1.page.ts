import { Component } from '@angular/core';
import { TipoProductoService as TipoProductoService } from '../https/tipo-producto.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {
  constructor(private productoService: TipoProductoService) {}
  ngOnInit(): void {}

  public consultarTiposProductos() {
    this.productoService.getApiData().subscribe((data) => console.log(data));
  }
}
