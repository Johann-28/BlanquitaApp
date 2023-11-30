import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { AuthService } from '../https/auth.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    
  ],
  declarations: [LoginComponent],
  providers : [AuthService],
  exports : [LoginComponent]
})
export class LoginModule { }
