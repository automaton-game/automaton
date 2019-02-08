import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JuegoManualResponse } from './modelos/juegoManualResponse';
import { AccionRobot } from './modelos/accionRobot';
import { timer } from 'rxjs/observable/timer';
import { switchMap } from 'rxjs/operators';
import { Subscription } from 'rxjs/Subscription';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-juegoManual-component',
  templateUrl: './juegoManual.component.html',
})
export class JuegoManualComponent implements OnInit {
  private suscripcionRefresco: Subscription;

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
    this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/CrearTablero', {} )
      .subscribe(result => {
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
        this.router.navigate(['.', this.idTablero], { relativeTo: this.route });
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/ObtenerTablero?idTablero=' + this.idTablero)
      .subscribe(result => {
        this.juegoManualResponse = result;
        if (!this.idJugador) {
          this.idJugador = result.jugadores[0];
          this.verificarTurnoJugadorActual();
        }
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }
  
  accionarTablero(accionRobot: AccionRobot) {
    this.http.post<JuegoManualResponse>(this.baseUrl + 'api/Tablero/AccionarTablero', { idTablero: this.idTablero, idJugador: this.idJugador, accionRobot: accionRobot  })
      .subscribe(result => {
        this.juegoManualResponse = result;
        this.verificarTurnoJugadorActual();
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

 

  public verificarTurnoJugadorActual() {
    const miTurno = this.juegoManualResponse.jugadorTurnoActual == this.idJugador;
    this.frenarRefresco();
    if (!miTurno) {
      this.suscripcionRefresco = this.iniciarTimerRefresco();
    }
  }

  private frenarRefresco() {
    if (this.suscripcionRefresco) {
      this.suscripcionRefresco.unsubscribe();
    }
  }

  private iniciarTimerRefresco() {
    const suscripcionRefresco = timer(3000, 3000).subscribe(count => { this.obtenerTablero(); });
    return suscripcionRefresco;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(s => {
      const idTablero = s.get('id');
      if (idTablero && idTablero != this.idTablero) {
        this.idTablero = idTablero;
        this.obtenerTablero();
      }
    });
  }
}
