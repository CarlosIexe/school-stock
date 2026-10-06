using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolStock.Data;
using SchoolStock.Models.Context;
using SchoolStock.Repositories;
using SchoolStock.Repositories.Impl;
using SchoolStock.Services;
using SchoolStock.Services.Impl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var connectionString =
    builder.Configuration.GetConnectionString(
        "DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IStockRepository,StockRepositoryImpl>();
builder.Services.AddScoped<IProductService,ProductServiceImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<ISchoolService, SchoolServiceImpl>();
builder.Services.AddScoped<IStockService, StockServiceImpl>();
builder.Services.AddScoped<IStockMovementService, StockMovementServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

//builder.Services.AddScoped<IStockService,StockServiceImpl>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
            )
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageUsers", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("CanManageCategories", policy =>
    {
        policy.RequireRole("Admin", "Gestor");
    });

    options.AddPolicy("CanDelete", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("CanManageProducts", policy =>
    {
        policy.RequireRole("Admin", "Gestor");
    });

    options.AddPolicy("CanManageSchools", policy =>
    {
        policy.RequireRole("Admin", "Gestor");
    });

    options.AddPolicy("CanManageStock", policy =>
    {
        policy.RequireRole("Admin", "Gestor");
    });

    options.AddPolicy("CanManageMovements", policy =>
    {
        policy.RequireRole("Admin", "Gestor");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder.SeedAsync(
        userManager,
        roleManager
    );
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
