import { Component, Inject, OnInit, Injectable, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { CasilleroTorneoModel } from './casilleroTorneo.model';

@Injectable()
export class TorneoService implements OnDestroy {
    

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

      let celdaVacia = new CasilleroTorneoModel();
      {
        let fila = [celdaVacia];

        let cabecera = jugadores.map(j => {
          let casillero = new CasilleroTorneoModel();
          casillero.texto = j;
          return casillero;
        });

        fila = fila.concat(cabecera);
        casilleros.push(fila);
      }
    }
    
    return casilleros;
  }

  ngOnDestroy(): void {
    throw new Error("Method not implemented.");
  }
}

class PartidoTorneoDto {
  public id?: number;
  public jugadores: Array<string>;
  public ganador: string;
  public porcentajeProgreso: number;
}
