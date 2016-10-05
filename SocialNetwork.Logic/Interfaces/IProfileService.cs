using SocialNetwork.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IProfileService
    {
        Profile GetById(int id);
        void Update(int id, Profile profile);
    }
}
