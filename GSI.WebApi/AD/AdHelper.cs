using System.Data.SqlTypes;
using System.DirectoryServices;

namespace GSI.WebApi.AD
{
    public class AdHelper
    {
        public static List<AdUser> GetUsers()
        {
            using (var root = new DirectoryEntry($"LDAP://192.168.1.180"))
            {
                root.Username = "Administrator";
                root.Password = "teste123!@#";

                using (var searcher = new DirectorySearcher(root))
                {
                    searcher.Filter = $"(&(objectCategory=person)(objectClass=user))";


                    SearchResultCollection results = searcher.FindAll();

                    List<AdUser> users = new List<AdUser>();

                    foreach (SearchResult result in results)
                    {
                        AdUser user = new AdUser();
                        user.Id = new Guid((System.Byte[])result.Properties["objectGUID"][0]).ToString();
                        user.Name = result.Properties["name"][0].ToString();

                        user.LastLogon = GetDate((long)result.Properties["lastLogon"][0]);

                        user.AccountExpires = GetDate((long)result.Properties["accountExpires"][0]);

                        user.Department = GetOU(result.Properties["distinguishedName"][0].ToString());

                        users.Add(user);
                    }

                    return users;
                }
            }
        }

        private static DateTime? GetDate(long value)
        {
            try
            {
                DateTime dt = DateTime.FromFileTime(value);
                return dt < SqlDateTime.MinValue ? null : dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetOU(string value)
        {
            string ou = value.Split(",").ToList().FirstOrDefault(x => x.StartsWith("OU="));
            return string.IsNullOrEmpty(ou) ? null : ou.Split("=")[1];
        }
    }
}
