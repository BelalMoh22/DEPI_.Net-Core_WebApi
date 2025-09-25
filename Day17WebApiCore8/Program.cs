
namespace Day17WebApiCore8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a builder for the web application
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(); // here we are adding controllers support so it will know that i will work as Web API
            //builder.Services.AddControllersWithViews();  // here we are adding controllers with views support so it will know that i will work as MVC
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Swagger is a tool for generating documentation and testing UI for your Web APIs.
            // It provides a user-friendly interface to explore and interact with the API endpoints.
            builder.Services.AddEndpointsApiExplorer(); // This service is responsible for generating metadata about the API endpoints in your application.
            builder.Services.AddSwaggerGen(); // This service is responsible for generating Swagger/OpenAPI documentation for your API.

            // Build the application
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) // This condition checks if the application is running in the development environment
            {
                // If the application is running in the development environment, enable Swagger middleware
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseHttpsRedirection();

            // Enable authorization middleware to enforce authorization policies.
            app.UseAuthorization();

            // Map controller routes to the application
            app.MapControllers();

            app.Run();
        }
    }
}
