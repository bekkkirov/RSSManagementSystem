using Microsoft.EntityFrameworkCore;
using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(RssContext context) : base(context)
    {
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);
    }
}