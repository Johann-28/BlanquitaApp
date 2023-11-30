import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, Router } from '@angular/router';
import { SessionService } from '../https/session.service';

@Injectable({
    providedIn: 'root'
})
export class RoleGuard implements CanLoad {
    constructor(private sessionServce : SessionService, private router: Router) { }

    canLoad(route: Route, segments: UrlSegment[]): boolean {
        if (this.sessionServce.esAdministrador()) {
            return true;
        } else {
            return false;
        }
    }
}