using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMir.Interface.Data
{
    public interface IWriterRepository<TEntity>
        where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity originalEntity,TEntity source = null);
        void Delete(TEntity entity);
    }

    public interface IWriterRepositoryAsync<TEntity>
        where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity originalEntity, TEntity source = null);
        Task Delete(TEntity entity);
    }
}
