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
  public error: { msg: string } = { msg: null };

  login() {
    this.authService.login(this.usuario).subscribe(value => {
      if (value.ok) {
        this.bsModalRef.hide();
      } else {
        this.error.msg = value.error;
      }
    });
  }

  ngOnInit(): void {
  }
}
