using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Interface.Data
{
    public interface IWriterRepository<TEntity>
        where TEntity : class
        //where TEntityKey : IEquatable<TEntityKey>
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity originalEntity,TEntity source = null);
        void Delete(TEntity entity);
    }
}
