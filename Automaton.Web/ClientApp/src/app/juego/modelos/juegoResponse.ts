import { Tablero } from "./tablero";

export class JuegoResponse {
  public tableros: Tablero[];
  public ganador: string;
  public motivoDerrota: string;
}
