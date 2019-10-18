import {  Inject, Injectable, OnDestroy } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { CasilleroTorneoModel } from './casilleroTorneo.model';
import { SocketClientServiceFactory } from '../socketClientFactory.service';
import { SocketClientService } from '../socketClient.service';

@Injectable()
export class TorneoService implements OnDestroy {
   
  private socketConnection: SocketClientService;
  private subj = new Subject<Array<Array<CasilleroTorneoModel>>>();

  constructor(private http: HttpClient, private socketClientServiceFactory: SocketClientServiceFactory) {
  }

  Get(): Observable<Array<Array<CasilleroTorneoModel>>> {
    let self = this;

    this.http
      .get<{ partidos: Array<PartidoTorneoDto> }>("api/Torneo/Get")
      .subscribe(response => {
        self.Send(response);

        self.connect();
      });

    return this.subj.asObservable();
  }

  Post(logica1: string) {
    let subj = new Subject<Array<Array<CasilleroTorneoModel>>>();
    this.http
      .post<void>("api/Torneo/Post", { logica: logica1 })
      .subscribe(() => {
        subj.next();
      }, (err: HttpErrorResponse) => {
          if (err.error && err.error.errors) {
            subj.error(err.error.errors.map(m => m.message));
          }
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

  Send(respuesta: { partidos: Array<PartidoTorneoDto> }) {
    let mapeado = this.Map(respuesta.partidos);
    this.subj.next(mapeado);
  }

  ngOnDestroy(): void {
    this.socketConnection && this.socketConnection.ngOnDestroy();
  }

  private connect() {
    if (this.socketConnection) return;

    let self = this;
    this.socketConnection = this.socketClientServiceFactory.connect("/torneo");
    this.socketConnection.read<{ partidos: Array<PartidoTorneoDto> }>("NotificarUltimasPartidas").subscribe(rta => self.Send(rta));
  }
}

class PartidoTorneoDto {
  public id?: number;
  public jugadores: Array<string>;
  public ganador: string;
  public porcentajeProgreso: number;
}
