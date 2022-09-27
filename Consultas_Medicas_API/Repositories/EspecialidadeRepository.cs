using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public EspecialidadeRepository(ConsultaMedVetContext _ctx) //cria-se um método construtor
        {
            ctx = _ctx;
        }
        public void Alterar(Especialidade especialidade)
        {
            
            // para fazer a alteração
            ctx.Update(especialidade);
            ctx.SaveChanges(); // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade)
        {
            patchEspecialidade.ApplyTo(especialidade);  // aplicar o Patch no atributo especialidade
            ctx.Entry(especialidade).State = EntityState.Modified;  // mostra o estado da consulta e utiliza-se a função EntityState
                                                                    // para fazer a alteração
            ctx.SaveChanges(); // salva as alterações
        }

        public Especialidade BuscarPorId(int id)
        {
            return ctx.Especialidade.Find(id);  // procura pelo id
        }

        public void Excluir(Especialidade especialidade)
        {
            ctx.Especialidade.Remove(especialidade);   //remove o atributo no parâmetro da função Remove
            ctx.SaveChanges(); // salva as alterações
        }

        public Especialidade Inserir(Especialidade especialidade)
        {
            ctx.Especialidade.Add(especialidade);  // adiciona o que foi inserido dentro da entidade
            ctx.SaveChanges(); // salva as alterações
            return especialidade;  // retorna o resultado
        }

        public ICollection<Especialidade> ListarTodas()
        {
            var especialidade = ctx.Especialidade
               .Include(m => m.Medico) // inclui a classe Médico para ser exibido
                .ThenInclude(u => u.Usuario) // e inclui o Usuario, sendo seu id pertencente à classe Médico
               .ToList();

            return especialidade; // para listar todas as especialidades, é utilizada a biblioteca Linq
        }
    }
}
