﻿using System.Collections.Generic;

namespace HedonismBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Post> Posts { get; set; } = new();
    }
}
