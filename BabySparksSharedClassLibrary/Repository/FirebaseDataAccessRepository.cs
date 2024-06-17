using BabySparksSharedClassLibrary.Enums;
using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Repository
{
    public class FirebaseDataAccessRepository : IFirebaseDataAccess
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public FirebaseDataAccessRepository()
        {
            string filepath = "D:\\Downloads\\babysparks-e2b75-firebase-adminsdk-6alv0-8a199ae983.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            projectId = "babysparks-e2b75";
            fireStoreDb = FirestoreDb.Create(projectId);
        }
        public async Task AddUser(User user)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                await colRef.AddAsync(user);
            }
            catch
            {
                throw;
            }
        }
        public async Task AddDaycare(DayCare daycare)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                await colRef.AddAsync(daycare);
            }
            catch
            {
                throw;
            }
        }
        public async Task AddParent(Parent parent)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                await colRef.AddAsync(parent);
            }
            catch
            {
                throw;
            }
        }
        public async Task AddNanny(Nanny nanny)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                await colRef.AddAsync(nanny);
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> GetUser(string id)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                Query query = colRef.WhereEqualTo("Id", id);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot snapshot = querySnapshot.Documents[0];
                    User user = snapshot.ConvertTo<User>();
                    switch (user.userType)
                    {
                        case UserType.Parent:
                            return snapshot.ConvertTo<Parent>();
                        case UserType.Nanny:
                            return snapshot.ConvertTo<Nanny>();
                        case UserType.DayCare:
                            return snapshot.ConvertTo<DayCare>();
                        default:
                            return user;
                    }
                }
                else
                {
                    return new User();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<Parent> GetParent(string id)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                Query query = colRef.WhereEqualTo("Id", id);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot snapshot = querySnapshot.Documents[0];
                    Parent parent = snapshot.ConvertTo<Parent>();
                    return parent;
                }
                else
                {
                    return new Parent();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<Nanny> GetNanny(string id)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                Query query = colRef.WhereEqualTo("Id", id);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot snapshot = querySnapshot.Documents[0];
                    Nanny nanny = snapshot.ConvertTo<Nanny>();
                    return nanny;
                }
                else
                {
                    return new Nanny();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<DayCare> GetDaycare(string id)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("user");
                Query query = colRef.WhereEqualTo("Id", id);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot snapshot = querySnapshot.Documents[0];
                    DayCare daycare = snapshot.ConvertTo<DayCare>();
                    return daycare;
                }
                else
                {
                    return new DayCare();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
