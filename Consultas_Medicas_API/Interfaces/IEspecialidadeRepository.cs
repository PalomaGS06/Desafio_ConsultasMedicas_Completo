using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IEspecialidadeRepository
    {
        //Criação da interface com as funções que serão implementadas

        //CREATE
        Especialidade Inserir(Especialidade especialidade);
        //SELECT
        ICollection<Especialidade> ListarTodas();
        Especialidade BuscarPorId(int id);
        //UPDATE
        void Alterar(Especialidade especialidade);
        //DELETE
        void Excluir(Especialidade especialidade);
        //UPDATE
        void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade);
    }
}
