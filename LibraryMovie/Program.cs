
using AutoMapper;
using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository;
using LibraryMovie.Repository.Interface;
using LibraryMovie.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            #region addiction
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMoviesRepository, MovieRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region configuration Mapper
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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}
