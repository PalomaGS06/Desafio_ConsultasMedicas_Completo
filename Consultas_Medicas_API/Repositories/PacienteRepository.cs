using ConsultaMedicaVet.Contexts;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaMedicaVet.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        // Injeção de Dependência
        ConsultaMedVetContext ctx; //criando a dependência de contexto
        public PacienteRepository(ConsultaMedVetContext _ctx) //método construtor
        {
            ctx = _ctx;
        }
        public void Alterar(Paciente paciente)
        {

            // para fazer a alteração

            ctx.Update(paciente);
            ctx.SaveChanges();  // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente)
        {
            patchPaciente.ApplyTo(paciente);    // aplicar o Patch no atributo paciente
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();  // salva as alterações
        }

        public Paciente BuscarPorId(int id)
        {
            var pacienteId = ctx.Paciente 
               .Include(u => u.Usuario)   //inclui o campo  IdUsuario
               .FirstOrDefault(m => m.Id == id);

            return pacienteId;
        }

        public void Excluir(Paciente paciente)
        {
            ctx.Paciente.Remove(paciente);    //remove o atributo no parâmetro da função Remove
            var usuarioP = ctx.Usuarios.Find(paciente.IdUsuario); // a função Find vai procurar em qual
                                                                  // usuario o paciente selecionado para a exclusão está
            ctx.Usuarios.Remove(usuarioP);  //remove o usuario paciente
            ctx.SaveChanges();  // salva as alterações
        }

        public Paciente Inserir(Paciente paciente)
        {
            ctx.Paciente.Add(paciente);   // adiciona o que foi inserido dentro da entidade
            ctx.SaveChanges();  // salva as alterações
            return paciente;
        }

        public ICollection<Paciente> ListarTodos()
        {
            var consultas = ctx.Paciente 
                   .Include(c => c.Consulta)  // inclui a classe Consulta para ser exibida
                   .Include(u => u.Usuario)  // inclui a classe Usuario para ser exibido
                   .ToList();

            return consultas;
        }
    }
}
