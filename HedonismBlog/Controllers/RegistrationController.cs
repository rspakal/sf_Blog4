using HedonismBlog.Models;
using HedonismBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static HedonismBlog.Models.Role;
using System.Threading.Tasks;
using System;
using HedonismBlog.DataAccess.Repositories;
using AutoMapper;
using HedonismBlog.DAL.Repositories;

namespace HedonismBlog.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RegistrationController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("SubmitRegister")]
        public async Task<IActionResult> SubmitRegister(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if ((await _userRepository.GetByEmail(viewModel.Email)) != null)
            {
                ViewBag.Message = $"The user with '{viewModel.Email}' address is already exist";
                return View("Register");
            }
            var user = _mapper.Map<User>(viewModel);
            var role = await _roleRepository.GetRolesByName(Roles.User.ToString());
            if (role == null)
                throw new NullReferenceException($"No role with the name \"{Roles.User}\" in DB");
            user.Role = role;
            await _userRepository.Create(user);
            return View("RegisterSucceed");
        }
    }
}
