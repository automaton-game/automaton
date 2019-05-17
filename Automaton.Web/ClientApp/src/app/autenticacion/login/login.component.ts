import { Component } from '@angular/core';
import { AutenticacionService } from '../autenticacion.service';
import { BsModalRef } from 'ngx-bootstrap';
import { LoginComponentTemplate } from './login.template';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LoginComponent extends LoginComponentTemplate {
  constructor(
    authService: AutenticacionService
  ) {
    super(authService, new BsModalRef());
  }
}
