
using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.repository;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Library_Web_Application_NET.Server.src.statistics;
using Library_Web_Application_NET.Server.src.statistics.interfaces;
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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb")));

            builder.Services.AddTransient<DbInitializer>();

            // Repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
            builder.Services.AddScoped<IResourceInstanceRepository, ResourceInstanceRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
            builder.Services.AddScoped<IAuthorResourceRepository, AuthorResourceRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IResourceInstanceService, ResourceInstanceService>();
            builder.Services.AddScoped<IResourceService, ResourceService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();

            // Contollers
            builder.Services.AddControllers();


            // Cors Config

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

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

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
