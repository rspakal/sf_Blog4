using AutoMapper;
using HedonismBlog.DataAccess.Repositories;
using HedonismBlog.Models;
using HedonismBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HedonismBlog.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class CommentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository, ILogger<HomeController> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            var post = await _postRepository.Get(postViewModel.Id);
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userRepository.GetByEmail(userEmail);
            var comment = _mapper.Map<Comment>(postViewModel.NewComment);
            comment.User = user;
            comment.Post = post;
            await _commentRepository.Create(comment);
            return RedirectToAction("Index", "Post");
        }

        public IActionResult Get(int id)
        {
            var comment = _commentRepository.Get(id);
            return View(comment);
        }

        public IActionResult GetAll(int id)
        {
            var comments = _commentRepository.GetAll();
            return View(comments);
        }

        public IActionResult Update(Comment comment)
        {
            _commentRepository.Update(comment);
            return View();
        }

        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return View();
        }

    }
}
