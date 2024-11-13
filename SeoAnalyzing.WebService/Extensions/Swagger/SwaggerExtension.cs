using Microsoft.OpenApi.Models;

namespace SeoAnalyzing.WebService.Extensions.Swagger
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SeoAnalyzing.WebService", Version = "v1" });
            });
        }

        public static void UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeoAnalyzing.WebService v1"));
        }
    }
}
