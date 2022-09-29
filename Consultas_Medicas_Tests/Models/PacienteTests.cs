using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class PacienteTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnPacienteNotNull()
        {
            // Preparação
            Paciente paciente;

            // Execução
            paciente = new Paciente();

            // Retorno esperado
            Assert.NotNull(paciente);

        }
    }
}
