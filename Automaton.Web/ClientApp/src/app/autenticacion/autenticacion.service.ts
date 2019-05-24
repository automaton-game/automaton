import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { LocalStorage } from 'ngx-webstorage';

@Injectable()
export class AutenticacionService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  @LocalStorage()
  public userData: { usuario: string, token: string };

  login(userName: string): Observable<{ok: boolean, error?: string }> {
    let observable = new Subject<{ ok: boolean, error?: string }>();
    this.http.post<{ token: string }>(this.baseUrl + 'api/Token/Create', { userName })
      .subscribe(response => {
        this.userData = { token: response.token, usuario: userName };
        observable.next({ ok: true });
      }, (err: HttpErrorResponse) => { observable.next({ ok: false, error: err.error.message }); });
    return observable.asObservable();
  }

  getToken() {
    if (this.userData) {
      return this.userData.token;
    }

    return null;
  }
} 
