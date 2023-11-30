import { Component, OnInit } from '@angular/core';
import { SessionService } from '../https/session.service';

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: ['tabs.page.scss'],
})
export class TabsPage implements OnInit {
  protected esAdmin: boolean = false;

  constructor(private sessionService: SessionService) {}

  ngOnInit(): void {
    this.esAdmin = this.sessionService.esAdministrador();
  }
}
