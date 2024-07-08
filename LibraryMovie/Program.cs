using AutoMapper;
using FluentValidation.AspNetCore;
using LibraryMovie.Data;
using LibraryMovie.DTOs;
using LibraryMovie.Models;
using LibraryMovie.Repository;
using LibraryMovie.Repository.Interface;
using LibraryMovie.Validators;
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
            string chaveSecreta = "988b98fc-a834-4fbb-b58f-ceeee47a0463"; 

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMemoryCache();

            builder.Services.AddControllers()
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<UserValidator>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<MovieValidator>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CategoryValidator>());



            #region DBConnection
            var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging(true)
            );
            #endregion
 
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

                c.CreateMap<UsersModel, UserDto>();
                c.CreateMap<UserDto, UsersModel>();

                c.CreateMap<MoviesModel, MoviesDto>();
                c.CreateMap<MoviesDto, MoviesModel>();

                c.CreateMap<CategoryModel, CategoryDto>();
                c.CreateMap<CategoryDto, CategoryModel>();

            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion

            #region JWT
            
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
            builder.Services.AddSwaggerGen(c =>
            {

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "LibraryMovie",
                    Description = "",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }

                };

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema, new string[]{} }
                });

                // Adicionar suporte a comentários XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "LibraryMovie",
                    ValidAudience = "minha_aplicacao",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
                };
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseApiVersioning(); 

            app.MapControllers();

            app.Run();
        }
    }
}
