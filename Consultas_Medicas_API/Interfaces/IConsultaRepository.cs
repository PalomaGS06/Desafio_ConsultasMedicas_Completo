using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IConsultaRepository
    {
        //Criação da interface com as funções que serão implementadas

        //CREATE
        Consulta Inserir(Consulta consultas); // Cadastrar/criar Consulta, função do tipo Consulta

        //SELECT
        ICollection<Consulta> ListarTodas(); // Lista da classe Consulta utilizada na função de busca,
                                             // função do tipo Enum ICollection
        Consulta BuscarPorId(int id);

        //UPDATE
        void Alterar(Consulta consultas);  // Alteração dos dados, função do tipo Consulta
        void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consultas);

        //DELETE
        void Excluir(Consulta consultas); // Função de exclusão, do tipo Consulta

    }
}
