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
    public class GroupRepository : IGroupRepository
    {
        private readonly MyWalletContext _context;

        internal GroupRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public GroupRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

        }

        public async Task<Group> AddGroup(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            var addedGroup = _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return addedGroup;
        }

        public async Task<Group> GetSingleGroup(Guid id)
          => await _context
                .Groups
                .Where(group => group.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Group[]> GetAllGroups()
         => await _context.Groups.ToArrayAsync();
    }
}