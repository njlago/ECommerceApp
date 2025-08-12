using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[AllowAnonymous]

public class AuthenticateController : Controller
{
    private IUserRepository userRepository;
    private ITokenService tokenService;

    public AuthenticateController(IUserRepository userRepository, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
    }

    [HttpPost("login")]

    public IActionResult Login([FromBody] Login user)
    {
        var userFound = userRepository.Login(user);
        if (userFound != null)
        {
            var token = tokenService.GenerateToken(userFound);
            return Ok(new { token });

        }
        else
        {
            return Unauthorized("Invalid credentials");
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            userRepository.Register(user);
            return Ok("User registered successfully");
        }
        else
        {
            return BadRequest("User Info Invalid");
        }
    }

}

