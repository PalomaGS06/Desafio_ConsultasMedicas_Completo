using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace APIConsultasMedicas.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        public void Alterar(Administrador admin)
        {
            throw new System.NotImplementedException();
        }

        public void AlterarParcialmente(JsonPatchDocument patchAdmin, Administrador admin)
        {
            throw new System.NotImplementedException();
        }

        public Administrador BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(Administrador admin)
        {
            throw new System.NotImplementedException();
        }

        public Administrador Inserir(Administrador admin)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Administrador> ListarTodosAdmin()
        {
            throw new System.NotImplementedException();
        }
    }
}
