import { Injectable } from "@angular/core";

Injectable()
export class ColorService {

  public getColor(hashId: string) {
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
