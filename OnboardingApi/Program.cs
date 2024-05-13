using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnboardingApi.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
        .UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"),
          b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
