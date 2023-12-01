import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductDTO, ProductTypeDTO } from 'src/app/DTOs/products.dto';
import { ProductoService } from 'src/app/https/productos.service';
import { TipoProductoService } from 'src/app/https/tipo-producto.service';
import { AlertService } from '../../https/alert.service';


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
    @Inject(MAT_DIALOG_DATA) public data: any,
    private AlertService:AlertService
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
  this.AlertService.mostrarModal('Exito','Se modifico exitosamente el producto')
}

isNotEmpty(): boolean {
  return this.idTipoProducto > 0 && this.descripcion.length > 0 && this.precio > 0;
}

onSubmitDelete(){
  this.productoService.deleteProduct(this.id).subscribe(res => {
    this.dialogRef.close();
    this.AlertService.mostrarModal('Exito','Se elimino exitosamente el producto en el sistema')
},(err) =>{
  this.dialogRef.close();
  this.AlertService.mostrarModal('Error','No se puede eliminar el producto ya que este contiene combo en el, elimine primero los combos relacionados al producto')
})


}

closeDialog(): void {
  this.dialogClosed.emit();
  this.dialogRef.close();
}
}


 



