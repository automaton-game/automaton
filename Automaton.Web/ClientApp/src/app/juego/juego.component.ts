import { Component, OnInit } from '@angular/core';
import { JuegoResponse } from './modelos/juegoResponse';
import { PartidaService } from '../services/partida.service';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-juego-component',
	templateUrl: './juego.component.html',
})
export class JuegoComponent implements OnInit {

	public juegoResponse: JuegoResponse;

  constructor(private partidaService: PartidaService, private activatedRoute: ActivatedRoute) {
		
	}

	Get(idPartida: number) {
      this.partidaService.Get(idPartida)
        .subscribe(data => this.juegoResponse = data);
	}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(s => this.Get(+s.get('id')));
	}
}
