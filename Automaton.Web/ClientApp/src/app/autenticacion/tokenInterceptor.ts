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
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { LoginComponentTemplate } from './login/login.template';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  private bsModalRef: BsModalRef;

  constructor(
    private auth: AutenticacionService,
    private modalService: BsModalService,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //this.bsModalRef = this.modalService.show(LoginComponentTemplate);
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.token}`
      }
    });
    return next.handle(request);
  }
}
