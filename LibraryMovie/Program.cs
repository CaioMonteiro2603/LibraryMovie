using AutoMapper;
using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository;
using LibraryMovie.Repository.Interface;
using LibraryMovie.Services;
using LibraryMovie.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

namespace LibraryMovie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            #region DBConnection
            var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging(true)
            );
            #endregion

            builder.Services.AddScoped<AuthenticationService>(); 
            #region DependencyInection
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMoviesRepository, MovieRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region MapperConfig 
            var mapperConfig = new AutoMapper.MapperConfiguration(c => {
                c.AllowNullCollections = true;
                c.AllowNullDestinationValues = true;

                c.CreateMap<UsersModel, LoginRequestVM>();
                c.CreateMap<LoginRequestVM, UsersModel>();

                c.CreateMap<LoginResponseVM, UsersModel>();
                c.CreateMap<UsersModel, LoginResponseVM>();

                c.CreateMap<UsersModel, UserResponseVM>();

            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion

            #region JWT
            bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
            {
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }

            var key = Encoding.ASCII.GetBytes(Settings.SECRET_TOKEN);

            builder.Services.AddAuthentication(a => {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = "library",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        LifetimeValidator = CustomLifetimeValidator, // forma de validar se o token está expirado
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
            #endregion

            #region versionAPI
            builder.Services.AddApiVersioning(options =>
            {
                options.UseApiBehavior = false;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(3, 0);
                options.ApiVersionReader =
                    ApiVersionReader.Combine(
                        new HeaderApiVersionReader("x-api-version"),
                        new QueryStringApiVersionReader(),
                        new UrlSegmentApiVersionReader());
            });

            builder.Services.AddVersionedApiExplorer(setup => {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion

            builder.Services.AddEndpointsApiExplorer();


            #region APIDocumentation
            builder.Services.AddSwaggerGen(options =>
            { 
            // Adicionar suporte a comentários XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            });
            #endregion
            var app = builder.Build();

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                // version in swagger
                app.UseSwaggerUI(c =>
                {
                    foreach (var d in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(
                            $"/swagger/{d.GroupName}/swagger.json",
                            d.GroupName.ToUpperInvariant());
                    }

                    c.DocExpansion(DocExpansion.List);
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseApiVersioning(); 

            app.MapControllers();

            app.Run();
        }
    }
}
