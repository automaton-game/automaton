import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AutenticacionService } from '../autenticacion.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LoginComponent {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private authService: AutenticacionService,
  ) {

  }

  public usuario: string;

  login() {
    this.http.post<{ token: string}>(this.baseUrl + 'api/Token/Create', { userName: this.usuario })
      .subscribe(response => {
        this.authService.token = response.token;
      }, (err: HttpErrorResponse) => alert(err.error.errors.map(m => m.message)));
  }
}
