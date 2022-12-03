using DearSanta.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearSanta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepo;


        public UserController(IUser userRepository)

        {
            _userRepo = userRepository;

        }

        
        //GET: UserController
        [HttpGet]
        public ActionResult GetAllUsers()
        {

            var users = _userRepo.GetAllUsers();
            return Ok(users);

        }

      
       
      

       
    }
}
