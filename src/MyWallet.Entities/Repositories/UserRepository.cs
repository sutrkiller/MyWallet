using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyWalletContext _context;

        internal UserRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public UserRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

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

        public async Task<User> GetUserByEmail(string email)
            => await _context
                .Users
                .Where(u => u.Email == email)
                .SingleOrDefaultAsync();

        public IQueryable<User> GetAllUsers()
        => _context.Users.AsQueryable();

        public async Task<User[]> GetUsersFromIds(ICollection<Guid> userIds)
        => await _context.Users.Where(r => userIds.Contains(r.Id)).ToArrayAsync();
    }
}