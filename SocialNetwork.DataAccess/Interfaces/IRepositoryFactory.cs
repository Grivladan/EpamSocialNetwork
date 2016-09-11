using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.EF;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(ApplicationDbContext context) where T : class, IEntity;
    }
}
