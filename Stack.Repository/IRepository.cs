﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            int pageNumber = 0,
            int itemsPerPage = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);
        Task<TEntity> GetByIdAsync(params object[] id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);

    }
}
