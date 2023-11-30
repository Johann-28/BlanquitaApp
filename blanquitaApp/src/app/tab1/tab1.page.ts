import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TipoProductoService } from '../https/tipo-producto.service';
import { ProductTypeDTO } from '../DTOs/products.dto';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EditDialogComponent } from './edit-dialog/edit-dialog.component';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss'],
})
export class Tab1Page implements OnInit {
  displayedColumns: string[] = ['Clave', 'Descripción', 'Edición'];
  editDialogConfig = new MatDialogConfig();
  productType = new ProductTypeDTO();
  dataSource: ProductTypeDTO[] = [];
  productTypes: ProductTypeDTO[] = [];
  clave: string = '';
  descripcion: string = '';

  constructor(
    private dialog: MatDialog,
    private tipoProductoService: TipoProductoService,
    private cdr: ChangeDetectorRef
  ) {
    
  }

  ngOnInit(): void {
    this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
      this.productTypes = productTypesData;
      this.dataSource = this.productTypes;
    });
  }

  consultarTiposProductos() {
    this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
      this.productTypes = productTypesData;
    });
  }

  isNotEmpty(): boolean {
    return this.clave.length > 0 && this.descripcion.length > 0;
  }

  editProductType(row: any): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '30%',
      height: '60%',
      data: {
        productType: row
      }
    });

    dialogRef.afterClosed().subscribe(result => {
        this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
        this.productTypes = productTypesData;
        this.dataSource = this.productTypes;
        this.ngOnInit();
      });
    });
  }

  onSubmit() {
    this.productType.clave = this.clave;
    this.productType.descripcion = this.descripcion;

    this.tipoProductoService.postProductType(this.productType).subscribe(() => {
        this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
          this.productTypes = productTypesData;
          this.dataSource = this.productTypes;
          this.clave = '';
          this.descripcion = '';
        });
    });

  }
}
