using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JWTConstructor>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//conection to dabase
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDBConection>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

//use bearear for jwt authentication
var key = builder.Configuration["jwt"];
if (string.IsNullOrEmpty(key))
    throw new Exception("JWT Key not configured");

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)
                )
        };
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
