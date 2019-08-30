import { Component, Inject, OnInit } from '@angular/core';
import { CasilleroTorneoModel } from '../services/casilleroTorneo.model';
import { TorneoService } from '../services/torneo.service';

@Component({
	selector: 'app-torneo-component',
  templateUrl: './torneo.component.html',
    styleUrls: ['./torneo.component.css'],
})
export class TorneoComponent implements OnInit {

  casillerosTorneo: Array<Array<CasilleroTorneoModel>> = [];

  constructor(private torneoService: TorneoService) {
		
  }

  cabecera() {
    return this.casillerosTorneo[0];
  }

  cuerpo() {
    return this.casillerosTorneo.filter(f => f !== this.cabecera());
  }

  ngOnInit(): void {
    this.torneoService.Get()
      .subscribe(data => this.casillerosTorneo = data);

      //this.casillerosTorneo = [
      //  [
      //    {
      //      texto: '',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'A',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'B',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'C',
      //      descripcion: '',
      //      progreso: null,
      //      idPartida: null
      //    }
      //  ],
      //  [
      //    {
      //      texto: 'A',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: '',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'B',
      //      descripcion: 'A > B',
      //      progreso: 30,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'A',
      //      descripcion: 'A > C',
      //      progreso: 0,
      //      idPartida: null
      //    }
      //  ],
      //  [
      //    {
      //      texto: 'B',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'A',
      //      descripcion: 'B > A',
      //      progreso: 100,
      //      idPartida: 4
      //    },
      //    {
      //      texto: '',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'C',
      //      descripcion: 'B > C',
      //      progreso: 50,
      //      idPartida: null
      //    }
      //  ],
      //  [
      //    {
      //      texto: 'C',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'A',
      //      descripcion: 'C > A',
      //      progreso: 25,
      //      idPartida: null
      //    },
      //    {
      //      texto: 'B',
      //      descripcion: 'C > B',
      //      progreso: 100,
      //      idPartida: 67
      //    },
      //    {
      //      texto: '',
      //      descripcion: null,
      //      progreso: null,
      //      idPartida: null
      //    }
      //  ]
      //];
  }
}

