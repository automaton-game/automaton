import { Component, Inject, OnInit } from '@angular/core';
import { TorneoService } from '../services/torneo.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-inscripcion-component',
  templateUrl: './inscripcion.component.html',
    styleUrls: ['./inscripcion.component.css'],
})
export class InscripcionComponent implements OnInit {

  public logica1: string;
  public errores: string[];
  public ocultarEditor: boolean = false; 

  constructor(
    private torneoService: TorneoService,
    private router: Router) {
  }

  enviarLogica() {
    this.torneoService.Post(this.logica1).subscribe(() =>
      this.router.navigate(["/torneo"]), (errors: Array<string> ) => {
        this.errores = errors;
      });
  }

  ngOnInit(): void {
    const logica = `    using Automaton.Contratos.Entorno;
		using Automaton.Contratos.Helpers;
		using Automaton.Contratos.Robots;
		
		/// <summary>
		/// El nombre de la clase es el nombre del usuario. 
		/// Por lo tanto siempre debes utilizar el mismo nombre en la creación de tu clase
		/// </summary>
		/// <example> MiRobotJose </example>
		public class MiRobotJose : ARobot
		{
				private int cont;

				///<summary>El metodo que resuelve el proximo movimiento que el robot debe ejecutar</summary>
				///<returns>Devuelve una accion según el estado actual del tablero.</returns>
				public override AccionRobotDto GetAccionRobot(IConsole console)
				{
						/// -------------------------------------
						/// Completar aqui con tu logica
						/// -------------------------------------

						/// -------------------------------------
						/// Prueba distintos ejemplos de movimiento --->


						
						return MovimientoEjemplo1(console);
						//return MovimientoEjemplo2(console);
						//return MovimientoEjemplo3(console);
				}

				private AccionRobotDto MovimientoEjemplo1(IConsole console)
				{
						console.WriteLine("Turno {0} de MovimientoEjemplo1", cont++);        

						// Obtengo el casillero donde estoy parado
						var casillero = this.GetPosition();

						// Verifico si ya construi una muralla. Sino envio la orden de construir una
						if (casillero.Muralla == null && casillero.Robots.Count == 1)
						{
								return new AccionConstruirDto() { };
						}

						//Si tengo lugar, intento moverme para arriba
						if (this.BuscarRelativo(DireccionEnum.Arriba) != null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Arriba };
						}
						// Sino, intento moverme para abajo
						else if (this.BuscarRelativo(DireccionEnum.Abajo) != null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Abajo };
						}

						// Si no se que hacer, devuelvo nulo. Y el juego termina.
						return null;
				}

				private AccionRobotDto MovimientoEjemplo2(IConsole console)
				{
						console.WriteLine("Turno {0} de MovimientoEjemplo2", cont++);  

						// Obtengo el casillero donde estoy parado
						var casillero = this.GetPosition();

						// Verifico si ya construi una muralla. Sino envio la orden de construir una
						if (casillero.Muralla == null && casillero.Robots.Count == 1)
						{
								return new AccionConstruirDto() { };
						}

						//Si tengo lugar, intento moverme para arriba
						if (this.BuscarRelativo(DireccionEnum.Arriba) != null && this.BuscarRelativo(DireccionEnum.Arriba).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Arriba };
						}
						// Sino, intento moverme para abajo
						else if (this.BuscarRelativo(DireccionEnum.Abajo) != null && this.BuscarRelativo(DireccionEnum.Abajo).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Abajo };
						}

						// Si no se que hacer, devuelvo nulo. Y el juego termina.
						return null;
				}

				private AccionRobotDto MovimientoEjemplo3(IConsole console)
				{
						console.WriteLine("Turno {0} de MovimientoEjemplo3", cont++);

						// Obtengo el casillero donde estoy parado
						var casillero = this.GetPosition();

						// Verifico si ya construi una muralla. Sino envio la orden de construir una
						if (casillero.Muralla == null && casillero.Robots.Count == 1)
						{
								return new AccionConstruirDto() { };
						}

						//Si tengo lugar, intento moverme para arriba
						if (this.BuscarRelativo(DireccionEnum.Arriba) != null && this.BuscarRelativo(DireccionEnum.Arriba).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Arriba };
						}
						// Sino, intento moverme para abajo
						else if (this.BuscarRelativo(DireccionEnum.Abajo) != null && this.BuscarRelativo(DireccionEnum.Abajo).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Abajo };
						}
						// Sino, intento moverme para abajo
						else if (this.BuscarRelativo(DireccionEnum.Izquierda) != null && this.BuscarRelativo(DireccionEnum.Izquierda).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Izquierda };
						}
						// Sino, intento moverme para abajo
						else if (this.BuscarRelativo(DireccionEnum.Derecha) != null && this.BuscarRelativo(DireccionEnum.Derecha).Muralla == null)
						{
								return new AccionMoverDto { Direccion = DireccionEnum.Derecha };
						}


						// Si no se que hacer, devuelvo nulo. Y el juego termina.
						return null;
				}
		}`;

    this.logica1 = logica;
  }
}
