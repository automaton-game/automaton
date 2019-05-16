import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
//import { AuthService } from './auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { AutenticacionService } from './autenticacion.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public auth: AutenticacionService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.token}`
      }
    });
    return next.handle(request);
  }
}
