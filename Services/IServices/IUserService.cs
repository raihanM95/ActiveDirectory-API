using ActiveDirectory_API.Models;
using System.DirectoryServices;

namespace ActiveDirectory_API.Repositories.IRepositories
{
    public interface IUserService
    {
        Task<bool> AuthenticateUser(UserCredential user);
        Task<SearchResultCollection> GetAllUsersData();
        Task<SearchResult> GetUserData(string username);
        Task<List<User>> GetUserDataFormatted();
    }
}
