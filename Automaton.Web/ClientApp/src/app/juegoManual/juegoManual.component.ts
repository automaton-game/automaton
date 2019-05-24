import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { timer } from 'rxjs/observable/timer';
import { switchMap } from 'rxjs/operators';
import { Subscription } from 'rxjs/Subscription';

import { JuegoManualResponse } from './modelos/juegoManualResponse';
import { AccionRobot } from './modelos/accionRobot';

@Component({
  selector: 'app-juegoManual-component',
  templateUrl: './juegoManual.component.html',
})
export class JuegoManualComponent implements OnInit {
  private suscripcionRefresco: Subscription;
  // Set the http options
  private HTTP_OPTIONS = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
  };

  private _hubConnection: HubConnection;

  public juegoManualResponse: JuegoManualResponse;
  public ganador: string;
  public errores: string[];
  public idTablero: string;
  public idJugador: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router,) {
  }

  crearTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/CrearTablero', this.HTTP_OPTIONS )
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
        this.router.navigate(['.', this.idTablero], { relativeTo: this.route });
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/ObtenerTablero?idTablero=' + this.idTablero, this.HTTP_OPTIONS)
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        if (!this.idJugador) {
          this.idJugador = result.jugadores[0];
        }

        this.cambioJugador();
        
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }
  
  accionarTablero(accionRobot: AccionRobot) {
    const susc = this.http.post<JuegoManualResponse>(this.baseUrl + 'api/Tablero/AccionarTablero', { idTablero: this.idTablero, idJugador: this.idJugador, accionRobot: accionRobot  })
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        this.iniciarTimerRefresco();
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

 

  public cambioJugador() {
    const miTurno = this.juegoManualResponse.jugadorTurnoActual == this.idJugador;
    if (miTurno) {
      this.frenarTimer();
    } else {
      this.iniciarTimerRefresco();
    }
  }

  private frenarTimer() {
    if (this.suscripcionRefresco) {
      this.suscripcionRefresco.unsubscribe();
    }
  }

  private iniciarTimerRefresco() {
    if (this.suscripcionRefresco && !this.suscripcionRefresco.closed) {
      return;
    }
    this.suscripcionRefresco = timer(3000, 1000).subscribe(count => {
      this.obtenerTablero();
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(s => {
      const idTablero = s.get('id');
      if (idTablero && idTablero != this.idTablero) {
        this.idTablero = idTablero;
        this.obtenerTablero();
        this.connect();
      }
    });
  }

  private connect() {
    let connection = new HubConnectionBuilder()
      .withUrl("/turno")
      .build();

    connection.on("FinTurno", (idPartida: string, hashRobot: string) => {
      console.info(idPartida);
      this.obtenerTablero();
    });

    connection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
  }
}
