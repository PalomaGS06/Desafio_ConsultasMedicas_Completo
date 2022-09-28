using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using ConsultaMedicaVet.Contexts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIConsultasMedicas.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        // Injeção de Dependência de contexto
        ConsultaMedVetContext _context; //criando a dependência de contexto
        public AdministradorRepository(ConsultaMedVetContext ctx) //método construtor criado
        {
            _context = ctx;
        }

        public void Alterar(Administrador admin)
        {
            // para fazer a alteração
            _context.Update(admin);
            _context.SaveChanges();  // salva as alterações
        }

        public void AlterarParcialmente(JsonPatchDocument patchAdmin, Administrador admin)
        {
            patchAdmin.ApplyTo(admin);   // aplicar o Patch no atributo admin
            _context.Entry(admin).State = EntityState.Modified;
            _context.SaveChanges();  // salva as alterações
        }

        public Administrador BuscarPorId(int id)
        {
            var AdminId = _context.Administrador
              .Include(u => u.Usuario)   //inclui o campo  IdUsuario que essa classe possui como chave primária
              .FirstOrDefault(m => m.Id == id);

            return AdminId;
        }

        public void Excluir(Administrador admin)
        {
            _context.Administrador.Remove(admin);    //remove o atributo no parâmetro da função Remove
            var usuarioAdmin = _context.Usuarios.Find(admin.IdUsuario); // a função Find vai procurar em qual
                                                                        // usuario o administrador está selecionado para a exclusão 
            _context.Usuarios.Remove(usuarioAdmin); //remove o usuario admin
            _context.SaveChanges();  // salva as alterações
        }

        public Administrador Inserir(Administrador admin)
        {
            _context.Administrador.Add(admin); // adiciona o que foi inserido dentro da entidade
            _context.SaveChanges();  // salva as alterações
            return admin;  // retorna o resultado
        }

        public ICollection<Administrador> ListarTodosAdmin()
        {
            var buscarAdmin = _context.Administrador
                  .Include(u => u.Usuario)  // inclui a classe Usuario para ser exibido
                  .ToList();

            return buscarAdmin;

            // return _context.Administrador.ToList();
        }
    }
}
