using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public UsuarioRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }

        public void Alterar(Usuario usuario)
        {
             
            // para fazer a alteração

            ctx.Update(usuario);
            ctx.SaveChanges();  // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario)
        {
            patchUsuario.ApplyTo(usuario);    // aplicar o Patch no atributo usuario
            ctx.Entry(usuario).State = EntityState.Modified; // mostra o estado da consulta e utiliza-se a função EntityState
                                                             // para fazer a alteração
            ctx.SaveChanges();  // salva as alterações
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.Find(id); //busca o id
        }

        public void Excluir(Usuario usuario)
        {
            ctx.Usuarios.Remove(usuario);   //remove o atributo no parâmetro da função Remove
            ctx.SaveChanges();  // salva as alterações
        }

        public Usuario Inserir(Usuario usuario)
        {
            ctx.Usuarios.Add(usuario); // adiciona o novo usuario
            ctx.SaveChanges();  // salva as alterações
            return usuario;
        }

        public ICollection<Usuario> ListarTodosUsers()
        {
            return ctx.Usuarios.ToList(); // retorna a lista de todos os usuarios existentes
        }

        public ICollection<Usuario> ListarMedicosUsers()
        {
            var medicos = ctx.Usuarios
                   .Include(m => m.Medico) // inclui a classe Medico para ser exibida
                    .ThenInclude(es => es.Especialidade)  // inclui a classe Especialidade para ser exibida
                   .Where(m => m.IdTipoUsuario == 1) // quando o id do tipo de usuario for igual a 1 (que são os médicos)
                   .ToList();

            return medicos;
        }

        public ICollection<Usuario> ListarPacientesUsers()
        {
            var pacientes = ctx.Usuarios
                   .Include(p => p.Paciente) // inclui a classe Paciente para ser exibida
                   .Where(p => p.IdTipoUsuario == 2)  // quando o id do tipo de usuario for igual a 2 (que são os pacientes)
                   .ToList();

            return pacientes;
        }
    }
}
