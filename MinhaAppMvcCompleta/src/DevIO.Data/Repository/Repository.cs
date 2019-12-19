using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new ()
    {
        protected readonly MeuDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(MeuDBContext context)
        {
            _context = context;

            _dbSet = _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context?.Dispose();            
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = new TEntity { Id = id };
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }        

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }        

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
