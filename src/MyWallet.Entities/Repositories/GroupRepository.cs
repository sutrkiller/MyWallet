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
            var users = group.Users;
            group.Users = new List<User>();
            foreach (var user in users)
            {
                group.Users.Add(_context.Users.Find(user.Id));
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

        public IQueryable<Group> GetAllGroups()
         => _context.Groups.AsQueryable();
        
        public IQueryable<Group> GetGroupsFromIds(ICollection<Guid> groupIds)
        => _context.Groups.Where(r => groupIds.Contains(r.Id));

        public async Task DeleteGroup(Group group)
        {
            _context.Entry(group).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            var local = await _context.Groups.Include(x=>x.Users).SingleOrDefaultAsync(x=>x.Id == group.Id);
            _context.Entry(local).CurrentValues.SetValues(group);
            var users = group.Users.Select(x => _context.Users.Find(x.Id)).ToList();
            local.Users = new HashSet<User>(users);
            await _context.SaveChangesAsync();
            return group;
        }
    }
}