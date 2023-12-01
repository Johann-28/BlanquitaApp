import { Component, OnInit } from '@angular/core';
import { CorteCajaService } from '../https/corte-caja.service';
import { CorteCajaListadoDTO } from '../dtos/corte-caja-listado-dto';
import { ObtenerListadoFormDTO } from '../dtos/obtener-listado-form-dto';
import { CorteCajaFormDTO } from '../dtos/corte-caja-form-dto';
import { Form } from '@angular/forms';
import { SessionService } from '../https/session.service';
import { AlertService } from '../https/alert.service';

@Component({
  selector: 'app-corte-caja',
  templateUrl: './corte-caja.page.html',
  styleUrls: ['./corte-caja.page.scss'],
})
export class CorteCajaPage {
  listadoOrdenes:CorteCajaListadoDTO[] = [];
  displayedColumns: any[] = [
    {headerName:"Responsable", field: 'nombre'},
    {headerName:"Total", field: 'total'},
    {headerName:"Fecha", field: 'fecha'}
  ];
  defaultColDef = {sortable:true}

  consulta: ObtenerListadoFormDTO = {
    fecha : new Date()
  };

  dia = {
    0: "Domingo",
    1: 'Lunes',
    2: 'Martes',
    3: 'Miercoles',
    4: 'Jueves',
    5: "Viernes",
    6: "Sabado"
  }

  corteCaja:CorteCajaFormDTO = {
    idUsuario: 1,
    fecha:new Date(),
    comentarios : '',
    saldoFinal : 0,
    saldoInicial :0
  };

  constructor(private corteCajaService:CorteCajaService , private sessionService : SessionService,
              private AlertService:AlertService) { }

  /*ngOnInit() {
    this.obtenerListadoDeOrdenes()
    this.obtenerListadoSuma()
  }*/
  ionViewWillEnter(){
    this.obtenerListadoDeOrdenes()
    this.obtenerListadoSuma()
  }

  obtenerListadoDeOrdenes(){
    let dia = new Date().getDate()
    let mes = new Date().getMonth()+1;
    let anio = new Date().getFullYear()
    this.consulta.fecha = new Date(`${anio}-${mes}-${dia}`)
    
    //this.consulta.fecha.setDate(this.consulta.fecha.getDate())

    this.corteCajaService.getObtenerListado(this.consulta).subscribe(res => {
      this.listadoOrdenes = res;
    })
  }

  obtenerListadoSuma(){
    let dia = new Date().getDate()
    let mes = new Date().getMonth()+1;
    let anio = new Date().getFullYear()
    this.consulta.fecha = new Date(`${anio}-${mes}-${dia}`)

    this.corteCajaService.ObtenerListadoSuma(this.consulta).subscribe(res => {
      this.corteCaja.saldoInicial = res.total
    })
  }

  getDay(): string{
    let day =  new Date().getDay()
    return this.dia[day as keyof typeof this.dia]
  }

  getDate(): number{
    return new Date().getDate()
  }

  onSubmit(form: Form){
    let dia = new Date().getDate()
    let mes = new Date().getMonth()+1;
    let anio = new Date().getFullYear()
    this.corteCaja.fecha = new Date(`${anio}-${mes}-${dia}`)
    this.corteCajaService.postCorteCaja(this.corteCaja).subscribe(res => {
     
      this.corteCaja.idUsuario = this.getIdUsuario();
      this.corteCaja.fecha = new Date();
      this.corteCaja.comentarios = '';
      this.corteCajaService.ObtenerListadoSuma(this.consulta).subscribe(res => {
        this.corteCaja.saldoInicial = res.total
      })
      this.corteCaja.saldoFinal = 0;

      this.AlertService.mostrarModal('Exito','Se realizo el corte de caja de manera exitosa')
    })
  }

  getIdUsuario(){
    const idUsuario = this.sessionService.obtenerIdUsuarioSesion();
    if(idUsuario){
      return idUsuario;
    }
    else {
      return 0;
    }

  }

}
