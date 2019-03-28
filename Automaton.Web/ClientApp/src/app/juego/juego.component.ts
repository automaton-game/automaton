import { Component, Inject, OnInit } from '@angular/core';
import { Tablero } from './modelos/tablero';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JuegoResponse } from './modelos/juegoResponse';

@Component({
  selector: 'app-juego-component',
  templateUrl: './juego.component.html',
  //styleUrls: ['./juego.component.css', '../../../node_modules/codemirror/lib/codemirror.css']
})
export class JuegoComponent implements OnInit {

  public juegoResponse: JuegoResponse;

  public logica1: string;
  public ganador: string;

  public errores: string[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  obtenerTablero() {
    this.http.post<JuegoResponse>(this.baseUrl + 'api/Tablero/GetTablero', { logicaRobot: this.logica1 })
      .subscribe(result => {
        this.juegoResponse = result;
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  ngOnInit(): void {
    const logica = `
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Contratos.Robots;
using System;
using System.Collections.Generic;

namespace Automaton.Robots
{
    public class RobotUsuario : IRobot
    {
        public Tablero Tablero { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            var casillero = this.GetPosition(Tablero);
            if (casillero.Muralla == null && casillero.Robots.Count == 1)
            {
                return new AccionConstruirDto() { };
            }

            var direcciones = new List<DireccionEnum>();

            var direccion = GenerarDireccionAleatoria(direcciones);
            var movimiento = EvaluarMovimiento(casillero, direccion);
            while (movimiento == null)
            {
                direcciones.Add(direccion);
                if (direcciones.Count >= 4)
                {
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
            if (relativo != null)
            {
                if (relativo.Muralla == null || relativo.Muralla == this)
                {
                    return new AccionMoverDto() { Direccion = direccion };
                }
            }

            return null;
        }

        private DireccionEnum GenerarDireccionAleatoria()
        {
            var random = new Random().Next(0,4);
            return (DireccionEnum)random;
        }

        private DireccionEnum GenerarDireccionAleatoria(IList<DireccionEnum> evitar)
        {
            var obtenido = GenerarDireccionAleatoria();
            while (evitar.Contains(obtenido))
            {
                obtenido = GenerarDireccionAleatoria();
            }

            return obtenido;
        }
    }
}

    `;

    this.logica1 = logica;
  }
}
