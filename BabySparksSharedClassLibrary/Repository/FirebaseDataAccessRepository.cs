using BabySparksSharedClassLibrary.Enums;
using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Repository
{
    public class CredentialParameters
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }
        [JsonProperty("private_key_id")]
        public string PrivateKeyId { get; set; }
        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }
        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }
        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }
        [JsonProperty("auth_provider_x509_cert_url")]
        public string AuthProviderCertUrl { get; set; }
        [JsonProperty("client_x509_cert_url")]
        public string ClientCertUrl { get; set; }
    }
    public class FirebaseDataAccessRepository : IFirebaseDataAccess
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public FirebaseDataAccessRepository()
        {
            //string filepath = "D:\\Downloads\\babysparks-e2b75-firebase-adminsdk-6alv0-8a199ae983.json";
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            //projectId = "babysparks-e2b75";
            //fireStoreDb = FirestoreDb.Create(projectId);

            // string jsonContent = "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"babysparks-e2b75\",\r\n  \"private_key_id\": \"8a199ae9831db0bc1161ab79384a26d0aa3d6dfa\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCqYcrRu9YrArj+\\n8fS9EMkHZF/VBG1BjjcGELj0a/qnHk509z4IaC4nSlwkl7eMvTG6nYcxuRWbR0Jd\\n+Qf+/OK6OCJsTHv1Nghk8meJXSYTldVIt5+YHAkeTm5CQwXFFDIZikaOTfBaSQou\\nJoA4dRiWSJ4GdWEWAHZWnsKQIwHzUFCGdnuBJWkKmFBDm0dSx8D7r9q9WtO783YI\\n0vhdAeQwQq8Ms/wnxTAp293XGaS6b3h1kEb6Vg6xQoUFmxNQKI2jHp4ncDWAPslp\\neRiNkSeGw3xQhSClYVzGstHbVZWY3Q0JlI6TK7fIUdFyqUbnD/KMD/crsQYUTT6n\\nJhd2iI2NAgMBAAECggEADx55aG4SHaPytZDT5lkQdar3BAiOB2SuxkeCmhHg3qaG\\nUMPFyeowukTTc13RKFh7irNC1Ws2mx5SKdOY3YwkNGEZ0OFimIpQaWDHRj2xTMid\\ncgP1UgKe3oUYLKIU532YyoJmK2eIHgLgN+MrupbNdTyBZVJSKKkm1x3kZvTsTeJb\\n7F8BhoswhK7ANsTQhdUFir8VJlLej3BKMiJiFvnjgyu3vc4vVJRzzLYKpTxjRZ8u\\nhPxvf62/TRS9OezszTid9MBHNP9jmLUePXczDT1moyWaNljnXFyHFChKgdvFnmKf\\nck/ZE+/A/q0RUg6cP//3WOC58qvoIPny/XKj2ZM0YwKBgQDiEZk8fT08huVSVV2g\\n3z21hAvhugUplsMC05k2iWzvcqCAIxUqtRgu6p7Tr1fBamUqUvXDLEXa+j9snrdh\\nQB/2nlLhKxGWcvmcQGXsGcZFqgpiwgtdDKqYN2O/RC640HaAoFjn+b8myhwqnjLf\\n7vBpU9c1TpGRZBdRtaswu7vjewKBgQDA8L2OXBHyR/7udOXLfOnh7CJ92S8yLSnc\\n8q96ww2du2Z+39njLS1lNbyuzQ1Wn5ZC8lZZoOM8deHeVPdtvJi/oh0zGiUmDs0t\\n7VeO80yjMnxsjxvcD2XF6pGUfNwBxmMUCls32o88QczLpbzuZI7CkBrgC056cChe\\naP1MXPIglwKBgQCa/f3VXvgQ+1tWK3jZxrSEMlgDFopbjSV+VpyAq4+oSyqHL4Zf\\nlXFTGUBzWZAM52kcr2wXt87c5x181wRbBJ/lcX7ZKbIROBqyaeR3DuTE9mPQecFY\\ntkpvueoFRDXUN/hHD6hkwvGvInpVLckrhqIFVJoDzi43RCxItQj9jHiw6QKBgQCb\\n8Le0XPPgV5JTVsX4HsDF6d3Cy4vaySgBvWBZH1dc+f8QYoqvk28SU0lqGq9CKQAA\\noe9qx5+B2WEyGohU+E6Y7EUfbW1DAkmRajgPgNObP43TDCOfTA6c1UbOtc/320lv\\nBCPP0+Va9W+51P4Ly9iapnAiTuEiEo5+J+s6EhFchwKBgQCEQm/nkZFXIKjiH7eH\\ntNg/VREgbiPg/hZvaWbJ0Z100k63SSHfY3B5TQO08C4RIneuohI07m1kCm2upKGL\\n4C8DrhOleVhPhy13Qa2qTZt+tttlLCaVBjR5eL2Xnm6zS2BYv6CqKvNr8AsxOxnr\\nt0F3wKChQn+mpir48rO98X47xw==\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"firebase-adminsdk-6alv0@babysparks-e2b75.iam.gserviceaccount.com\",\r\n  \"client_id\": \"105289808747856008732\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-6alv0%40babysparks-e2b75.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}";
            string jsonContent = @"
            {
              ""type"": ""service_account"",
              ""project_id"": ""babysparks-e2b75"",
              ""private_key_id"": ""8a199ae9831db0bc1161ab79384a26d0aa3d6dfa"",
              ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCqYcrRu9YrArj+\n8fS9EMkHZF/VBG1BjjcGELj0a/qnHk509z4IaC4nSlwkl7eMvTG6nYcxuRWbR0Jd\n+Qf+/OK6OCJsTHv1Nghk8meJXSYTldVIt5+YHAkeTm5CQwXFFDIZikaOTfBaSQou\nJoA4dRiWSJ4GdWEWAHZWnsKQIwHzUFCGdnuBJWkKmFBDm0dSx8D7r9q9WtO783YI\n0vhdAeQwQq8Ms/wnxTAp293XGaS6b3h1kEb6Vg6xQoUFmxNQKI2jHp4ncDWAPslp\neRiNkSeGw3xQhSClYVzGstHbVZWY3Q0JlI6TK7fIUdFyqUbnD/KMD/crsQYUTT6n\nJhd2iI2NAgMBAAECggEADx55aG4SHaPytZDT5lkQdar3BAiOB2SuxkeCmhHg3qaG\nUMPFyeowukTTc13RKFh7irNC1Ws2mx5SKdOY3YwkNGEZ0OFimIpQaWDHRj2xTMid\ncgP1UgKe3oUYLKIU532YyoJmK2eIHgLgN+MrupbNdTyBZVJSKKkm1x3kZvTsTeJb\n7F8BhoswhK7ANsTQhdUFir8VJlLej3BKMiJiFvnjgyu3vc4vVJRzzLYKpTxjRZ8u\nhPxvf62/TRS9OezszTid9MBHNP9jmLUePXczDT1moyWaNljnXFyHFChKgdvFnmKf\nck/ZE+/A/q0RUg6cP//3WOC58qvoIPny/XKj2ZM0YwKBgQDiEZk8fT08huVSVV2g\n3z21hAvhugUplsMC05k2iWzvcqCAIxUqtRgu6p7Tr1fBamUqUvXDLEXa+j9snrdh\nQB/2nlLhKxGWcvmcQGXsGcZFqgpiwgtdDKqYN2O/RC640HaAoFjn+b8myhwqnjLf\n7vBpU9c1TpGRZBdRtaswu7vjewKBgQDA8L2OXBHyR/7udOXLfOnh7CJ92S8yLSnc\n8q96ww2du2Z+39njLS1lNbyuzQ1Wn5ZC8lZZoOM8deHeVPdtvJi/oh0zGiUmDs0t\n7VeO80yjMnxsjxvcD2XF6pGUfNwBxmMUCls32o88QczLpbzuZI7CkBrgC056cChe\naP1MXPIglwKBgQCa/f3VXvgQ+1tWK3jZxrSEMlgDFopbjSV+VpyAq4+oSyqHL4Zf\nlXFTGUBzWZAM52kcr2wXt87c5x181wRbBJ/lcX7ZKbIROBqyaeR3DuTE9mPQecFY\ntkpvueoFRDXUN/hHD6hkwvGvInpVLckrhqIFVJoDzi43RCxItQj9jHiw6QKBgQCb\n8Le0XPPgV5JTVsX4HsDF6d3Cy4vaySgBvWBZH1dc+f8QYoqvk28SU0lqGq9CKQAA\noe9qx5+B2WEyGohU+E6Y7EUfbW1DAkmRajgPgNObP43TDCOfTA6c1UbOtc/320lv\nBCPP0+Va9W+51P4Ly9iapnAiTuEiEo5+J+s6EhFchwKBgQCEQm/nkZFXIKjiH7eH\ntNg/VREgbiPg/hZvaWbJ0Z100k63SSHfY3B5TQO08C4RIneuohI07m1kCm2upKGL\n4C8DrhOleVhPhy13Qa2qTZt+tttlLCaVBjR5eL2Xnm6zS2BYv6CqKvNr8AsxOxnr\nt0F3wKChQn+mpir48rO98X47xw==\n-----END PRIVATE KEY-----\n"",
              ""client_email"": ""firebase-adminsdk-6alv0@babysparks-e2b75.iam.gserviceaccount.com"",
              ""client_id"": ""105289808747856008732"",
              ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
              ""token_uri"": ""https://oauth2.googleapis.com/token"",
              ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
              ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-6alv0%40babysparks-e2b75.iam.gserviceaccount.com""
            }";
            // Deserialize the JSON content to an object
            var credentialParameters = JsonConvert.DeserializeObject<CredentialParameters>(jsonContent);

            if (credentialParameters == null)
            {
                throw new InvalidOperationException("Failed to deserialize credential parameters from the provided JSON content.");
            }

            // Create GoogleCredential from the deserialized object
            var initializer = new ServiceAccountCredential.Initializer(credentialParameters.ClientEmail)
            {
                ProjectId = credentialParameters.ProjectId,
                KeyId = credentialParameters.PrivateKeyId
            }.FromPrivateKey(credentialParameters.PrivateKey);

            var credential = new ServiceAccountCredential(initializer);

            if (credential == null)
            {
                throw new InvalidOperationException("Failed to create GoogleCredential from the deserialized JSON content.");
            }

            // Initialize Firestore using the GoogleCredential
            fireStoreDb = FirestoreDb.Create(credentialParameters.ProjectId, new FirestoreClientBuilder
            {
                Credential = GoogleCredential.FromServiceAccountCredential(credential)
            }.Build());

            if (fireStoreDb == null)
            {
                throw new InvalidOperationException("Failed to initialize Firestore.");
            }

            Console.WriteLine("Firestore initialized successfully.");


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
        public async Task AddChild(Child child, string parentId)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("user").Document(parentId);
                CollectionReference colref = docRef.Collection("children");
                child.createdRecord = DateTime.UtcNow.Date;
                child.createdRecord = DateTime.SpecifyKind(child.createdRecord, DateTimeKind.Utc);
                child.modifiedRecord = DateTime.SpecifyKind(child.modifiedRecord, DateTimeKind.Utc);
                child.DateOfBirth = DateTime.SpecifyKind(child.DateOfBirth, DateTimeKind.Utc);
                await colref.AddAsync(child);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Child>> GetChildren(string parentId)
        {
            try
            {
                // Reference the specific document for the parent using the provided parentId
                DocumentReference docRef = fireStoreDb.Collection("user").Document(parentId);

                // Reference the "children" subcollection within the parent document
                CollectionReference childrenColRef = docRef.Collection("children");

                // Retrieve all documents from the "children" subcollection
                QuerySnapshot snapshot = await childrenColRef.GetSnapshotAsync();

                // Convert the documents to a list of Child objects
                List<Child> children = new List<Child>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        Child child = document.ConvertTo<Child>();
                        child.DocId = document.Id;
                        children.Add(child);
                    }
                }

                return children;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                throw new Exception("Error retrieving children from Firestore", ex);
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
                    user.DocId = snapshot.Id;
                    switch (user.userType)
                    {
                        case UserType.Parent:
                            Parent parent = snapshot.ConvertTo<Parent>();
                            parent.DocId = snapshot.Id;
                            return parent;
                        case UserType.Nanny:
                            Nanny nanny = snapshot.ConvertTo<Nanny>();
                            nanny.DocId = snapshot.Id;
                            return nanny;
                        case UserType.DayCare:
                            DayCare dayCare = snapshot.ConvertTo<DayCare>();
                            dayCare.DocId = snapshot.Id;
                            return dayCare;
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
        public async Task<IEnumerable<DayCare>> GetDaycareInCity(string city)
        {
            try
            {

                CollectionReference colRef = fireStoreDb.Collection("user");
                Query query = colRef.WhereEqualTo("City", city)
                    .WhereEqualTo("userType", UserType.DayCare);
                
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
                List<DayCare> daycares = new List<DayCare>();
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        DayCare daycare = documentSnapshot.ConvertTo<DayCare>();
                        daycare.DocId = documentSnapshot.Id;
                        daycares.Add(daycare);
                    }
                }

                // Return the list of daycares
                return daycares;
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
