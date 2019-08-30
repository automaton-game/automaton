using System;

namespace Automaton.Logica.Factories
{
    public class JuegoFactory : IJuegoFactory
    {
        private readonly Func<IJuego2v2> factory;

        public JuegoFactory(Func<IJuego2v2> factory)
        {
            this.factory = factory;
        }

        public IJuego2v2 CreateJuego2V2()
        {
            return factory();
        }
    }
}
