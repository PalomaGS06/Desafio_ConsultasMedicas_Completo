using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class MedicoTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnMedicoNotNull()
        {
            // Preparação
            Medico medico;

            // Execução
            medico = new Medico();

            // Retorno esperado
            Assert.NotNull(medico);

        }
    }
}
