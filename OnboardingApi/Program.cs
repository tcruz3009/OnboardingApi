using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Extensions;
using OnboardingApi.Persistence.Context;
using OnboardingApi.Persistence.Repositories;
using OnboardingApi.Services;

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

builder.Services.AddMemoryCache();

//repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITotverRepository, TotverRepository>();
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IOnboardingRepository, OnboardingRepository>();
builder.Services.AddScoped<IAtividadeOnboardingRepository, AtividadeOnboardingRepository>();

//serviços
builder.Services.AddScoped<ITotverService, TotverService>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IOnboardingService, OnboardingService>();
builder.Services.AddScoped<IAtividadeOnboardingService, AtividadeOnboardingService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
  app.UseCustomSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
