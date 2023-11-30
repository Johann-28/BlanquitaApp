import { Component } from '@angular/core';
import { ComboDTO } from '../dtos/combo-dto';
import { ComboService } from '../https/combo.service';
import { ProductDTO } from '../DTOs/products.dto';
import { ProductoService } from '../https/producto.service';
import { ProductoDTO } from '../dtos/producto-dto';
import { DetalleOrdenDTO } from '../dtos/detalle-orden-dto';
import { ProductoComboService } from '../https/producto-combo.service';
import { ProductoComboDTO } from '../dtos/producto-combo-dto';
import { MatDialog } from '@angular/material/dialog';
import { EditarComboComponent } from './editar-combo/editar-combo.component';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {
  combos:ComboDTO[] = [];
  productos:ProductoDTO[] = [];
  detalleOrden: DetalleOrdenDTO[] = [];
  combo: ComboDTO = {
    idCombo: 0,
    descripcion: '',
    total:0
  }

  constructor(private ComboService:ComboService,
              private ProductoService:ProductoService,
              private ProductoComboService:ProductoComboService,
              private dialog: MatDialog,
              private alertController:AlertController) {}

  ionViewWillEnter(){
    this.obtenerCombos();
    this.obtenerProductos();
  }

  obtenerCombos(){
    this.ComboService.getCombos().subscribe(res => {
      this.combos = res;
    })
  }
  obtenerProductos(){
    this.ProductoService.getProductos().subscribe(res => {
      this.productos = res;
    })
  }

  agregarProducto(idProducto:number, precio: number, descripcion:string){
    const existeElemento = this.detalleOrden.some(detalle => detalle.idProducto === idProducto);
    if(existeElemento){
      this.detalleOrden.forEach(elm => {
        if(elm.idProducto == idProducto ){
          elm.cantidad += 1;
          elm.total += precio
        }
      })
    }
    else{
      let aux:DetalleOrdenDTO = {
        idProducto,
        descripcion: descripcion,
        cantidad: 1,
        total: precio
      }
      this.detalleOrden.push(aux);
    }
    this.obtenerTotalOrden();
  }

  eliminarProducto(idProducto:number, precio: number, descripcion:string){
    const existeElemento = this.detalleOrden.some(detalle => detalle.idProducto === idProducto);
    if(existeElemento){
      this.detalleOrden.forEach(elm => {
        if(elm.idProducto == idProducto ){
          elm.cantidad -= 1;
          elm.total -= precio
        }

        if(elm.cantidad === 0){
          this.detalleOrden = this.detalleOrden.filter(el => el.idProducto != idProducto);
        }
      })
      this.obtenerTotalOrden();
    }
  }

  habilitarBtnMenosProducto (idProducto:number):boolean{
    return ! this.detalleOrden.some(el => el.idProducto === idProducto)
  }

  eliminarDeOrden(idProducto?:number, idCombo?: number){
    if(idCombo){
      this.detalleOrden = this.detalleOrden.filter(el => el.idCombo != idCombo);
    }
    else{
      this.detalleOrden = this.detalleOrden.filter(el => el.idProducto != idProducto);
    }
    this.obtenerTotalOrden();
  }

  enviarCombo(){
    this.ComboService.postCombo(this.combo).subscribe(res => {
      this.detalleOrden.forEach(x => {
        let aux:ProductoComboDTO = {
          idProductoCombo: 0,
          idCombo: res.idCombo,
          idProducto: x.idProducto || 0,
        };

        this.ProductoComboService.postProductoCombo(aux).subscribe(res => {

        });

        this.obtenerCombos();
        this.mostrarMensajeExitoAgregar();
        this.combo.descripcion = '';
        this.combo.total = 0;
        this.detalleOrden = []
      })
    })
  }

  habilitarBtn():boolean{
    if(this.combo.descripcion === '' || this.combo.total === 0 || this.detalleOrden.length == 0){
      return true;
    }
    return false
  }

  editProductType(row: ComboDTO): void {
    const dialogRef = this.dialog.open(EditarComboComponent, {
      width: '80%',
      height: '80%',
      data: row
    });

    // dialogRef.afterClosed().subscribe(result => {
    //     this.tipoProductoService.getApiData().subscribe((productTypesData: ProductTypeDTO[]) => {
    //     this.productTypes = productTypesData;
    //     this.dataSource = this.productTypes;
    //   });
    // });
  }

  eliminarCombo(combo:ComboDTO){
    this.ProductoComboService.deletePorCombo(combo.idCombo).subscribe(res => {
      this.ComboService.deleteCombo(combo.idCombo).subscribe(res => {
        this.mostrarMensajeExitoEliminar();
        this.obtenerCombos();
      })
    })
  }

  async mostrarMensajeExitoEliminar(){
    const mensaje = await this.alertController.create({
      header: 'Exito',
      message: 'Combo eliminado exitosamente del sistema',
      buttons: ['Ok']
    });

    await mensaje.present()
  }

  async mostrarMensajeExitoAgregar(){
    const mensaje = await this.alertController.create({
      header: 'Exito',
      message: 'Combo agregado exitosamente del sistema',
      buttons: ['Ok']
    });

    await mensaje.present()
  }

  obtenerTotalOrden(){
    this.combo.total = 0;

    this.detalleOrden.forEach(el => {
      this.combo.total += el.total;
    })
  }

}
