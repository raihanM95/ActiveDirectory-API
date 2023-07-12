using ActiveDirectory_API.Models;
using ActiveDirectory_API.Repositories.IRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace ActiveDirectory_API.Repositories
{
    public class UserService : IUserService
    {
        private readonly AppSettings _configuration;

        public UserService(IOptions<AppSettings> appIdentitySettingsAccessor)
        {
            _configuration = appIdentitySettingsAccessor.Value;
        }

        public async Task<bool> AuthenticateUser(UserCredential user)
        {
            try
            {
                //// 1st Code //
                using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry())
                {
                    entry.Username = user.Username;
                    entry.Password = user.Password;

                    //entry.Username = "Administrator";
                    //entry.Password = "Msdsl@2020";

                    //entry.Username = "raihan";
                    //entry.Password = "c!C9E~~tgXQ5`r(t";

                    //entry.Path = "LDAP://your-domain-controller";
                    //entry.Path = "LDAP://msdsl.com";

                    entry.Path = _configuration.Path.ToString();

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = $"(&(objectClass=user)(sAMAccountName={entry.Username}))";
                    searcher.PropertiesToLoad.Add("cn"); // You can load additional user attributes as needed

                    SearchResult result = searcher.FindOne();

                    if (result != null)
                    {
                        // User authenticated successfully
                        return true;
                    }
                }


                //// 2nd Code //
                //using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                //{
                //    // Validate the username and password against the Active Directory
                //    bool isValid = context.ValidateCredentials(username, password);

                //    return isValid;
                //}

                //// 3rd Code  Remove User //
                //string adPath = "LDAP://AD.msdsl.com";
                //System.DirectoryServices.DirectoryEntry directoryEntry = new System.DirectoryServices.DirectoryEntry(adPath);

                //DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
                //directorySearcher.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
                //SearchResult searchResult = directorySearcher.FindOne();

                //if (searchResult != null)
                //{
                //    System.DirectoryServices.DirectoryEntry userEntry = searchResult.GetDirectoryEntry();

                //    // Remove the user from Active Directory
                //    //userEntry.DeleteTree();
                //    //userEntry.CommitChanges();

                //    Console.WriteLine("User removed successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("User not found.");
                //}

                //// Dispose the objects
                //directorySearcher.Dispose();
                //directoryEntry.Dispose();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the authentication process
                // For security reasons, you should provide a generic error message here
                Console.WriteLine("An error occurred while authenticating. Please try again later.");
            }

            return false;
        }

        public async Task<SearchResultCollection> GetAllUsersData()
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry())
                {
                    entry.Path = _configuration.Path.ToString();

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(objectClass=user)";
                    searcher.PropertiesToLoad.Add("*"); // Load all properties for each user

                    SearchResultCollection results = searcher.FindAll();

                    return results;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the user data retrieval process
                // For security reasons, you should provide a generic error message here
                Console.WriteLine("An error occurred while retrieving user data. Please try again later.");
                return null;
            }
        }

        public async Task<SearchResult> GetUserData(string username)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry())
                {
                    entry.Path = _configuration.Path.ToString();

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";
                    searcher.PropertiesToLoad.Add("*"); // Load all properties for the user

                    SearchResult result = searcher.FindOne();

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the user data retrieval process
                // For security reasons, you should provide a generic error message here
                Console.WriteLine("An error occurred while retrieving user data. Please try again later.");
                return null;
            }
        }

        public async Task<List<User>> GetUserDataFormatted()
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry())
                {
                    entry.Path = _configuration.Path.ToString();

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(objectClass=user)";
                    searcher.PropertiesToLoad.Add("*"); // Load all properties for each user

                    List<User> users = new List<User>();
                    SearchResultCollection results = searcher.FindAll();
                    if (results != null)
                    {
                        foreach (SearchResult result in results)
                        {
                            User user = new User();
                            // Access individual attributes using the property names
                            var fName = result.Properties["givenname"].Count > 0 ? result.Properties["givenname"][0].ToString() : "";
                            var lName = result.Properties["sn"].Count > 0 ? result.Properties["sn"][0].ToString() : "";
                            user.Name = fName + " " + lName;
                            user.Username = result.Properties["sAMAccountName"].Count > 0 ? result.Properties["sAMAccountName"][0].ToString() : "";
                            user.Email = result.Properties["mail"].Count > 0 ? result.Properties["mail"][0].ToString() : "";
                            user.Contact = result.Properties["mobile"].Count > 0 ? result.Properties["mobile"][0].ToString() : "";
                            var streetaddress = result.Properties["streetaddress"].Count > 0 ? result.Properties["streetaddress"][0].ToString() : "";
                            var postofficebox  = result.Properties["postofficebox"].Count > 0 ? result.Properties["postofficebox"][0].ToString() : "";
                            var l = result.Properties["l"].Count > 0 ? result.Properties["l"][0].ToString(): "";
                            var st = result.Properties["st"].Count > 0 ? result.Properties["st"][0].ToString() : "";
                            var postalcode = result.Properties["postalcode"].Count > 0 ? result.Properties["postalcode"][0].ToString() : "";
                            var co = result.Properties["co"].Count > 0 ? result.Properties["co"][0].ToString() : "";
                            user.Address = "Street: " + streetaddress + ", P.O Box: " + postofficebox + ", City: " + l + ", State/province: " + st + ", Zip/Postal Code: " + postalcode + ", Country/region: " + co;
                            user.Designation = result.Properties["title"].Count > 0 ? result.Properties["title"][0].ToString() : "";
                            user.Department = result.Properties["department"].Count > 0 ? result.Properties["department"][0].ToString() : "";
                            user.Branch = result.Properties["physicaldeliveryofficename"].Count > 0 ? result.Properties["physicaldeliveryofficename"][0].ToString() : "";
                            user.Company = result.Properties["company"].Count > 0 ? result.Properties["company"][0].ToString() : "";

                            users.Add(user);
                        }
                        
                    }

                    return users;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the user data retrieval process
                // For security reasons, you should provide a generic error message here
                Console.WriteLine("An error occurred while retrieving user data. Please try again later.");
                return null;
            }
        }
    }
}
