import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../https/auth.service';
import { SessionService } from '../https/session.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  
  protected nombreUsuario : string = '';
  
  constructor(private authService : AuthService , private sessionService : SessionService) { 

  }
  ngOnInit() {
    this.getNombreUsuario();
  }

  protected logout(){
    this.authService.logout();
  }

  protected getNombreUsuario(){
    const nombre = this.sessionService.obtenerNombreUsuarioSesion();
    this.nombreUsuario = nombre !== null ? nombre : '';
  }

  
}
