using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<IEnumerable<T>> GetAll();
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        IQueryable<T> Query { get; }
    }
}
