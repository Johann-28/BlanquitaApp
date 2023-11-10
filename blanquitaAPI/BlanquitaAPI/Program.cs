using System.Text.Json.Serialization;
using BlanquitaAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<TacosBlanquitaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"
  )));

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngularApp",
      builder => builder.WithOrigins("http://localhost:4200") // URL de tu aplicación Angular
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
app.UseAuthorization();
app.UseRouting();

app.MapControllers();

app.Run();
