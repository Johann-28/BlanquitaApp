import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { MatTableModule } from '@angular/material/table';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { BaseUrlInterceptor } from './auth/interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from './https/auth.service';
import { HeaderModule } from './header/header.module';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, IonicModule.forRoot(), AppRoutingModule , HttpClientModule, MatTableModule, BrowserAnimationsModule, HeaderModule,MatSelectModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
