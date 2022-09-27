using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public TipoUsuarioRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }

        public void Alterar(TipoUsuario tipoUsuario)
        {
          
            // para fazer a alteração

            ctx.Update(tipoUsuario);
            ctx.SaveChanges();  // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario)
        {
            patchTipoUsuario.ApplyTo(tipoUsuario);   // aplicar o Patch no atributo tipoUsuario
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();  // salva as alterações
        }

        public TipoUsuario BuscarPorId(int id)
        {
            return ctx.TipoUsuario.Find(id); //procura pelo id
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Remove(tipoUsuario);   //remove o atributo no parâmetro da função Remove
            ctx.SaveChanges();  // salva as alterações
        }

        public TipoUsuario Inserir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Add(tipoUsuario);  // adiciona o que foi inserido dentro da entidade
            ctx.SaveChanges();  // salva as alterações
            return tipoUsuario;
        }

        public ICollection<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuario.ToList(); //Procura todos os tipos de usuários existentes
        }
    }
}
