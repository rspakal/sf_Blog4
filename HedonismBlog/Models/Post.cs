﻿using System.Collections.Generic;

namespace HedonismBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
