using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILogerrClass _logger;
      public UserController( ILogerrClass logger)
        {
            _logger = logger;

            logger.WriteEvent("Сообщение о событии программы");
            logger.WriteError("Сообщение о событии программы");
        }

        [HttpGet]
        public User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Burunov",
                Email = "Ivan@gmail.com",
                Password = "12345678",
                Login = "Ivanov"

            };
        }

        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Burunov",
                Email = "ivan@gmail.com",
                Password = "12345678",
                Login = "Ivanov"
            };

            UserViewModel userViewModel = new UserViewModel(user);

            return userViewModel;
        }
    }

  
}
