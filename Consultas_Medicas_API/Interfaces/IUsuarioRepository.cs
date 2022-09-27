using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface IUsuarioRepository
    {
        //Criação da interface com as funções que serão implementadas

        //CREATE
        Usuario Inserir(Usuario usuario);

        //SELECT
        ICollection<Usuario> ListarTodosUsers();
        ICollection<Usuario> ListarMedicosUsers();
        ICollection<Usuario> ListarPacientesUsers();
        Usuario BuscarPorId(int id);

        //UPDATE
        void Alterar(Usuario usuario);
        void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario);

        //DELETE
        void Excluir(Usuario usuario);
  
    }
}
