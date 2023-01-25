using Microsoft.EntityFrameworkCore;
using RSS.Infrastructure.Options;
using RSS.Infrastructure.Persistence;

namespace RSS.API.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
    }

    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionOptions = configuration.GetSection(DbConnectionOptions.SectionName)
                                             .Get<DbConnectionOptions>();

        services.AddDbContext<RssContext>(opt => opt.UseSqlServer(connectionOptions.RssContext));
    }
}