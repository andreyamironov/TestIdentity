using System;
using System.Collections.Generic;
using System.Text;

namespace AMir.Interface.Data
{
    public interface IReaderRepository<TEntity, TEntityKey>
        where TEntity : class
        where TEntityKey : IEquatable<TEntityKey>
    {
        IEnumerable<TEntity> GetList(Func<TEntity, bool> predicate, int skip, int take, out int total);
        TEntity Get(Func<TEntity, bool> predicate);
        int IndexOf(Func<TEntity, bool> predicate, TEntity entity);
    }

    public interface IReaderByOwnerRepository<TOwnerEntityKey,TEntity, TEntityKey>
        where TOwnerEntityKey : IEquatable<TOwnerEntityKey>
        where TEntity : class
        where TEntityKey : IEquatable<TEntityKey>
    {
        IEnumerable<TEntity> GetList(TOwnerEntityKey ownerEntityKey,Func<TEntity, bool> predicate, int skip, int take, out int total);
        TEntity Get(TOwnerEntityKey ownerEntityKey,Func<TEntity, bool> predicate);
        int IndexOf(TOwnerEntityKey ownerEntityKey,Func<TEntity, bool> predicate, TEntity entity);
    }
}
