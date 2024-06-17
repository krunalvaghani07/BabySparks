using BabySparksSharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.IServices
{
    public interface IFirebaseDataAccess
    {
        Task<User> GetUser(string id);
        Task<Parent> GetParent(string id);
        Task<Nanny> GetNanny(string id);
        Task<DayCare> GetDaycare(string id);
        Task AddUser(User user);
        Task AddParent(Parent parent);
        Task AddNanny(Nanny nanny);
        Task AddDaycare(DayCare daycare);

    }
}
