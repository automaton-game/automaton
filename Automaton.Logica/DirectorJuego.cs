using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class DirectorJuego
    {
        private readonly IJuego2v2 juego;
        private readonly IPlataformaVisual plataformaVisual;
        private bool estadoActivo = false;
        private bool iniciado = false;

        public DirectorJuego(
            IJuego2v2 juego,
            IPlataformaVisual plataformaVisual )
        {
            this.juego = juego;
            this.plataformaVisual = plataformaVisual;
        }

        public async Task JugarTurno(int turnos = 0)
        {
            ProbarInicio();

            estadoActivo = true; 

            var cont = 0;
            while ((turnos == 0 || cont++ < turnos) && estadoActivo)
            {
                estadoActivo = !await SiguienteAsync();
            }
        }

        public void Pausar()
        {
            estadoActivo = false;
        }

        private bool Siguiente()
        {
            try
            {
                var resultado = this.juego.JugarTurno() == null;
                plataformaVisual.Dibujar(this.juego.Tablero);
                return resultado;
            }
            catch (System.Exception ex)
            {
                plataformaVisual.Consola(ex.Message);
                return false;
            }
        }

        private async Task<bool> SiguienteAsync()
        {
            await Task.Delay(1000);
            return Siguiente();
        }

        private void ProbarInicio()
        {
            if (!iniciado)
            {
                iniciado = true;
                //this.juego.Iniciar();
                plataformaVisual.Dibujar(this.juego.Tablero);
            }
        }
    }
}
