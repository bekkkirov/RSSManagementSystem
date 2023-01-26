using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSS.API.Services;
using RSS.Application.Interfaces.Repositories;
using RSS.Application.Interfaces.Services;
using RSS.Infrastructure.Identity;
using RSS.Infrastructure.Identity.Entities;
using RSS.Infrastructure.Mapping;
using RSS.Infrastructure.Options;
using RSS.Infrastructure.Persistence;
using RSS.Infrastructure.Persistence.DataAccess;
using RSS.Infrastructure.Services;

namespace RSS.API.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddRepositories();
        services.AddAutoMapper();
        services.AddApplicationOptions(configuration);
        services.AddApplicationServices();
        services.AddJwt(configuration);
        services.AddIdentity();
    }

    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionOptions = configuration.GetSection(DbConnectionOptions.SectionName)
                                             .Get<DbConnectionOptions>();

        services.AddDbContext<RssContext>(opt => opt.UseSqlServer(connectionOptions.RssContext));
        services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(connectionOptions.IdentityContext));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IChannelImageRepository, ChannelImageRepository>();
        services.AddTransient<IChannelRepository, ChannelRepository>();
        services.AddTransient<IFeedItemRepository, FeedItemRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
    }

    public static void AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddSingleton<NewsBackgroundService>();
    }


    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(JwtOptions.SectionName)
                                   .Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = options.Issuer,
                        ValidateIssuer = true,

                        ValidAudience = options.Audience,
                        ValidateAudience = true,

                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                    };
                });
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<UserIdentity>(opt =>
                {
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 5;
                })
                .AddRoles<UserRole>()
                .AddRoleManager<RoleManager<UserRole>>()
                .AddSignInManager<SignInManager<UserIdentity>>()
                .AddRoleValidator<RoleValidator<UserRole>>()
                .AddEntityFrameworkStores<IdentityContext>();
    }
}