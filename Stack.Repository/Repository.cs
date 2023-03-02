using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stack.Repository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public TContext context;
        public DbSet<TEntity> dbSet;

        public Repository(TContext _context)
        {
            context = _context;
            dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            int pageNumber = 0,
            int itemsPerPage = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties) && !string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        query = query.Include(includeProperty);
                    }
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                if (pageNumber > 0 && itemsPerPage > 0)
                {
                    query = orderBy(query)
                        .Skip((pageNumber - 1) * itemsPerPage)
                        .Take(itemsPerPage);
                }
                else
                {
                    query = orderBy(query); ;
                }
            }
            else
            {
                if (pageNumber > 0 && itemsPerPage > 0)
                {
                    query = query
                        .Skip((pageNumber - 1) * itemsPerPage)
                        .Take(itemsPerPage);
                }
            }
            return await Task.Run(() =>
            {
                return query;
            });
        }

        public virtual async Task<TEntity> GetByIdAsync(params object[] id)
        {
            var item = await dbSet.FindAsync(id);
            //if (item != null && !item.IsDeleted)
            //{
            return item;
            //}
            //return null;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            //entity.CreatedOn = DateTime.UtcNow;
            var dbChangeTracker = await dbSet.AddAsync(entity);
            return dbChangeTracker.State == EntityState.Added ? dbChangeTracker.Entity : null;
        }


        public virtual async Task<bool> UpdateAsync(TEntity entityToUpdate)
        {
            return await Task.Run(() =>
            {
                //entityToUpdate.ModifiedOn = DateTime.UtcNow;
                var dbChangeTracker = dbSet.Update(entityToUpdate);
                return dbChangeTracker.State == EntityState.Modified;
            });
        }

        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
            return await Task.Run(() =>
            {
                if (entity != null)
                {
                    //entity.IsDeleted = true;
                    //return await UpdateAsync(entityToDelete) != null ? true : false;
                    var dbChangeTracker = dbSet.Remove(entity);
                    return dbChangeTracker.State == EntityState.Deleted;
                }
                return false;
            });

        }

    }
}
