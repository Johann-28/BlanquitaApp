import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComboDTO } from 'src/app/dtos/combo-dto';

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
  constructor(@Inject(MAT_DIALOG_DATA) public data: ComboDTO) 
  { 
    this.combo = data
  }

  ngOnInit() {
    console.log(this.combo)
  }

  ionViewWillEnter(){
    console.log(this.combo)
  }

}