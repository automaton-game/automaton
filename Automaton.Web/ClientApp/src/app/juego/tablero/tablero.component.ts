import { Component, Input, OnInit } from '@angular/core';
import { FilaTablero } from '../modelos/filaTablero';
import { Tablero } from '../modelos/tablero';
import { JuegoResponse } from '../modelos/juegoResponse';
import { setTimeout } from 'timers';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { ColorService } from '../../color.service';

@Component({
  selector: 'tablero-component',
  templateUrl: './tablero.component.html',
  styleUrls: ['./tablero.component.css']
})
export class TableroComponent implements OnInit {
    
  private _juegoResponse: JuegoResponse;

  public consola: Array<string>;
  public filas: Array<FilaTablero>;
  public tablero: Tablero;
  public colorTablero: string;
  public actual: number;
  public ganador: string;
  public motivos: Array<string>;
  public animacion: Subscription;

  @Input()
  public mostrarAnimacion: boolean;

  @Input()
  set juegoResponse(juegoResponse: JuegoResponse) {
    this._juegoResponse = juegoResponse;
    this.actual = 0;
    this.ganador = juegoResponse.ganador;
    this.motivos = juegoResponse.motivoDerrota.split('\n');

    if (!this.juegoResponse.tableros) {
      return;
    }

    this.actualizarTablero();

    if (this.mostrarAnimacion) {
      this.animacion = Observable.timer(1000, 200).subscribe(() => {
        if (this.incrementar()) {
          this.actualizarTablero();
        } else {
          this.animacion.unsubscribe();
        }
      });
    } else {
      this.actual = this.juegoResponse.tableros.length - 1;
      this.actualizarTablero();
    }
  }

  get juegoResponse(): JuegoResponse { return this._juegoResponse; }

  constructor(private colorService: ColorService) {

  }

  public getColor(hashId: string) {
    return this.colorService.getColor(hashId);
    
  }

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
    if (this.animacion) {
      this.animacion.unsubscribe();
    }
    
    this.actualizarTablero();
  }

  actualizarTablero() {
    if (this.juegoResponse.tableros.length <= 0) {
      return;
    }

    this.tablero = this.juegoResponse.tableros[this.actual];
    if (this.tablero.turnoRobot) {
      this.colorTablero = this.colorService.getColor(this.tablero.turnoRobot);
    } else {
      this.colorTablero = "#000";
    }
    
    this.filas = this.tablero.filas;
    this.consola = this.tablero.consola;
  }
}
