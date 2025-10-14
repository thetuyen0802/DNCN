using Application.Services;
using Domain.Repositories;
using Infestrueture.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services.JwtServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Thêm dịch vụ DbContext với kết nối đến SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<Infrastructure.Data.IConnectionFactory>(new Infrastructure.Data.SqlConnnectionFactory(connectionString));

// Register the BcryptOption with a default work factor
// Đọc cấu hình từ appsetting.json

var bcryptOption = builder.Configuration.GetSection("Bcrypt");
int workFactor = bcryptOption.GetValue<int>("WorkFactor", 12);// Gía trị mặc định là 12 

//Tạo và cấu hình opption BcryptOption
builder.Services.AddSingleton(new Infrastructure.Services.PasswordHasher.BcryptOption
{
    WorkFactor = workFactor
});
// Register the PasswordHasher service
builder.Services.AddScoped<Infrastructure.Services.PasswordHasher.IPasswordHasher, Infrastructure.Services.PasswordHasher.PasswordHasher>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<AccountService>();



builder.Services.AddControllers();

var  jwtconfig= builder.Configuration.GetSection("jwtconfig");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
