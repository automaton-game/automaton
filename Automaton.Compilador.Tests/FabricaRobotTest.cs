using Automaton.Logica;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Automaton.Compilador.Tests
{
    public class FabricaRobotTest
    {
        IFabricaRobot fabrica;

        [Fact]
        public void FabricarRobotBase()
        {
            fabrica = new FabricaRobot(new TempFileManager(new Mock<ILogger<TempFileManager>>().Object), new DomainFactory(new Mock<ILogger<DomainFactory>>().Object), new MetadataFactory());

            var text = @"
                using Automaton.Contratos.Entorno;
                using Automaton.Contratos.Helpers;
                using Automaton.Contratos.Robots;
                using System;
                using System.Collections.Generic;

                namespace Automaton.Robots
                {
                    public class Robot: IRobot {
                        public Tablero Tablero { get; set; }

                        public AccionRobotDto GetAccionRobot()
                        {
                            return null;
                        }
                    }
                }";

            var objecto = fabrica.ObtenerRobot(text);
            Assert.NotNull(objecto);
        }

        [Fact]
        public void FabricarRobotCompleto()
        {
            fabrica = new FabricaRobot(new TempFileManager(new Mock<ILogger<TempFileManager>>().Object), new DomainFactory(new Mock<ILogger<DomainFactory>>().Object), new MetadataFactory());

            var text = @"
                using Automaton.Contratos.Entorno;
                using Automaton.Contratos.Helpers;
                using Automaton.Contratos.Robots;
                using System;
                using System.Collections.Generic;

                namespace Automaton.Robots
                {
                    public class RobotDefensivo : IRobot
                    {
                        public Tablero Tablero { get; set; }

                        public AccionRobotDto GetAccionRobot()
                        {
                            var casillero = this.GetPosition(Tablero);

                            if (casillero.Muralla == null)
                            {
                                return new AccionConstruirDto() { };
                            }

                            var direcciones = new List<DireccionEnum>();

                            var direccion = GenerarDireccionAleatoria(direcciones);
                            var movimiento = EvaluarMovimiento(casillero, direccion);
                            while (movimiento == null)
                            {
                                direcciones.Add(direccion);
                                if (direcciones.Count >= 4)
                                {
                                    return null;
                                }

                                direccion = GenerarDireccionAleatoria(direcciones);
                                movimiento = EvaluarMovimiento(casillero, direccion);
                            }

                            return movimiento;
                        }

                        private AccionMoverDto EvaluarMovimiento(Casillero casillero, DireccionEnum direccion)
                        {
                            var relativo = casillero.BuscarRelativo(direccion);
                            if (relativo != null)
                            {
                                if (relativo.Muralla == null && relativo.Robot == null)
                                {
                                    return new AccionMoverDto() { Direccion = direccion };
                                }
                            }

                            return null;
                        }

                        private DireccionEnum GenerarDireccionAleatoria()
                        {
                            var random = new Random().Next(0,4);
                            return (DireccionEnum)random;
                        }

                        private DireccionEnum GenerarDireccionAleatoria(IList<DireccionEnum> evitar)
                        {
                            var obtenido = GenerarDireccionAleatoria();
                            while (evitar.Contains(obtenido))
                            {
                                obtenido = GenerarDireccionAleatoria();
                            }

                            return obtenido;
                        }
                    }
                }
                ";

            var objecto = fabrica.ObtenerRobot(text);
            Assert.NotNull(objecto);
        }
    }
}
