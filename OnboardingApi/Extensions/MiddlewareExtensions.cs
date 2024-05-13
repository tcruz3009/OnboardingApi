using Microsoft.OpenApi.Models;
using System.Reflection;

namespace OnboardingApi.Extensions
{
  public static class MiddlewareExtensions
  {
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(cfg =>
      {
        cfg.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Onboarding API",
          Version = "v5",
          Description = "Simple RESTful API built with ASP.NET Core.",
          Contact = new OpenApiContact
          {
            Name = "Tiago Cruz",
            Url = new Uri("https://tcruz3009.github.io/")
          },
          License = new OpenApiLicense
          {
            Name = "MIT",
          },
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        cfg.IncludeXmlComments(xmlPath);
      });
      return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
      app.UseSwagger().UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Onboarding API");
        options.DocumentTitle = "Onboarding API";
      });
      return app;
    }
  }
}
