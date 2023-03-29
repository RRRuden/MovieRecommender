using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.API.Models.AuthModels;

namespace MovieRecommender.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [HttpPut("UpdateRole")]
    public async Task<ActionResult<UserResponse>> RoleUpdate(RoleUpdateRequest request)
    {
        var user = await _unitOfWork.User.GetById(request.Id);
        if (user == null)
            return NotFound();

        user.Role = request.Role;
        await _unitOfWork.User.Update(user);

        return Ok(new UserResponse
        {
            Id = user.Id,
            Login = user.Login,
            Role = user.Role
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserResponse>> DeleteUser(int id)
    {
        var user = await _unitOfWork.User.GetById(id);

        if (user == null)
            return NotFound();

        await _unitOfWork.User.Delete(user);

        return Ok(
            new UserResponse
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role
            });
    }

    [HttpGet("All")]
    public async Task<IEnumerable<UserResponse>> GetAll()
    {
        var users = await _unitOfWork.User.GetUsers();
        return users.Select(x => new UserResponse { Id = x.Id, Login = x.Login, Role = x.Role });
    }
}