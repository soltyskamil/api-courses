
using API_Managment_Courses.Filters;
using API_Managment_Courses.Interfaces;
using API_Managment_Courses.Mapping;
using API_Managment_Courses.Models.Api;
using API_Managment_Courses.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API_Managment_Courses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("database");

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddScoped<ICourseServices, CoursesServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ILessonsServices, LessonsServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<ValidationFilterAttribute>();
            builder.Services.AddScoped<GlobalExceptionFilter>();
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    policy => policy.WithOrigins("http://localhost:5173") // Dodaj URL swojego frontendu
                                   .AllowAnyMethod()
                    .AllowAnyHeader());
            });





            builder.Services.AddControllers(options =>
            {
                options.Filters.AddService<ValidationFilterAttribute>();
                options.Filters.AddService<GlobalExceptionFilter>();
            }
            );


            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.AddSingleton(jwtSettings);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                              .AddJwtBearer("scheme", jwtOptions =>
                                              {
                                                  jwtOptions.RequireHttpsMetadata = false;
                                                  jwtOptions.SaveToken = true;
                                                  jwtOptions.Audience = builder.Configuration["Jwt:audience"];
                                                  jwtOptions.TokenValidationParameters = new TokenValidationParameters
                                                  {
                                                      ValidateIssuer = false,
                                                      ValidateAudience = true,
                                                      ValidateIssuerSigningKey = true,
                                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"])),
                                                      ValidateLifetime = true,

                                                  };

                                                  jwtOptions.Events = new JwtBearerEvents
                                                  {
                                                      OnMessageReceived = ctx =>
                                                      {
                                                          if (string.IsNullOrEmpty(ctx.Token) && ctx.Request.Cookies.TryGetValue("jwt-token", out var token))
                                                          {
                                                              ctx.Token = token;
                                                          }
                                                          return Task.CompletedTask;

                                                      }
                                                  };
                                              });
            builder.Services.AddAuthorization();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .SetIsOriginAllowed(origin => true)
                                .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
