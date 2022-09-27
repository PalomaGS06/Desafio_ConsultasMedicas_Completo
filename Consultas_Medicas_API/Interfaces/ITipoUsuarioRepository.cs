using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ConsultaMedicaVet.Interfaces
{
    public interface ITipoUsuarioRepository
    {  
        //Criação da interface com as funções que serão implementadas

        //CREATE
        TipoUsuario Inserir(TipoUsuario tipoUsuario);
        //SELECT
        ICollection<TipoUsuario> ListarTodos();
        TipoUsuario BuscarPorId(int id);
        //UPDATE
        void Alterar(TipoUsuario tipoUsuario);
        //DELETE
        void Excluir(TipoUsuario tipoUsuario);
        //UPDATE
        void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario);

    }
}
