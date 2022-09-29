using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class EspecialidadeTests
    {

        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnEspNotNull()
        {
            // Preparação
            Especialidade especialidade;

            // Execução
            especialidade = new Especialidade();

            // Retorno esperado
            Assert.NotNull(especialidade);

        }
    }
}
