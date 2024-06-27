using AutoMapper;
using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Services.Impl;
using GestaoDeResiduos.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestaoDeResiduos.Repositories.Impl;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.Filters;
using GestaoDeResiduos;
using GestaoDeResiduos.Converters;

var builder = WebApplication.CreateBuilder(args);

#region Auth

var secret = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();

#endregion


#region Converters

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("dd/MM/yyyy"));
    });

#endregion 

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
    options.Filters.Add<ViewModelExceptionFilter>();
});

#endregion



// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDbConnection")));


#region AutoMapper

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);

#endregion

#region Services

builder.Services.AddScoped<IUserService, UserService>();

#endregion


#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
