import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class AutenticacionService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) { }

  public usuario: string;
  public token: string;

  login(userName: string): Observable<{ok: boolean, error?: string }> {
    let observable = new Subject<{ ok: boolean, error?: string }>();
    this.http.post<{ token: string }>(this.baseUrl + 'api/Token/Create', { userName })
      .subscribe(response => {
        this.token = response.token;
        observable.next({ ok: true });
      }, (err: HttpErrorResponse) => { observable.next({ ok: false, error: err.error.message }); });
    return observable.asObservable();
  }
} 
