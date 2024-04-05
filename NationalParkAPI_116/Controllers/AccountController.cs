using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Repository.IRepository;

namespace NationalParkAPI_116.Controllers
{
    [Route("api/user")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            if(ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.UserName);
                if(!isUniqueUser)
                {
                    return BadRequest("User in use!!!!");
                }
                var userInfo=_userRepository.Register(user.UserName, user.Password);
                if (userInfo == null) return BadRequest();
            }
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserVM userVM)
        {
            var user=_userRepository.Authenticate(userVM.UserName,userVM.Password);
            if (user == null) return BadRequest("WrongUser/pwd!!");
            return Ok(user);
        }

    }
}
