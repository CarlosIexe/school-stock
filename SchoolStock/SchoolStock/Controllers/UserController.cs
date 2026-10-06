using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolStock.Data.DTO.User;
using SchoolStock.Services;

namespace SchoolStock.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CanManageUsers")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var users = await _service.FindAll();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(string id)
    {
        var user = await _service.FindById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request)
    {
        if (request == null)
            return BadRequest();

        try
        {
            var user = await _service.Create(request);

            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id}/role")]
    public async Task<IActionResult> UpdateRole(
        string id,
        [FromBody] UpdateUserRoleRequest request)
    {
        if (request == null)
            return BadRequest();

        try
        {
            var updated =
                await _service.UpdateRole(id, request);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}