import { Component, OnInit } from '@angular/core';
import { AutenticacionService } from '../autenticacion.service';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LoginComponentTemplate implements OnInit {
  constructor(
    private authService: AutenticacionService,
    public bsModalRef: BsModalRef, 
  ) { }

  public usuario: string;

  login() {
    this.authService.login(this.usuario);
  }

  ngOnInit(): void {
  }
}
