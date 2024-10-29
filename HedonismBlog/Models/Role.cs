using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HedonismBlog.Models
{
    public class Role
    {
        public enum Roles
        {
            Administarator,
            Moderator,
            User
        };
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
