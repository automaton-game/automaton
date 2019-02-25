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
    const susc = this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/CrearTablero', {} )
      .subscribe(result => {
        susc.unsubscribe();
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
        this.router.navigate(['.', this.idTablero], { relativeTo: this.route });
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    const susc = this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/ObtenerTablero?idTablero=' + this.idTablero)
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
      }
    });
  }
}
