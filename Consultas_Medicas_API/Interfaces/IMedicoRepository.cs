using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IMedicoRepository
    {
        //Criação da interface com as funções que serão implementadas

        //CREATE
        Medico Inserir(Medico medico);

        //SELECT
        ICollection<Medico> ListarTodos();
        Medico BuscarPorId(int id);

        //UPDATE
        void Alterar(Medico medico);

        //DELETE
        void Excluir(Medico medico);

        //UPDATE
        void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico);
    }
}
