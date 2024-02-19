
using DogWrapperApi.Context;
using DogWrapperApi.Contracts;
using DogWrapperApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

            //token based Authentication 
            //        builder.Services.AddSwaggerGen(option =>
            //        {
            //            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Dog API", Version = "v1" });
            //            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //            {
            //                In = ParameterLocation.Header,
            //                Description = "Please enter a valid token",
            //                Name = "Authorization",
            //                Type = SecuritySchemeType.Http,
            //                BearerFormat = "JWT",
            //                Scheme = "Bearer"
            //            });
            //            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type=ReferenceType.SecurityScheme,
            //                Id="Bearer"
            //            }
            //        },
            //        new string[]{}
            //    }
            //});
            //        });

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = false,
            //        ValidIssuer = "issuer", // Replace with your issuer
            //        ValidAudience = "audience", // Replace with your audience
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dogapi-secret-key")) // Replace with your secret key
            //    };
            //});



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
