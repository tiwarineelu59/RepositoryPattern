
using DogWrapperApi.Context;
using DogWrapperApi.Contracts;
using DogWrapperApi.Repository;

namespace DogWrapperApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IBreedRepository, BreedRepository>();
            builder.Services.AddControllers();

            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole(); // Example: Add console logging
                                      // Add other logging providers as needed
            });

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
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
