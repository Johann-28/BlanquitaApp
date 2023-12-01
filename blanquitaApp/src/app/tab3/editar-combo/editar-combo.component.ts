import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { ComboDTO } from 'src/app/dtos/combo-dto';
import { ProductoComboService } from '../../https/producto-combo.service';
import { DetalleOrdenDTO } from '../../dtos/detalle-orden-dto';
import { ProductoDTO } from 'src/app/dtos/producto-dto';
import { ProductoService } from '../../https/producto.service';
import { ProductoComboDTO } from 'src/app/dtos/producto-combo-dto';
import { ComboService } from '../../https/combo.service';
import { AlertController } from '@ionic/angular';
import { AlertService } from '../../https/alert.service';

@Component({
  selector: 'app-editar-combo',
  templateUrl: './editar-combo.component.html',
  styleUrls: ['./editar-combo.component.scss'],
})
export class EditarComboComponent  implements OnInit {
  combo:ComboDTO = {
    idCombo: 0,
    descripcion: '',
    total: 0
  }
  detalleOrden:DetalleOrdenDTO[] =[]
  productos:ProductoDTO[] = []

  constructor(@Inject(MAT_DIALOG_DATA) public data: ComboDTO,
              private productoComboService:ProductoComboService,
              private ProductoService:ProductoService,
              private ComboService:ComboService,
              private AlertService:AlertService,
              private dialog: MatDialog) 
  { 
    this.combo = data
  }

  ngOnInit() {
    this.obtenerProductosPorCombo();
    this.obtenerProductos();
  }

  obtenerProductosPorCombo(){
    this.productoComboService.getProductosPorCombo(this.combo.idCombo).subscribe(res=>{
      this.detalleOrden = res;
    })
  }

  obtenerProductos(){
    this.ProductoService.getProductos().subscribe(res => {
      this.productos = res;
    })
  }

  ionViewWillEnter(){
    console.log(this.combo)
  }

  agregarProducto(idProducto:number, precio: number, descripcion:string){
    const existeElemento = this.detalleOrden.some(detalle => detalle.idProducto === idProducto);
    if(!existeElemento){
      let aux:DetalleOrdenDTO = {
        idProducto,
        descripcion: descripcion,
        cantidad: 1,
        total: precio
      }
      this.detalleOrden.push(aux);

      let obj:ProductoComboDTO = {
        idProductoCombo: 0,
        idProducto,
        idCombo: this.combo.idCombo
      }

      this.productoComboService.postProductoCombo(obj).subscribe(res => {

      })
    }
  }

  eliminarDeOrden(idProducto?:number, idCombo?: number){
    if(idProducto){
      this.productoComboService.deletePorComboyProducto(idProducto,this.combo.idCombo).subscribe(res => {

      })
      this.detalleOrden = this.detalleOrden.filter(el => el.idProducto != idProducto);
    }
  }

  deshabilitarAgregarBtn(idProducto:number):boolean{
    return this.detalleOrden.some(x => x.idProducto == idProducto)
  }

  editarCombo(){
    this.ComboService.putCombo(this.combo).subscribe(res => {
      this.cerrarModal();
      this.AlertService.mostrarModal('Exito','Se edito exitosamente el combo')
    })
  }

  habilitarBtn():boolean{
    if(this.combo.descripcion === '' || this.combo.total === 0 || this.detalleOrden.length == 0){
      return true;
    }
    return false
  }

  cerrarModal(){
    this.dialog.closeAll();
  }

}