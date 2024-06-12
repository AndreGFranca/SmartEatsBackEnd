
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartEats.DataBase;
using SmartEats.Models.Users;
using SmartEats.Repositories.Confirms;
using SmartEats.Repositories.Justifies;
using SmartEats.Repositories.Menus;
using SmartEats.Repositories.Users;
using SmartEats.Seeds;
using SmartEats.Services.Confirms;
using SmartEats.Services.Justifies;
using SmartEats.Services.Menus;
using SmartEats.Services.Users;
using SmartEats.Services.Validators;
using System.Globalization;
using System.Net;
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
            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Pooling=true;MinPoolSize=0;MaxPoolSize=2;Connection Lifetime=15";
            Console.WriteLine($"{server} {port} {user} {password} {database}");
            // ConfigureServices method
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        //.WithOrigins("http://localhost:8081", "http://172.19.0.2:52707")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials()
                        );
            });


            // Add services to the container.
            builder.Services.AddDbContextPool<ApplicationDBContext>(opts =>
            {

                opts
                .UseLazyLoadingProxies()
                .UseMySQL(connectionString, option =>
                {
                    ServerVersion.AutoDetect(connectionString);
                })
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();                


            }, 2);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        SaveSigninToken = true
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministradorPolicy", policy => policy.RequireClaim("role", "Administrador"));
                options.AddPolicy("EmpresaPolicy", policy => policy.RequireClaim("role", "Empresa"));
                options.AddPolicy("FuncionarioPolicy", policy => policy.RequireClaim("role", "Funcionario"));
                options.AddPolicy("CozinhaPolicy", policy => policy.RequireClaim("role", "Cozinha"));
                options.AddPolicy("RHPolicy", policy => policy.RequireClaim("role", "RH"));
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                                        .Where(e => e.Value.Errors.Count > 0)
                                        .ToDictionary(
                                            e => e.Key,
                                            e => e.Value.Errors.Select(er => er.ErrorMessage).ToArray()
                                        );

                    var response = new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });
            ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

                //Configurar a autenticação no Swagger
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
                .AddIdentity<User, IdentityRole>(options =>
                {
                    // Permitir senhas simples inicialmente
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<TokenService>();

            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<IMenusRepository, MenusRepository>();

            builder.Services.AddScoped<ConfirmService>();
            builder.Services.AddScoped<IConfirmsRepository, ConfirmsRepository>();

            builder.Services.AddScoped<JustifyService>();
            builder.Services.AddScoped<IJustifiesRepository, JustifiesRepository>();

            var app = builder.Build();
            // Configure method
            app.UseCors("CorsPolicy");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();
            //Verifica as migrations
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Verificar conexão e aplicar migrações se necessário
                using (var context = services.GetRequiredService<ApplicationDBContext>())
                {
                    while (true)
                    {
                        try
                        {
                            if (context.Database.CanConnect())
                            {
                                break;
                            }
                        }
                        catch
                        {
                            Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                        }
                    }

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                } // Conexão fechada ao sair do bloco using

                // Semente inicializadora
                using (var context = services.GetRequiredService<ApplicationDBContext>())
                {
                    SeedCompany.Initialize(services, context);
                } // Conexão fechada ao sair do bloco using

                // Inicializar usuários
                using (var context = services.GetRequiredService<ApplicationDBContext>())
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    SeedUsers.Initialize(services, userManager, context);
                } // Conexão fechada ao sair do bloco using
            }
            app.Run();
        }
    }
}
