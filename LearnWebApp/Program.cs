
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using LearnWebApp.Configs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpLogging();

builder.Services.AddMvc();

builder.Services.AddCors(option =>
{
    option.AddPolicy("ReactPolicy", policy =>
    {
        policy
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});


#region UserServicesBind
builder.Services
    .AddScoped<UserManager<AppUser>>();
builder.Services.AddDbContext<IdentityDbContext<AppUser>, AppUserDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(AppUserDbContext)));
    }
);
builder.Services
    .AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppUserDbContext>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = JwtBearerConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtBearerConfig.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = JwtBearerConfig.GetKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services
    .AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors("ReactPolicy");
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
