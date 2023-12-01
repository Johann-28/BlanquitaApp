import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TipoProductoService } from '../https/tipo-producto.service';
import { ProductDTO, ProductTypeDTO } from '../DTOs/products.dto';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EditDialogComponent } from './edit-dialog/edit-dialog.component';
import { ProductoService } from '../https/productos.service';
import { TitleService } from '../https/title.service';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss'],
})
export class Tab2Page implements OnInit {
  displayedColumns: string[] = ['Descripción', 'TipoProducto', 'Precio'];
  editDialogConfig = new MatDialogConfig();
  dataSource: ProductDTO[] = []
  precio: number = 0;
  descripcion: string = '';
  idTipoProducto: number = 0;
  productTypes: ProductTypeDTO[] = [];
  protected titulo = 'Productos';

  producto = new ProductDTO;
  productos: ProductDTO[] = []

  constructor(
    private dialog: MatDialog,
    private productoService: ProductoService,
    private tipoProductoService: TipoProductoService,
    private titleService : TitleService
  ) {
    
  }

  ionViewWillEnter(){
    this.changeTitle();
  }

  ngOnInit(): void {
    this.productoService.getApiData().subscribe((productosData: ProductDTO[]) => {
      this.productos = productosData;
      this.dataSource = this.productos;
    });
    this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
      this.productTypes = productTypesData;
    });
  }

  isNotEmpty(): boolean {
    return this.idTipoProducto > 0 && this.descripcion.length > 0 && this.precio > 0;
  }

  editProduct(row: any): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '30%',
      height: '60%',
      data: {
        product: row
      }
    });

    dialogRef.afterClosed().subscribe(result => {
        this.productoService.getApiData().subscribe((productsData: ProductDTO[]) => {
        this.productos = productsData;
        this.dataSource = this.productos;
        this.ngOnInit();
      });
    });
  }

  onSubmit() {
    this.producto.idTipoProducto = this.idTipoProducto;
    this.producto.precio = this.precio;
    this.producto.descripcion = this.descripcion;

    this.productoService.postProduct(this.producto).subscribe(() => {
        this.productoService.getApiData().subscribe((productsData: ProductDTO[]) => {
          this.productos = productsData;
          this.dataSource = this.productos;
          this.idTipoProducto = 0;
          this.precio = 0;
          this.descripcion = '';
        });
    });

  }

  getTipoProductoDescription(idTipoProducto: any): any {
    const tipoProducto = this.productTypes.find(tipo => tipo.idTipoProducto === idTipoProducto);
    return tipoProducto?.descripcion ?? '';
  }

  changeTitle(){
    this.titleService.changeTitle(this.titulo);
  }
  
}
