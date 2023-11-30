import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductTypeDTO } from 'src/app/DTOs/products.dto';
import { TipoProductoService } from 'src/app/https/tipo-producto.service';


@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.scss'],
})
export class EditDialogComponent  implements OnInit {
clave: string = '';
descripcion: string = '';
productType = new ProductTypeDTO
id: number = 0;
productTypes: ProductTypeDTO[] = [];
dataSource: ProductTypeDTO[] = [];
@Output() dialogClosed = new EventEmitter();

  constructor(
    private dialogRef: MatDialogRef<EditDialogComponent>,
    private tipoProductoService: TipoProductoService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
      this.clave = data.productType.clave;
      this.descripcion = data.productType.descripcion;
      this.id = data.productType.idTipoProducto
   }

  ngOnInit() {
  }

  onSubmit(){
    this.productType.idTipoProducto = this.id;
    this.productType.clave = this.clave;
    this.productType.descripcion = this.descripcion;
    this.tipoProductoService.putProductType(this.id, this.productType).subscribe(res => {
  })
  this.closeDialog();
}

isNotEmpty(): boolean {
  return this.clave.length > 0 && this.descripcion.length > 0; 
}

onSubmitDelete(){
  this.tipoProductoService.deleteProductType(this.id).subscribe(res => {
})
this.dialogRef.close();

}

closeDialog(): void {
  this.dialogClosed.emit();
  this.dialogRef.close();
}
}


 



