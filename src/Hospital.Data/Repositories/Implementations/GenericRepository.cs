using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericReposiotry<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<TEntity> Table => _context.Set<TEntity>(); 

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task createAsync(TEntity entity)
        {
           Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query=Table.AsQueryable();
            return expression is not null
                 ?query.Where(expression) 
                 : query;
           
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = Table.AsQueryable();
            return expression is not null
                 ? query.Where(expression).FirstOrDefaultAsync()
                 : query.FirstOrDefaultAsync();
        }
        public IQueryable<TEntity> GetQuery(params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
