using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.API.Helpers;
using MovieRecommender.API.Models.AuthModels;

namespace MovieRecommender.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public AuthController(IConfiguration configuration, IUnitOfWork unitOf)
    {
        _configuration = configuration;
        _unitOfWork = unitOf;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterRequest request)
    {
        var hash = Hasher.Hash(request.Password);
        var user = new User
        {
            Login = request.Login,
            PasswordHash = hash,
            Role = "User"
        };

        await _unitOfWork.User.Create(user);
        return Ok("Register!");
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthenticateRequest request)
    {
        var user = await _unitOfWork.User.GetByLogin(request.Login);

        if (user == null)
            return NotFound("User with that login is not found");

        if (!Hasher.Verify(request.Password, user.PasswordHash))
            return BadRequest("Password is wrong");

        var token = GetToken(user);

        return Ok(new AuthResponse
        {
            Id = user.Id,
            Login = user.Login,
            Role = user.Role,
            Token = token
        });
    }

    private string GetToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}