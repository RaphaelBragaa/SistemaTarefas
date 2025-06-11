using Microsoft.AspNetCore.Mvc;
using SistemaTarefa.Models;
using SistemaTarefa.Repositories.Interfaces;
using SistemaTarefa.Services;

[Route("api/[controller]")]
[ApiController]
public class ContaController : ControllerBase
{
    private readonly IUserRepositorie _userRepositorie;
    private readonly TokenService _tokenService;

    public ContaController(
        IUserRepositorie userRepositorie,
        TokenService tokenService)
    {
        _userRepositorie = userRepositorie;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserModel login)
    {

        var user = await _userRepositorie.GetUserByEmail(login.Email);
        Console.WriteLine("Cheguei n o login");

        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
        {
            return BadRequest(new
            {
                mensagem = "Credenciais inválidas. Verifique e-mail e senha."
            });
        }

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { token });
    }
}
