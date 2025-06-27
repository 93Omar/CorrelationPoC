using Shared;
using System.Text.Json;
using WebApplicationSender.Services;

namespace WebApplicationSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IServiceCollection services = builder.Services;

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();
            services.AddTransient<CorrelationDelegatingHandler>();

            services
                .AddHttpClient<SenderService>((httpClient) =>
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7052/");
                })
                .AddHttpMessageHandler<CorrelationDelegatingHandler>();

            var app = builder.Build();

            app.UseMiddleware<CorrelationMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
