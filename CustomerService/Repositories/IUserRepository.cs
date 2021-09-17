

namespace CustomerService.Repositories
{
    using CustomerService.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IUserRepository
    {

        Task<IEnumerable<AppUser>> GetUsers();


        Task<AppUser> GetUserById(int UserId);


        Task<AppUser> GetUserByName(string username);

        Task<AppUser> AddUser(AppUser appUser);

        void DeleteUser(int UserId);

        void SaveUser();
    }
}
