using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class ConsultaTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnConsultaNotNull()
        {
            // Preparação
            Consulta consulta;

            // Execução
            consulta = new Consulta();

            // Retorno esperado
            Assert.NotNull(consulta);

        }
    }
}
