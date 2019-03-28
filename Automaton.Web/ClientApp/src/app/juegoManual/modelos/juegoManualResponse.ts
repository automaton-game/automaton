import { JuegoResponse } from "../../juego/modelos/juegoResponse";

export class JuegoManualResponse extends JuegoResponse {
  public jugadores: Array<string>;
  public jugadorTurnoActual: string;
  public idTablero: string;
}
