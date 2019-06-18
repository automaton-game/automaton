import { Component, Input, OnInit } from '@angular/core';
import { FilaTablero } from '../modelos/filaTablero';
import { Tablero } from '../modelos/tablero';
import { JuegoResponse } from '../modelos/juegoResponse';
import { setTimeout } from 'timers';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'tablero-component',
  templateUrl: './tablero.component.html',
  styleUrls: ['./tablero.component.css']
})
export class TableroComponent implements OnInit {
    
  private _juegoResponse: JuegoResponse;

  public consola: Array<string>;
  public filas: Array<FilaTablero>;
  public actual: number;
  public ganador: string;
  public motivo: string;
  public animacion: Subscription;

  @Input()
  set juegoResponse(juegoResponse: JuegoResponse) {
    this._juegoResponse = juegoResponse;
    this.actual = 0;
    this.ganador = juegoResponse.ganador;
    this.motivo = juegoResponse.motivoDerrota;
    this.actualizarTablero();

    this.animacion = Observable.timer(1000, 200).subscribe(() => {
      if (this.incrementar()) {
        this.actualizarTablero();
      } else {
        this.animacion.unsubscribe();
      }
    });
  }

  get juegoResponse(): JuegoResponse { return this._juegoResponse; }

  ngOnInit(): void {
    
  }

  incrementar() {
    let max = this.juegoResponse.tableros.length - 1;
    if (this.actual >= max) {
      return false;
    } else {
      this.actual++;
      return true;
    }
  }

  selectorChange() {
    this.animacion.unsubscribe();
    this.actualizarTablero();
  }

  actualizarTablero() {
    this.filas = this.juegoResponse.tableros[this.actual].filas;
    this.consola = this.juegoResponse.tableros[this.actual].consola;
  }
}
