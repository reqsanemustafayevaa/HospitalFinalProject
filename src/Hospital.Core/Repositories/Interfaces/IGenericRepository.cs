using Hospital.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity : BaseEntity, new()
    {
        DbSet<TEntity> Table {  get; }
        Task CreateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity,bool>>? expression=null,params string[]? includes);
        IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity,bool>>? expression = null, params string[]? includes);

        Task<int> CommitAsync();
    }
}
