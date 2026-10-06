using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SchoolStock.Data.DTO.User;
using SchoolStock.Services;

namespace SchoolStock.Services.Impl;

public class UserServiceImpl : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserServiceImpl(
        UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<UserResponse>> FindAll()
    {
        var users = _userManager.Users.ToList();

        var response = new List<UserResponse>();

        foreach (var user in users)
        {
            response.Add(
                await ConvertToResponse(user)
            );
        }

        return response;
    }

    public async Task<UserResponse?> FindById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return null;

        return await ConvertToResponse(user);
    }

    public async Task<UserResponse> Create(
        CreateUserRequest request)
    {
        var existingUser =
            await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Já existe um usuário cadastrado com este e-mail."
            );
        }

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password
        );

        if (!result.Succeeded)
        {
            var errors = string.Join(
                "; ",
                result.Errors.Select(e => e.Description)
            );

            throw new InvalidOperationException(errors);
        }

        await _userManager.AddClaimAsync(
            user,
            new Claim(
                ClaimTypes.Name,
                request.Name
            )
        );

        var validRoles = new[]
        {
            "Admin",
            "Gestor",
            "User"
        };

        if (!validRoles.Contains(request.Role))
        {
            await _userManager.DeleteAsync(user);

            throw new InvalidOperationException(
                "Role inválida."
            );
        }

        await _userManager.AddToRoleAsync(
            user,
            request.Role
        );

        return await ConvertToResponse(user);
    }

    public async Task<bool> UpdateRole(
        string id,
        UpdateUserRoleRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return false;

        var validRoles = new[]
        {
            "Admin",
            "Gestor",
            "User"
        };

        if (!validRoles.Contains(request.Role))
        {
            throw new InvalidOperationException(
                "Role inválida."
            );
        }

        var currentRoles =
            await _userManager.GetRolesAsync(user);

        if (currentRoles.Any())
        {
            var removeResult =
                await _userManager.RemoveFromRolesAsync(
                    user,
                    currentRoles
                );

            if (!removeResult.Succeeded)
                throw new InvalidOperationException(
                    "Não foi possível remover as roles atuais."
                );
        }

        var addResult =
            await _userManager.AddToRoleAsync(
                user,
                request.Role
            );

        if (!addResult.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    "; ",
                    addResult.Errors.Select(e => e.Description)
                )
            );
        }

        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return false;

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    "; ",
                    result.Errors.Select(e => e.Description)
                )
            );
        }

        return true;
    }

    private async Task<UserResponse> ConvertToResponse(
        IdentityUser user)
    {
        var claims =
            await _userManager.GetClaimsAsync(user);

        var name =
            claims
                .FirstOrDefault(
                    c => c.Type == ClaimTypes.Name
                )
                ?.Value ?? string.Empty;

        var roles =
            await _userManager.GetRolesAsync(user);

        return new UserResponse
        {
            Id = user.Id,
            Name = name,
            Email = user.Email ?? string.Empty,
            Roles = roles
        };
    }
}
