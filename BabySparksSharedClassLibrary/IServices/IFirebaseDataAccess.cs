﻿using BabySparksSharedClassLibrary.Models;
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
        Task<IEnumerable<DayCare>> GetDaycareInCity(string city);
        Task<IEnumerable<Nanny>> GetNanniesInCity(string city);
        Task<List<Child>> GetChildren(string parentId);
        Task AddChild(Child child, string parentId);
        Task<List<Message>> GetMessagesForUser(string id);
        Task AddMessage(Message message);
        Task<IEnumerable<DayCare>> GetDaycares();
        Task<IEnumerable<Nanny>> GetNannies();
        Task EnrollChildren(Enrollment enrollment, string dayCareId);
        Task<List<Child>> GetEnrolledChildrens(string daycareId);
        Task<List<Enrollment>> GetEnrollments(string daycareId);
    }
}
