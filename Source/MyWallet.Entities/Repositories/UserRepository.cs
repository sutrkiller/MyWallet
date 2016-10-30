using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MyWalletContext _context;

        public UserRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
        }
        public async Task<User> AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var addedUser = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return addedUser;
        }

        public async Task<User> GetSingleUser(Guid id)
          => await _context
                .Users
                .Where(user => user.Id == id)
                .SingleOrDefaultAsync();

        public async Task<User[]> GetAllUsers()
        => await _context.Users.ToArrayAsync();
    }
}