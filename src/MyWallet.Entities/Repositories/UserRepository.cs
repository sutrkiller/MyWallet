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
    internal class UserRepository : IUserRepository
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
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.PreferredCurrency == null)
            {
                throw new ArgumentNullException(nameof(user.PreferredCurrency));
            }
            
            user.PreferredCurrency = _context.Currencies.Find(user.PreferredCurrency.Id);
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

        public IQueryable<User> GetUsersFromIds(ICollection<Guid> userIds)
        => _context.Users.Where(r => userIds.Contains(r.Id));

        public async Task<User> EditUser(User user)
        {
            var local = await _context.Users.FirstOrDefaultAsync(x=>x.Id  == user.Id);
            var localCur = await _context.Currencies.FirstOrDefaultAsync(x=>x.Id == user.PreferredCurrency.Id);

            if (local == null) throw new ArgumentException("User with this id not found.");
            if (localCur == null) throw new ArgumentException("Currency with this id not found.");

            local.PreferredCurrency = localCur;
             await _context.SaveChangesAsync();
            return local;
        }
    }
}