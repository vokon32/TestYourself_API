using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string Id);
        Task<AppUser> GetUsersTestsResultById(string Id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
