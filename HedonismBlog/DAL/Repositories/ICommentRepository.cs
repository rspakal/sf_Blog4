using HedonismBlog.Models;
using System.Collections;
using System.Threading.Tasks;

namespace HedonismBlog.DataAccess.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> Create(Comment comment);
        public Task<Comment> Get(int id);
        public Task<IEnumerable> GetAll();
        public Task<Comment> Update(Comment comment);
        public Task Delete(int id);
    }
}
