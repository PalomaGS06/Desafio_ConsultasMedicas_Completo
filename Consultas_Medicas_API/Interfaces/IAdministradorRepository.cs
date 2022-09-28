using APIConsultasMedicas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace APIConsultasMedicas.Interfaces
{
    public interface IAdministradorRepository 
    {
        //Criação da interface com as funções que serão implementadas para o desenvolvedor admin

        //CREATE
        Administrador Inserir(Administrador admin);

        //SELECT
        ICollection<Administrador> ListarTodosAdmin();
        Administrador BuscarPorId(int id);

        //UPDATE
        void Alterar(Administrador admin);

        //UPDATE
        void AlterarParcialmente(JsonPatchDocument patchAdmin, Administrador admin);

        //DELETE
        void Excluir(Administrador admin);

    }
}
