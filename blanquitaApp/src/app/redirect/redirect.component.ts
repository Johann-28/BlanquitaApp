import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../https/auth.service';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css'],
})
export class RedirectComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['tabs']);
    } else {
      this.router.navigate(['login']);
    }
  }
}
