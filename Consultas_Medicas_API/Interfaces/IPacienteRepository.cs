using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IPacienteRepository
    {
        //Criação da interface com as funções que serão implementadas

        //CREATE
        Paciente Inserir(Paciente paciente);
        //SELECT
        ICollection<Paciente> ListarTodos();
        Paciente BuscarPorId(int id);
        //UPDATE
        void Alterar(Paciente paciente);
        //DELETE
        void Excluir(Paciente paciente);
        //UPDATE
        void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente);
    }
}
