using Application.Helper.Mapping;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services.JwtServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


// Database connection 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<Infrastructure.Data.IConnectionFactory>(new Infrastructure.Data.SqlConnnectionFactory(connectionString));

//Bycrpt Configuration 
// Register the BcryptOption with a default work factor
// Đọc cấu hình từ appsetting.json
var bcryptOption = builder.Configuration.GetSection("Bcrypt");
int workFactor = bcryptOption.GetValue<int>("WorkFactor", 12);// Gía trị mặc định là 12 

//Tạo và cấu hình opption BcryptOption
builder.Services.AddSingleton(new Infrastructure.Services.PasswordHasher.BcryptOption
{
    WorkFactor = workFactor
});

// Services Register
builder.Services.AddScoped<Infrastructure.Services.PasswordHasher.IPasswordHasher, Infrastructure.Services.PasswordHasher.PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
;
//Repositories Register
builder.Services.AddScoped<IUserRepository,Infrastrueture.Repositories.UserRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
//Application Services
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProffile).Assembly);
builder.Services.AddScoped<AccountService>();
//JWT Authentication configuration 
var  jwtconfig= builder.Configuration.GetSection("Jwt");
var secretKey = jwtconfig["Key"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("JWT Key not found in configuration.");
}
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtconfig["Issuer"],
            ValidAudience = jwtconfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Cấu hình ModelState tự động trả ra 400 nếu model không hợp lệ
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        setupAction.SuppressModelStateInvalidFilter = false;

    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
