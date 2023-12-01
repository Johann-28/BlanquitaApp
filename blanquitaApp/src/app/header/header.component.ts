import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../https/auth.service';
import { SessionService } from '../https/session.service';
import { Observable } from 'rxjs';
import { TitleService } from '../https/title.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  protected title: string;

  protected nombreUsuario: string = '';

  constructor(
    private authService: AuthService,
    private sessionService: SessionService,
    private titleService: TitleService
  ) {}
  ngOnInit() {
    this.getNombreUsuario();
    this.titleService.title$.subscribe((title) => {
      this.title = title;
    });
  }

  protected logout() {
    this.authService.logout();
  }

  protected getNombreUsuario() {
    const nombre = this.sessionService.obtenerNombreUsuarioSesion();
    this.nombreUsuario = nombre !== null ? nombre : '';
  }
}
