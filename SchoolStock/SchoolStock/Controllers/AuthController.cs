using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolStock.Data.DTO.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolStock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                message = "O nome é obrigatório."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest(new
            {
                message = "O e-mail é obrigatório."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                message = "A senha é obrigatória."
            });
        }

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return BadRequest(new
            {
                message = "Já existe um usuário cadastrado com este e-mail."
            });
        }

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                message = "Não foi possível criar o usuário.",
                errors = result.Errors.Select(e => e.Description)
            });
        }

        // Salva o nome como uma claim
        await _userManager.AddClaimAsync(
            user,
            new Claim(ClaimTypes.Name, request.Name)
        );

        // Cria a role User caso ela ainda não exista
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(
                new IdentityRole("User")
            );
        }

        // Define o usuário como User
        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new
        {
            message = "Usuário cadastrado com sucesso."
        });
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Unauthorized(new
            {
                message = "E-mail ou senha inválidos."
            });
        }

        var passwordValid = await _userManager.CheckPasswordAsync(
            user,
            request.Password
        );

        if (!passwordValid)
        {
            return Unauthorized(new
            {
                message = "E-mail ou senha inválidos."
            });
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                user.Id
            ),

            new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email!
            ),

            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id
            ),

            new Claim(
                ClaimTypes.Email,
                user.Email!
            )
        };

        // Recupera o nome salvo como Claim
        var nameClaim = await _userManager.GetClaimsAsync(user);

        var name = nameClaim
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)
            ?.Value;

        if (!string.IsNullOrWhiteSpace(name))
        {
            claims.Add(
                new Claim(ClaimTypes.Name, name)
            );
        }

        // Adiciona as roles ao JWT
        foreach (var role in roles)
        {
            claims.Add(
                new Claim(ClaimTypes.Role, role)
            );
        }

        var key = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
        {
            return StatusCode(500, new
            {
                message = "Chave JWT não configurada."
            });
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Ok(new LoginResponse
        {
            Token = tokenString,
            Name = name ?? string.Empty,
            Email = user.Email!,
            Roles = roles
        });
    }
}
