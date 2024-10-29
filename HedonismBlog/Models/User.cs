using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace HedonismBlog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
