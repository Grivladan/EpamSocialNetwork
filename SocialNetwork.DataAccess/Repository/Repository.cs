using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialNetwork.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly IdentityDbContext _context;
        private DbSet<T> _entities;

        public Repository(IdentityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public T GetById(int id)
        {
            return Query.First(x => x.Id == id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                Entities.Add(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    throw new ArgumentNullException("id");
                }
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected IDbSet<T> Entities {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public IQueryable<T> Query
        {
            get { return Entities; }
        }
    }
}
