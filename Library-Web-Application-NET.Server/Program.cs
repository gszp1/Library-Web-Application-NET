
using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.repository;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb")));

            builder.Services.AddTransient<DbInitializer>();

            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IResourceRepository, ResourceRepository>();
            builder.Services.AddTransient<IResourceInstanceRepository, ResourceInstanceRepository>();
            builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
            builder.Services.AddTransient<IPublisherRepository, PublisherRepository>();
            builder.Services.AddTransient<IAuthorResourceRepository, AuthorResourceRepository>();
            builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllers();


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var initialiser = services.GetRequiredService<DbInitializer>();
            initialiser.Run();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
