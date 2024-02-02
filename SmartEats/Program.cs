
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartEats.DataBase;
using SmartEats.Models.Users;
using SmartEats.Repositories;
using SmartEats.Seeds;
using SmartEats.Services.Users;
using System.Text;

namespace SmartEats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();

            var server = Environment.GetEnvironmentVariable("DbServer") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DbPort") ?? "3306";
            var user = Environment.GetEnvironmentVariable("DbUser") ?? "app";
            var password = Environment.GetEnvironmentVariable("Password") ?? "12345678";
            var database = Environment.GetEnvironmentVariable("Database") ?? "smarteat";
            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDBContext>(opts => {

                opts
                .UseLazyLoadingProxies()
                .UseMySQL(connectionString);
                
                //(connectionString, ServerVersion.AutoDetect(connectionString),
                //     b => b.MigrationsAssembly("SmartEats")
                //);
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            //builder.Services.AddAuthorization(opts =>
            //{
            //    opts.AddPolicy("IdadeMinima", policy =>
            //    {
            //        policy.AddRequirements(new IdadeMinima(18));
            //    });
            //});

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

                //Configurar a autentica��o no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                                            {
                                                new OpenApiSecurityScheme
                                                {
                                                    Reference = new OpenApiReference
                                                    {
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = "Bearer"
                                                    }
                                                },
                                                new string[] { }
                                            }
                                        });
            });
            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TokenService>();

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
            //Verifica as migrations
            using (var scope = app.Services.CreateScope())
            {
                
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDBContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                SeedCompany.Initialize(services, context);
            }
            app.Run();
        }
    }
}
