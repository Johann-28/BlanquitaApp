import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RedirectComponent } from './redirect.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [RedirectComponent],
  exports: [RedirectComponent]
})
export class RedirectModule { }
