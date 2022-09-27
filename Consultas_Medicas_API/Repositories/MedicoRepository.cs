using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {

        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public MedicoRepository(ConsultaMedVetContext _ctx) //método construtor criado
        {
            ctx = _ctx;
        }


        public void Alterar(Medico medico)
        {
            
            // para fazer a alteração
            ctx.Update(medico);
            ctx.SaveChanges();  // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico)
        {
            patchMedico.ApplyTo(medico);   // aplicar o Patch no atributo medico
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();  // salva as alterações
        }

        public Medico BuscarPorId(int id)
        {
            var medicoId = ctx.Medico
               .Include(e => e.Especialidade) //inclui o campo IdEspecialidade
               .Include(u => u.Usuario)   //inclui o campo  IdUsuario
               .FirstOrDefault(m => m.Id == id);

            return medicoId;
        }

        public void Excluir(Medico medico)
        {
            ctx.Medico.Remove(medico);    //remove o atributo no parâmetro da função Remove
            var usuarioM = ctx.Usuarios.Find(medico.IdUsuario); // a função Find vai procurar em qual
                                                                // usuario o medico selecionado para a exclusão está
            ctx.Usuarios.Remove(usuarioM); //remove o usuario medico
            ctx.SaveChanges();  // salva as alterações
        }

        public Medico Inserir(Medico medico)
        {
            ctx.Medico.Add(medico); // adiciona o que foi inserido dentro da entidade
            ctx.SaveChanges();  // salva as alterações
            return medico;  // retorna o resultado
        }

        public ICollection<Medico> ListarTodos()
        {
            var consultas = ctx.Medico
                    .Include(c => c.Consulta) // inclui a classe Consulta para ser exibida
                    .Include(u => u.Usuario)  // inclui a classe Usuario para ser exibido
                    .ToList(); // lista os itens existentes

            return consultas;
        }
    }
}
