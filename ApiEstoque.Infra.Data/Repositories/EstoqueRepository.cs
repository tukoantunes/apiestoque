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
    public class EstoqueRepository : IEstoqueRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //método construtor para injeção de dependência (inicialização)
        public EstoqueRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Estoque entity)
        {
            _sqlServerContext.Estoque.Add(entity);
            _sqlServerContext.SaveChanges();
        }

        public void Update(Estoque entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public void Delete(Estoque entity)
        {
            _sqlServerContext.Estoque.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Estoque> GetAll()
        {
            return _sqlServerContext.Estoque
                .AsNoTracking()
                .OrderBy(e => e.Nome)
                .ToList();
        }

        public Estoque GetById(Guid id)
        {
            return _sqlServerContext.Estoque
                .AsNoTracking()
                .FirstOrDefault(e => e.IdEstoque == id);
        }

        public List<Estoque> GetAllByUsuario(Guid idUsuario)
        {
            return _sqlServerContext.Estoque
                .AsNoTracking()
                .Where(e => e.IdUsuario == idUsuario)
                .OrderBy(e => e.Nome)
                .ToList();
        }
    }
}



