import { Component, Input } from '@angular/core';
import { Casillero } from '../modelos/casillero';

@Component({
  selector: 'tablero-celda-component',
  templateUrl: './celda.component.html',
  styleUrls: ['./celda.component.css']
})
export class CeldaComponent {
  @Input() casillero: Casillero;

  public getColor(hashId: string) {
    var hashId = this.casillero.muralla || this.casillero.robotDuenio;
    if (hashId) {
      return "#" + this.intToRGB(this.hashCode(hashId));
    } else {
      return "#FFF";
    }
  }

  private hashCode(str) { // java String#hashCode
    var hash = 0;
    for (var i = 0; i < str.length; i++) {
      hash = str.charCodeAt(i) + ((hash << 5) - hash);
    }
    return hash;
  }

  private intToRGB(i) {
    var c = (i & 0x00FFFFFF)
      .toString(16)
      .toUpperCase();

    return "00000".substring(0, 6 - c.length) + c;
  }
}
