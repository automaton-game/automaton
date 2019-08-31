import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { JuegoResponse } from '../juego/modelos/juegoResponse';

@Injectable()
export class PartidaService {

	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
		
  }

  Get(id: number): Observable<JuegoResponse> {
    let subj = new Subject<JuegoResponse>();
    let subscription = this.http
      .get<JuegoResponse>("api/Partida/Get/" + id)
      .subscribe(response => {
        subj.next(response);
        subscription.unsubscribe();
      });

    return subj.asObservable();
  }
}
