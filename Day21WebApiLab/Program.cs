
using Day21WebApiLab.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Day21WebApiLab.Repositories;
using Day21WebApiLab.UnitOfWorks.Interfaces;
using Day21WebApiLab.UnitOfWorks.Implements;
using Day21WebApiLab.Services.Interfaces;
using Day21WebApiLab.Services.Implements;

namespace Day21WebApiLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mycors = "MyCors";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Add DBContext Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            // Cors
            builder.Services.AddCors(options =>
                options.AddPolicy(mycors , builder => {
                    // here any url
                    builder.AllowAnyOrigin();
                    // here for specified url 
                    //builder.WithOrigins("url");

                    // here any method
                    builder.AllowAnyMethod();
                    // here for specified methods
                   // builder.WithMethods("Get" , "Put");


                    builder.AllowAnyHeader();
                    }
                ));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Resolving Repoistory
            //builder.Services.AddSingleton();
            //builder.Services.AddScoped();
            //builder.Services.AddTransient();
            //builder.Services.AddScoped<DepartmentRepository>(); // Dependence injection 
            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>(); // Dependence Version

            // Resolving Services Repoistory
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                // Using For Swagger and Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(mycors);

            app.MapControllers();

            app.Run();
        }
    }
}