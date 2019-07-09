import { Component, Input } from '@angular/core';
import { Casillero } from '../modelos/casillero';
import { ColorService } from '../../color.service';

@Component({
  selector: 'tablero-celda-component',
  templateUrl: './celda.component.html',
  styleUrls: ['./celda.component.css']
})
export class CeldaComponent {
  @Input() casillero: Casillero;

  constructor(private colorService: ColorService) {

  }

  public getColor(hashId: string) {
    var hashId = this.casillero.muralla || this.casillero.robotDuenio;
    return this.colorService.getColor(hashId);
  }

}
