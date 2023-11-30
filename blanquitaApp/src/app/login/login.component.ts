import { Component, OnInit } from '@angular/core';
import { AuthService } from '../https/auth.service';
import { LoginRequestDto } from '../dtos/login-request-dto';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private alertController: AlertController
  ) {}

  protected loginRequest: LoginRequestDto = { correo: '', password: '' };
  protected modalAbierto: boolean = false;
  protected mensajeModal: string = '';

  ngOnInit() {}

  protected logout() {
    this.authService.logout();
  }

  protected login() {
    this.authService.login(this.loginRequest).subscribe({
      next: (data) => {},
      error: async (error) => {
        const alert = await this.alertController.create({
          header: 'Credenciales incorrectas',
          message: error.error.message,
          buttons: ['OK'],
        });

        await alert.present();
      },
      complete: () => {},
    });
  }

  protected cerrarModal() {
    this.modalAbierto = false;
  }
}
