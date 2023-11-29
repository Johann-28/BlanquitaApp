import { Component, OnInit } from '@angular/core';
import { ProductoDTO } from '../dtos/producto-dto';
import { ProductoService } from '../https/producto.service';
import { DetalleOrdenDTO } from '../dtos/detalle-orden-dto';
import { OrdenFormDTO } from '../dtos/orden-form-dto';
import { ComboService } from '../https/combo.service';
import { ComboDTO } from '../dtos/combo-dto';
import { OrdenService } from '../https/orden.service';

@Component({
  selector: 'app-orden',
  templateUrl: './orden.page.html',
  styleUrls: ['./orden.page.scss'],
})
export class OrdenPage implements OnInit {
  productos: ProductoDTO[] = [];
  combos:ComboDTO[] = [];
  detalleOrden: DetalleOrdenDTO[] = [];
  orden: OrdenFormDTO = {
    idUsuario: 1,
    total: 0,
    fecha: new Date()
  }

  constructor(private productoService:ProductoService,
              private comboService:ComboService,
              private ordenService:OrdenService) { }

  ngOnInit() {
    this.getProductos()
    this.getCombos();
  }

  getProductos(){
    this.productoService.getProductos().subscribe(res => {
      this.productos = res;
      console.log(this.productos);
    })
  }

  getCombos(){
    this.comboService.getCombos().subscribe(res => {
      this.combos = res;
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
  
  agregarCombo(idCombo:number, precio: number, descripcion:string){
    const existeElemento = this.detalleOrden.some(detalle => detalle.idCombo === idCombo);
    if(existeElemento){
      this.detalleOrden.forEach(elm => {
        if(elm.idCombo === idCombo ){
          elm.cantidad += 1;
          elm.total += precio
        }
      })
    }
    else{
      let aux:DetalleOrdenDTO = {
        idCombo,
        descripcion: descripcion,
        cantidad: 1,
        total: precio
      }
      this.detalleOrden.push(aux);
    }

    this.obtenerTotalOrden();
  }

  eliminarCombo(idCombo:number, precio: number, descripcion:string){
    const existeElemento = this.detalleOrden.some(detalle => detalle.idCombo === idCombo);
    if(existeElemento){
      this.detalleOrden.forEach(elm => {
        if(elm.idCombo === idCombo ){
          elm.cantidad -= 1;
          elm.total -= precio
        }

        if(elm.cantidad === 0){
          this.detalleOrden = this.detalleOrden.filter(el => el.idCombo != idCombo);
        }
      })

      this.obtenerTotalOrden();
    }
  }

  habilitarBtnMenosCombo (idCombo:number):boolean{
    return ! this.detalleOrden.some(el => el.idCombo === idCombo)
  } 

  obtenerTotalOrden(){
    this.orden.total = 0;

    this.detalleOrden.forEach(el => {
      this.orden.total += el.total;
    })
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

  enviarOrden(){
    let dia = new Date().getDate()
    let mes = new Date().getMonth()+1;
    let anio = new Date().getFullYear()
    this.orden.fecha = new Date(`${anio}-${mes}-${dia}`)

    this.ordenService.postCombo(this.orden).subscribe(res => {
      this.detalleOrden = []
      this.obtenerTotalOrden()
    })
  }

}
