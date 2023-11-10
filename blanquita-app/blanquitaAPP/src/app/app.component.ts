import { Component, OnInit } from '@angular/core';
import { ProductoService } from './Http/producto.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'blanquitaAPP';

  constructor(
    private productoService: ProductoService
  ) { }
  ngOnInit(): void {
  }

  public consultarTiposProductos(){
      this.productoService.getApiData().subscribe((data) => console.log(data));
  }

  

}
