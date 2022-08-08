using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAES.BLL.Interfaces
{
    public interface IPersona { 
    
            void Create<TEntity>(TEntity entity) where TEntity : class;

            void Update<TEntity>(TEntity entity) where TEntity : class;

            void Delete<TEntity>(object id) where TEntity : class;

            void Delete<TEntity>(TEntity entity) where TEntity : class;

            void Save();

            IEnumerable<TEntity> GetAll<TEntity>(
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = null,
                int? skip = null,
                int? take = null)
                where TEntity : class;

            IEnumerable<TEntity> Get<TEntity>(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = null,
                int? skip = null,
                int? take = null)
                where TEntity : class;

            TEntity GetFirst<TEntity>(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = null)
                where TEntity : class;

            TEntity GetById<TEntity>(object id)
                where TEntity : class;

            int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
                where TEntity : class;

            bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
                where TEntity : class;
        }
}

