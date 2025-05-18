using Base_API.Entities;
using Base_API.Infrastructure.Context;
using Base_API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base_API.Infrastructure.Repositories
{
    public class UserRepository(MainDbContext db) : BaseRepository<User>(db), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Where(user => user.Email.Equals(email))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet
                .Where(user => user.Email.Equals(email))
                .AnyAsync();
        }
    }
}
