using HedonismBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace HedonismBlog.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        HedonismBlogContext _context;
        public CommentRepository(HedonismBlogContext context)
        {
            _context = context;
        }
        public async Task<Comment> Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return await Get(comment.Id);
        }

        public async Task Delete(int id)
        {
            var _comment = await Get(id);
            _context.Comments.Remove(_comment);
            _context.SaveChanges();
        }

        public async Task<Comment> Get(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> Update(Comment comment)
        {
            var _comment = await Get(comment.Id);
            if (_comment == null) 
            {
                return null;
            }
            _comment = comment;
            _context.Comments.Update(_comment);
            await _context.SaveChangesAsync();
            return await Get(_comment.Id);
        }
    }
}
