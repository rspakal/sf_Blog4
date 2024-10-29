using HedonismBlog.DataAccess;
using HedonismBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using static HedonismBlog.Models.Role;

namespace HedonismBlog.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HedonismBlogContext _context;
        public RoleRepository(HedonismBlogContext context)
        {
            _context = context;
        }

        public async Task Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task<Role> GetByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
        public async Task<IEnumerable> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
        public Task<IEnumerable> GetAllUsers(Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> GetRolesByName(string Name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == Name);
        }

        public async Task<Role> Update(Role role)
        {
            var _role = await _context.Roles.FindAsync(role.Id);
            _role.Name = role.Name;
            _role.Description = role.Description;
            _context.Roles.Update(_role);
            await _context.SaveChangesAsync();
            return await _context.Roles.FindAsync(role.Id);
        }
    }
}
