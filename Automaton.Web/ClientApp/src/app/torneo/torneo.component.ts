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

  jugadores = 2;

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

      
  }
}

