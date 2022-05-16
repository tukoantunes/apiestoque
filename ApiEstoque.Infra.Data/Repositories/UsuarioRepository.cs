using ApiEstoque.Infra.Data.Contexts;
using ApiEstoque.Infra.Data.Entities;
using ApiEstoque.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoque.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //construtor para inicialização do atributo (injeção de dependência)
        public UsuarioRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Usuario entity)
        {
            _sqlServerContext.Usuario.Add(entity);
            _sqlServerContext.SaveChanges();
        }

        public void Update(Usuario entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public void Delete(Usuario entity)
        {
            _sqlServerContext.Usuario.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Usuario> GetAll()
        {
            return _sqlServerContext.Usuario
                .OrderBy(u => u.Nome)
                .ToList();
        }

        public Usuario GetById(Guid id)
        {
            return _sqlServerContext.Usuario.Find(id);
        }

        public Usuario Get(string email)
        {
            return _sqlServerContext.Usuario
                .FirstOrDefault(u => u.Email.Equals(email));
        }

        public Usuario Get(string email, string senha)
        {
            return _sqlServerContext.Usuario
                .FirstOrDefault(u => u.Email.Equals(email)
                                  && u.Senha.Equals(senha));
        }
    }
}



