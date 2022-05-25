using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebCbt_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(x =>
    x.UseNpgsql(builder.Configuration["ConnectionStrings:WebCbt"]));

builder.Services.AddDbContext<WebCbtDbContext>(x =>
    x.UseLazyLoadingProxies()
    .UseNpgsql(builder.Configuration["ConnectionStrings:WebCbt"]));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCors(x =>
    x.AddPolicy("AllOrigins", y =>
        y.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddAuthentication(x =>
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"]
        };
    });

builder.Services.AddAuthorization(x =>
    x.AddPolicy("AdminOnly", y =>
       y.RequireClaim("userStatus", "1")));

builder.Services.AddRouting(x =>
    x.LowercaseUrls = true);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
