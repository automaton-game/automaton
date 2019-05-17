import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class AutenticacionService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  public usuario: string;
  public token: string;

  login(userName: string) {
    this.http.post<{ token: string }>(this.baseUrl + 'api/Token/Create', { userName })
      .subscribe(response => {
        this.token = response.token;
      }, (err: HttpErrorResponse) => alert(err.error.errors.map(m => m.message)));
  }
} 
