using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public ConsultaRepository(ConsultaMedVetContext _ctx) //método construtor gerado
        {
            ctx = _ctx;
        }

        public void Alterar(Consulta consultas)
        {
            
            // para fazer a alteração
            ctx.Update(consultas);
            ctx.SaveChanges(); // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consultas)
        {

            patchConsulta.ApplyTo(consultas); // aplicar o Patch no atributo consultas
            ctx.Entry(consultas).State = EntityState.Modified; // mostra o estado da consulta e utiliza-se a função EntityState
                                                               // para fazer a alteração
            ctx.SaveChanges();// salva as alterações
        }

        public Consulta BuscarPorId(int id)
        {
            return ctx.Consultas.Find(id); // procura pelo id
        }

        public void Excluir(Consulta consultas)
        {
            ctx.Consultas.Remove(consultas);     //remove o atributo no parâmetro da função Remove
            ctx.SaveChanges();  // salva as alterações
        }

        public Consulta Inserir(Consulta consultas)
        {
            ctx.Consultas.Add(consultas);    // adiciona dentro da entidade
            ctx.SaveChanges();   // salva as alterações
            return consultas;   // retorna o resultado
        }

        public ICollection<Consulta> ListarTodas()
        {
            var consultas = ctx.Consultas   
                .Include(p => p.Paciente)  // inclui o Paciente para ser exibido
                    .ThenInclude(u => u.Usuario)
                        .ThenInclude(tu => tu.TipoUsuario)
                .Include(m => m.Medico)    // inclui o Médico para ser exibido
                    .ThenInclude(u => u.Usuario)
                        .ThenInclude(tu => tu.TipoUsuario)
                .Include(m => m.Medico)
                  .ThenInclude(e => e.Especialidade)
                .ToList();  //lista todos as consultas com os dados dos pacientes e médicos

            return consultas;    // retorna o resultado
        }
    }
}
