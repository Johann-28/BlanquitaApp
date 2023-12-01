import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../https/auth.service';
import { SessionService } from '../https/session.service';
import { Observable } from 'rxjs';
import { TitleService } from '../https/title.service';
import { PopoverController } from '@ionic/angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit
 {
  protected title: string;

  protected nombreUsuario: string = '';

  constructor(
    private authService: AuthService,
    private sessionService: SessionService,
    private titleService: TitleService,
    private popoverController : PopoverController
  ) {}
  ngOnInit() {
    this.getNombreUsuario();
    this.titleService.title$.subscribe((title) => {
      this.title = title;
    });
  }

  protected async logout() {
    this.authService.logout();
    await this.popoverController.dismiss();
  }

  protected getNombreUsuario() {
    const nombre = this.sessionService.obtenerNombreUsuarioSesion();
    this.nombreUsuario = nombre !== null ? nombre : '';
  }
}
