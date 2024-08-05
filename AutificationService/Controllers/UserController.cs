using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using AuthenticationService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AutificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionHandler]
    public class UserController : ControllerBase
    {
        private ILogerrClass _logger;
        private IMapper _mapper;
        private IUserRepository _userRepository;
      public UserController( ILogerrClass logger , IMapper mapper , IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;

            logger.WriteEvent("Сообщение о событии программы");
            logger.WriteError("Сообщение о событии программы");
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не коректен");

            User user = _userRepository.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь не найден");

            if (user.Password != password)
                throw new AuthenticationException("Пароль не верный");

            var claims = new List<Claim>
            {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims ,
                "AppCookie", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType 
                );

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)); 

            return _mapper.Map<UserViewModel>(user);
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{login}")]
        public IActionResult GetUserByLogin(string login)
        {
            var user = _userRepository.GetByLogin(login);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("viewmodel")]
        public IActionResult GetUserViewModel()
        {
            var user = _userRepository.GetByLogin("ivanov");
            if (user == null)
                return NotFound();

            var userViewModel = _mapper.Map<UserViewModel>(user);
            return Ok(userViewModel);
        }
    }

  
}
