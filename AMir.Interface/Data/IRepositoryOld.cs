using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AMir.Interface.Data
{
    public interface IRepositoryOld<TEntity,TEntityKey> 
        where TEntity : class
        where TEntityKey : IEquatable<TEntityKey>
    {
        IEnumerable<TEntity> Get(Func<TEntity,bool> predicate); 
        TEntity Get(TEntityKey id); 
        void Create(TEntity item); 
        void Update(TEntity item); 
        void Delete(TEntityKey id); 
        void Save(); 
    }
}
public interface IRepositoryCustom<TModel,TEntity, TEntityKey>
        where TEntity : class
        where TEntityKey : IEquatable<TEntityKey>
{
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    TEntity Get(TEntityKey id);
    void Create(TEntity item);
    void Update(TEntity item);
    void Delete(TEntityKey id);
    void Save();
}


