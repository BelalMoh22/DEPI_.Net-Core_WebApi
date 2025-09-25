
using Microsoft.EntityFrameworkCore;

namespace Day17WebApiCore9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // here we add the DbContext service to the DI (DI is Dependency Injection) container
            // here i make the connection string in the appsettings.json file and link to it when the DbContext service is added and program will read it from there
            builder.Services.AddDbContext<Models.WebApiDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            // here we add the EndpointsApiExplorer service
            builder.Services.AddEndpointsApiExplorer();
            // here we add the SwaggerGen (Swagger Generatator) service
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
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
