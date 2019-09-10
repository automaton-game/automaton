import {  Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { CasilleroTorneoModel } from './casilleroTorneo.model';

@Injectable()
export class TorneoService {
    

	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
		
  }

  Get(): Observable<Array<Array<CasilleroTorneoModel>>> {
    let subj = new Subject<Array<Array<CasilleroTorneoModel>>>();
    this.http
      .get<{ partidos: Array<PartidoTorneoDto> }>("api/Torneo/Get")
      .subscribe(response => {
        let mapeado = this.Map(response.partidos);
        subj.next(mapeado);
      });

    return subj.asObservable();
  }

  Post(logica1: string) {
    let subj = new Subject<Array<Array<CasilleroTorneoModel>>>();
    this.http
      .post<void>("api/Torneo/Post", { logica: logica1 })
      .subscribe(() => {
        subj.next();
      });

    return subj.asObservable();
  }

  Map(dto: Array<PartidoTorneoDto>): Array<Array<CasilleroTorneoModel>> {
    let casilleros: Array<Array<CasilleroTorneoModel>> = new Array<Array<CasilleroTorneoModel>>();

    if (dto && dto.length > 0) {
      let jugadores = dto
        .map(m => m.jugadores)
        .reduce((a, b) => a.concat(b)) // SelectMany
        .filter((v, i, a) => a.indexOf(v) === i); // Distinct

      // Cabecera
      {
        let celdaCabera = new CasilleroTorneoModel();
        celdaCabera.texto = '';
        
        let fila = [celdaCabera];

        let cabecera = jugadores.map(j => {
          let casillero = new CasilleroTorneoModel();
          casillero.texto = j;
          return casillero;
        });

        fila = fila.concat(cabecera);
        casilleros.push(fila);
      }

      let celdaVacia = new CasilleroTorneoModel();
      jugadores.forEach(function (jugadorFila) {
        let fila = [];

        {
          let casillero = new CasilleroTorneoModel();
          casillero.texto = jugadorFila;
          fila.push(casillero);
        }

        jugadores.forEach(jugadorColumna => {
          let partido = dto
            .find(f =>
              f.jugadores[0] === jugadorFila &&
              f.jugadores
                .filter(j => j !== f.jugadores[0])
                .includes(jugadorColumna));
          if (partido) {
            let casillero = new CasilleroTorneoModel();
            casillero.texto = partido.ganador;
            casillero.idPartida = partido.id;
            casillero.progreso = partido.porcentajeProgreso;
            casillero.descripcion = partido.jugadores.join(' ← ');
            fila.push(casillero);
          } else {
            fila.push(celdaVacia);
          }
        });

        casilleros.push(fila);
      });
    }
    
    return casilleros;
  }
}

class PartidoTorneoDto {
  public id?: number;
  public jugadores: Array<string>;
  public ganador: string;
  public porcentajeProgreso: number;
}
