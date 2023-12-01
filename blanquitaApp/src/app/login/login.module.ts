import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { AuthService } from '../https/auth.service';
import { FormsModule } from '@angular/forms';
import { LoginRoutingModule } from './login-routing.module';
import { IonicModule } from '@ionic/angular';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    LoginRoutingModule,
    IonicModule
    
  ],
  declarations: [LoginComponent],
  providers : [AuthService],
  exports : [LoginComponent]
})
export class LoginModule { }
