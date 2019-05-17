import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { LoginComponentTemplate } from './login/login.template';

@Injectable()
export class TokenService {

  private bsModalRef: BsModalRef;
  constructor(private modalService: BsModalService) { }

  public usuario: string;
  public token: string;

  loginModal() {
    this.bsModalRef = this.modalService.show(LoginComponentTemplate);
  }
} 
