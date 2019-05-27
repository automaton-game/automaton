import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';



import { JuegoManualResponse } from './modelos/juegoManualResponse';
import { AccionRobot } from './modelos/accionRobot';
import { SocketClientServiceFactory } from '../socketClientFactory.service';
import { SocketClientService } from '../socketClient.service';

@Component({
  selector: 'app-juegoManual-component',
  templateUrl: './juegoManual.component.html',
})
export class JuegoManualComponent implements OnInit, OnDestroy {

  private socketConnection: SocketClientService;

  // Set the http options
  private HTTP_OPTIONS = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
  };

  
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
    private router: Router,
    private socketClientServiceFactory: SocketClientServiceFactory
  ) {

    this.urlPrefix = this.baseUrl + 'api/JuegoManual/';
  }

  crearTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.urlPrefix + 'CrearTablero', this.HTTP_OPTIONS )
      .subscribe(result => {
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
        this.router.navigate(['.', this.idTablero], { relativeTo: this.route });
        susc.unsubscribe();
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.urlPrefix + 'ObtenerTablero?idTablero=' + this.idTablero, this.HTTP_OPTIONS)
      .subscribe(result => {
        this.juegoManualResponse = result;
        if (!this.idJugador) {
          this.idJugador = result.jugadores[0];
        }
        susc.unsubscribe();
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }
  
  accionarTablero(accionRobot: AccionRobot) {
    const susc = this.http.post<JuegoManualResponse>(this.urlPrefix + 'AccionarTablero', { idTablero: this.idTablero, idJugador: this.idJugador, accionRobot: accionRobot  })
      .subscribe(result => {
        this.juegoManualResponse = result;
        susc.unsubscribe();
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
    if (this.socketConnection) {
      this.socketConnection.ngOnDestroy();
    }
  }

  private connect() {
    this.socketConnection = this.socketClientServiceFactory.connect("/turno");

    this.socketConnection.read<{ juego: JuegoManualResponse, hashRobot: string }>().subscribe(resp => {
      if (this.idTablero == resp.juego.idTablero && this.idJugador != resp.hashRobot) {
        this.juegoManualResponse = resp.juego;
      }
    });
  }
}
