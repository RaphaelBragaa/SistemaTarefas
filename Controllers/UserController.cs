using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaTarefa.Repositories.Interfaces;


namespace SistemaTarefa.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorie _userRepositorie;
       


        public UserController(IUserRepositorie userRepositorie)
        {
            _userRepositorie = userRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> SearchAllUsers()
        {
            List<UserModel> users = await _userRepositorie.SearchAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> SearchById(int id)
        {
            UserModel user = await _userRepositorie.SearchById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Add([FromBody] UserModel userModel)
        {
            UserModel user = await _userRepositorie.Add(userModel);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userModel.Name) ||
                string.IsNullOrWhiteSpace(userModel.Email) ||
                string.IsNullOrWhiteSpace(userModel.Password))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var existingUser = await _userRepositorie.GetUserByEmail(userModel.Email);
            if (existingUser != null)
            {
                return BadRequest("E-mail já cadastrado.");
            }

            userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            var user = await _userRepositorie.Add(userModel);

            user.Password = null;
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Up([FromBody] UserModel userModel, int id)
        {
            UserModel user = await _userRepositorie.Update(userModel, id);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete( int id)
        {
            bool delete = await _userRepositorie.Delete( id);

            return Ok(delete);
        }
    }

    } 

