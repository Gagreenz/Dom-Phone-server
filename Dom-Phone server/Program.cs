using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Services.AccountService;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dom_Phone_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetSection("appSetings")["DefaultConnection"];
            if (connection == null) throw new Exception("Connection string is null. Check appsettings.json to solve this.");
            builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(connection));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            //TODO: You must change CORS when you will deploy 
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors();

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