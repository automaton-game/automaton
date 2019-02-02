import { Component, Inject, OnInit } from '@angular/core';
import { Tablero } from './modelos/tablero';
import { FilaTablero } from './modelos/filaTablero';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-juego-component',
  templateUrl: './juego.component.html',
  //styleUrls: ['./juego.component.css', '../../../node_modules/codemirror/lib/codemirror.css']
})
export class JuegoComponent implements OnInit {

  private tableros: Tablero[];

  public filas: Array<FilaTablero>;
  public max: number;
  public actual: number;

  public logica1: string;
  public logica2: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  actualizarTablero() {
    this.filas = this.tableros[this.actual].filas;
  }

  obtenerTablero() {
    this.http.post<Array<Tablero>>(this.baseUrl + 'api/Tablero/GetTablero', { logicasRobot: [this.logica1, this.logica2] })
      .subscribe(result => {
        this.max = result.length - 1;
        this.actual = this.max;

        this.tableros = result;
        this.actualizarTablero();
        
      });
  }

  ngOnInit(): void {
    const logica = `
    using AutomataNETjuegos.Contratos.Entorno;
    using AutomataNETjuegos.Contratos.Helpers;
    using AutomataNETjuegos.Contratos.Robots;
    using System;
    using System.Collections.Generic;

    namespace AutomataNETjuegos.Robots {
      public class RobotDefensivo : IRobot
      {
        public Tablero Tablero { get; set; }

        public AccionRobotDto GetAccionRobot()
{
  var casillero = this.GetPosition(Tablero);

  if (casillero.Muralla == null) {
    return new AccionConstruirDto() { };
  }

  var direcciones = new List<DireccionEnum>();

  var direccion = GenerarDireccionAleatoria(direcciones);
  var movimiento = EvaluarMovimiento(casillero, direccion);
  while (movimiento == null) {
    direcciones.Add(direccion);
    if (direcciones.Count >= 4) {
      return null;
    }

    direccion = GenerarDireccionAleatoria(direcciones);
    movimiento = EvaluarMovimiento(casillero, direccion);
  }

  return movimiento;
}

        private AccionMoverDto EvaluarMovimiento(Casillero casillero, DireccionEnum direccion)
{
  var relativo = casillero.BuscarRelativo(direccion);
  if (relativo != null) {
    if (relativo.Muralla == null && relativo.Robot == null) {
      return new AccionMoverDto() { Direccion = direccion };
    }
  }

  return null;
}

        private DireccionEnum GenerarDireccionAleatoria()
{
  var random = new Random().Next(0, 4);
  return (DireccionEnum)random;
}

        private DireccionEnum GenerarDireccionAleatoria(IList < DireccionEnum > evitar)
{
  var obtenido = GenerarDireccionAleatoria();
  while (evitar.Contains(obtenido)) {
    obtenido = GenerarDireccionAleatoria();
  }

  return obtenido;
}
    }
}

    `;

    this.logica1 = logica;
    this.logica2 = logica;
    
  }


  
}


