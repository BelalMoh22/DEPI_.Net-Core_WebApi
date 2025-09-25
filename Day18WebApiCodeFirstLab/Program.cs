
using Microsoft.EntityFrameworkCore;

namespace Day18WebApiCodeFirstLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // Connect the Api with the database using EF Core we need to add the connection string to the appsettings.json file
            //builder.Services.AddDbContext<>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add support for OpenAPI/Swagger  
            builder.Services.AddOpenApi();
            // Add swagger generation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.MapOpenApi();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                app.MapSwagger();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            // to make reverse engineering for existing database :
            //Scaffold-DbContext
            //"Data Source=Belal-2004;Initial Catalog=ADOTest;Integrated Security=True;Trust Server Certificate=True"
            //microsoft.entityframeworkcore.sqlserver -context AppDBContext -contextdir Data -outputdir Models -force
        }
    }
}
