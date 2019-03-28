import { Component, Input } from '@angular/core';
import { FilaTablero } from '../modelos/filaTablero';
import { Tablero } from '../modelos/tablero';
import { JuegoResponse } from '../modelos/juegoResponse';

@Component({
  selector: 'tablero-component',
  templateUrl: './tablero.component.html',
  styleUrls: []
})
export class TableroComponent {
  private _juegoResponse: JuegoResponse;

  public filas: Array<FilaTablero>;
  public actual: number;
  public ganador: string;
  public motivo: string;

  @Input()
  set juegoResponse(juegoResponse: JuegoResponse) {
    this._juegoResponse = juegoResponse;
    this.actual = juegoResponse.tableros.length - 1;
    this.ganador = juegoResponse.ganador;
    this.motivo = juegoResponse.motivoDerrota;
    this.actualizarTablero();
  }

  get juegoResponse(): JuegoResponse { return this._juegoResponse; }

  actualizarTablero() {
    this.filas = this.juegoResponse.tableros[this.actual].filas;
  }
}
