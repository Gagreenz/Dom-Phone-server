using Dom_Phone_server.Models.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Dom_Phone_server.Services.AccountService;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Dom_Phone_server.Services.TokenService;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Dom_Phone_server.Services;
using Dom_Phone_server.Services.PaymentService;

namespace Dom_Phone_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetSection("AppSettings:DefaultConnection").Value;
            if (connection == null) throw new Exception("Connection string is null. Check appsettings.json to solve this.");
            builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(connection));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(typeof(AppMappingProfile));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Security:AccessKey").Value))
                    };
                }); 
            builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "bearer {token}",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                });

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddSingleton<ITokenService,TokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<PaymentRepository>();
            

            var app = builder.Build();
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}