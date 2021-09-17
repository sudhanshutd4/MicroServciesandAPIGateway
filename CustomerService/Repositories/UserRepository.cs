

namespace CustomerService
{
    using Microsoft.EntityFrameworkCore;
    using CustomerService.Data;
    using CustomerService.Entities;
    using CustomerService.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.appUsers.ToListAsync();
        }

        public async Task<AppUser> GetUserById(int UserId)
        {
            return await _context.appUsers.FindAsync(UserId);
        }

        public async Task<AppUser> GetUserByName(string username)
        {
            return await _context.appUsers.FindAsync(username);
        }
        public async Task<AppUser> AddUser(AppUser appUser)
        {
            var result = await _context.AddAsync(appUser);
            SaveUser();
            return result.Entity;
        }

        public async void DeleteUser(int UserId)
        {
            var result = await _context.appUsers.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (result != null)
            {
                _context.appUsers.Remove(result);
                SaveUser();
            }
        }

        public async void SaveUser()
        {
            await _context.SaveChangesAsync();
        }
    }
}
