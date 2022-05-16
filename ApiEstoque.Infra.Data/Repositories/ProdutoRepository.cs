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
    public class ProdutoRepository : IProdutoRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //construtor para injeção de dependência
        public ProdutoRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Produto entity)
        {
            _sqlServerContext.Produto.Add(entity);
            _sqlServerContext.SaveChanges();
        }

        public void Update(Produto entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public void Delete(Produto entity)
        {
            _sqlServerContext.Produto.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Produto> GetAll()
        {
            return _sqlServerContext.Produto
                .AsNoTracking()
                .Include(p => p.Estoque) //JOIN
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public Produto GetById(Guid id)
        {
            return _sqlServerContext.Produto
                .AsNoTracking()
                .FirstOrDefault(p => p.IdProduto == id);
        }

        public List<Produto> GetAllByUsuario(Guid idUsuario)
        {
            return _sqlServerContext.Produto
                .AsNoTracking()
                .Include(p => p.Estoque) //JOIN
                .Where(p => p.IdUsuario == idUsuario)
                .OrderBy(p => p.Nome)
                .ToList();
        }
    }
}



