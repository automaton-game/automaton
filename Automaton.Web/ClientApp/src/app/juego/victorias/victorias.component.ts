import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { PodiumModel } from '../../podium/podium.model';

@Component({
	selector: 'victorias-juego-component',
	templateUrl: './victorias.component.html',
})
export class VictoriasComponent implements OnInit {

	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
		
	}

  podio: PodiumModel;
  jugadores: Array<JugadorModel>;

  ngOnInit(): void {
    this.http.get<Array<JugadorModel>>(this.baseUrl + 'api/RegistroRobot/Get')
      .subscribe(result => {
        this.jugadores = result;
        this.podio = new PodiumModel();

        if (this.jugadores[0]) {
          this.podio.first = this.jugadores[0].key + 'asdasdsad';
        }

        if (this.jugadores[1]) {
          this.podio.second = this.jugadores[1].key;
        }

        if (this.jugadores[2]) {
          this.podio.third = this.jugadores[2].key;
        }
      }, (err: HttpErrorResponse) => {
          alert(err.message);
      });

  }
}

class JugadorModel {
  key: string;
  value: number;
}
