import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { JuegoManualResponse } from './modelos/juegoManualResponse';
import { AccionRobot } from './modelos/accionRobot';

@Component({
  selector: 'app-juegoManual-component',
  templateUrl: './juegoManual.component.html',
})
export class JuegoManualComponent implements OnInit, OnDestroy {

  // Set the http options
  private HTTP_OPTIONS = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
  };

  private _hubConnection: HubConnection;
  private urlPrefix: string; 

  public juegoManualResponse: JuegoManualResponse;
  public ganador: string;
  public errores: string[];
  public idTablero: string;
  public idJugador: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router, ) {

    this.urlPrefix = this.baseUrl + 'api/JuegoManual/';
  }

  crearTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.urlPrefix + 'CrearTablero', this.HTTP_OPTIONS )
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
        this.router.navigate(['.', this.idTablero], { relativeTo: this.route });
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.urlPrefix + 'ObtenerTablero?idTablero=' + this.idTablero, this.HTTP_OPTIONS)
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        if (!this.idJugador) {
          this.idJugador = result.jugadores[0];
        }        
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }
  
  accionarTablero(accionRobot: AccionRobot) {
    const susc = this.http.post<JuegoManualResponse>(this.urlPrefix + 'AccionarTablero', { idTablero: this.idTablero, idJugador: this.idJugador, accionRobot: accionRobot  })
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
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

  ngOnDestroy() {
    if (this._hubConnection) {
      this._hubConnection.stop();
    }
  }

  private connect() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("/turno")
      .build();

    this._hubConnection.on("FinTurno", (idPartida: string, hashRobot: string) => {
      console.info(idPartida);
      this.obtenerTablero();
    });

    this._hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
  }
}
