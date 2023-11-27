using System.Text;
using System.Text.Json.Serialization;
using BlanquitaAPI.Data;
using BlanquitaAPI.Data.BlanquitaModels;
using BlanquitaAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

  // Configura Swagger para usar la autenticación
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[] {}
    }
  });
});

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioValidatorService>();
builder.Services.AddScoped<ComboService>();
builder.Services.AddScoped<OrdenComboService>();
builder.Services.AddScoped<OrdenService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<TipoProductoService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });
// Add DbContext
builder.Services.AddDbContext<TacosBlanquitaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"
  )));

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngularApp",
      builder => builder.WithOrigins("http://localhost:8100") // URL de tu aplicación Ionic
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
