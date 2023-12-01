import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private alertController:AlertController) { }

  public async mostrarModal(titulo:string, mensaje: string): Promise<void>{
    const alert = await this.alertController.create({
      header: titulo,
      message: mensaje,
      buttons: ['Ok']
    });

    await alert.present();
  }
}
