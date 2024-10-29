using HedonismBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HedonismBlog.DataAccess.Repositories
{
    public class TagRepository : ITagRepository
    {
        HedonismBlogContext _context;
        public TagRepository(HedonismBlogContext context)
        {
            _context = context;
        }
        public async Task<Tag> Create(Tag tag)
        {
            tag.Text = tag.Text.ToLower();
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return await GetByName(tag.Text);
        }

        public async Task Delete(int id)
        {
            var _tag = await GetById(id);
            _context.Tags.Remove(_tag);
        }

        public async Task<Tag> GetById(int id)
        {
            return await _context.Tags.FindAsync(id);
        }
        public async Task<Tag> GetByName(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Text == name.ToLower());
        }

        public async Task<IEnumerable> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }
        public async Task<IEnumerable> GetAllAsNoTracking()
        {
            return await _context.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<Tag> Update(Tag tag)
        {
            var _tag = await GetById(tag.Id);
            if (_tag == null) 
            {
                return null;
            }
            _tag.Text = tag.Text;
            _context.Tags.Update(_tag);
            await _context.SaveChangesAsync();
            return await GetById(_tag.Id);
        }
    }
}
