import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
	selector: 'victorias-juego-component',
  templateUrl: './victorias.component.html',
  styleUrls: ['./victorias.component.css']
})
export class VictoriasComponent implements OnInit {

	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
		
	}

  podio: Array<string>;
  jugadores: Array<JugadorModel>;

  ngOnInit(): void {
    this.http.get<Array<JugadorModel>>(this.baseUrl + 'api/RegistroRobot/Get')
      .subscribe(result => {
        this.jugadores = result;
        this.podio = result.map(m => m.key);

      }, (err: HttpErrorResponse) => {
          alert(err.message);
      });

  }

  borrar() {
    this.http.post<void>(this.baseUrl + 'api/RegistroRobot/BorrarTodo', {})
      .subscribe(r => this.ngOnInit());
  }
}

class JugadorModel {
  key: string;
  value: number;
}
