import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
  

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const apiReq = req.clone({ url: `https://tacosblanquitaapi.azurewebsites.net/api/${req.url}` });
    return next.handle(apiReq);
  }
}