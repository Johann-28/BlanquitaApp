import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { CorteCajaPageRoutingModule } from './corte-caja-routing.module';

import { CorteCajaPage } from './corte-caja.page';
import { MatTableModule } from '@angular/material/table';
import { AgGridModule } from 'ag-grid-angular';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CorteCajaPageRoutingModule,
    MatTableModule,
    AgGridModule
  ],
  declarations: [CorteCajaPage]
})
export class CorteCajaPageModule {}
