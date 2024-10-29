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
using System.Threading.Tasks;

namespace HedonismBlog.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TagController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, ILogger<HomeController> logger, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagRepository.GetAll();
            var tagViewModels = _mapper.Map<List<TagViewModel>>(tags);
            return View(tagViewModels);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            //ViewBag.Message = $"Create new tag";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SubmitCreate(TagViewModel viewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View("Create");
            }

            if ((await _tagRepository.GetByName(viewModel.Text) != null))
            {
                ViewBag.Message = $"The tag '{viewModel.Text.ToLower()}' is already exist";
                return View("Create");
            }

            var tag = _mapper.Map<Tag>(viewModel);
            await _tagRepository.Create(tag);
            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var tag = _tagRepository.GetById(id);
            return View(tag);
        }
        public IActionResult GetAll()
        {
            var tags = _tagRepository.GetAll();
            return View(tags);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagRepository.GetById(id);
            var tagViewModel = _mapper.Map<TagViewModel>(tag);
            return View(tagViewModel);
        }

        public async Task<IActionResult> SubmitEdit(TagViewModel tagViewModel) 
        {
            var tag = _mapper.Map<Tag>(tagViewModel);
            await _tagRepository.Update(tag);
            return RedirectToAction("Index", "Tag");
        }

        public IActionResult Delete(int id)
        {
            _tagRepository.Delete(id);
            return View();
        }

    }
}
