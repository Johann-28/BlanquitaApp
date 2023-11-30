import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductDTO, ProductTypeDTO } from 'src/app/DTOs/products.dto';
import { ProductoService } from 'src/app/https/productos.service';
import { TipoProductoService } from 'src/app/https/tipo-producto.service';


@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.scss'],
})
export class EditDialogComponent  implements OnInit {
  precio: number = 0;
  descripcion: string = '';
  idTipoProducto: number = 0;

product = new ProductDTO;
products: ProductDTO[] = []
id: number = 0;
dataSource: ProductTypeDTO[] = [];
@Output() dialogClosed = new EventEmitter();
productTypes: ProductTypeDTO[] = [];

  constructor(
    private dialogRef: MatDialogRef<EditDialogComponent>,
    private productoService: ProductoService,
    private tipoProductoService: TipoProductoService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
      this.id = data.product.idProducto
      this.idTipoProducto = data.product.idTipoProducto;
      this.precio = data.product.precio;
      this.descripcion = data.product.descripcion;
   }

  ngOnInit() {
    this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
      this.productTypes = productTypesData;
    });
  }

  onSubmit(){
    this.product.idProducto = this.id;
    this.product.idTipoProducto = this.idTipoProducto;
    this.product.precio = this.precio;
    this.product.descripcion = this.descripcion;
    this.productoService.putProduct(this.id, this.product).subscribe(res => {
  })
  this.closeDialog();
}

isNotEmpty(): boolean {
  return this.idTipoProducto > 0 && this.descripcion.length > 0 && this.precio > 0;
}

onSubmitDelete(){
  this.productoService.deleteProduct(this.id).subscribe(res => {
})
this.dialogRef.close();

}

closeDialog(): void {
  this.dialogClosed.emit();
  this.dialogRef.close();
}
}


 



