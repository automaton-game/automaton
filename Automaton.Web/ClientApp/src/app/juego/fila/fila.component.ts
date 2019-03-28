import { Component, Input, OnInit } from '@angular/core';
import { FilaTablero } from '../modelos/filaTablero';
import { Casillero } from '../modelos/casillero';

@Component({
  selector: 'juego-fila-component',
  templateUrl: './fila.component.html',
  styleUrls: ['./fila.component.css']
})
export class FilaComponent implements OnInit {
  ngOnInit(): void {
    this.casilleros = this.datosFila.casilleros;
  }

  @Input() datosFila: FilaTablero;

  public casilleros: Array<Casillero>;

  constructor() {
    
  }


}
