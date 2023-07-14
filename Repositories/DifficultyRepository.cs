using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Models.Domain;

namespace UdemyProject.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly DBContextClass _dbContextClasses;
        public DifficultyRepository(DBContextClass dBContextClasses)
        {
            _dbContextClasses = dBContextClasses;
        }

        public async Task<IEnumerable<DifficultyDomain>> GetAllAsync()
        {
            return await _dbContextClasses.DifficultyTable.ToListAsync();
        }

        public async Task<DifficultyDomain> GetAsync(Guid UserId)
        {
            return await _dbContextClasses.DifficultyTable.FirstOrDefaultAsync(x => x.Id == UserId);
                
        }

        public async Task<DifficultyDomain> AddAsync(DifficultyDomain DifficultyDomain)
        {
            DifficultyDomain.Id = Guid.NewGuid();
            await _dbContextClasses.DifficultyTable.AddAsync(DifficultyDomain);
            await _dbContextClasses.SaveChangesAsync();
            return DifficultyDomain;
        }

        public async Task<DifficultyDomain> UpdateAsync(Guid UserId, DifficultyDomain DifficultyDomain)
        {
            DifficultyDomain? existingDifficulty = await _dbContextClasses.DifficultyTable.FindAsync(UserId);

            if (existingDifficulty != null)
            {
                existingDifficulty.Code = DifficultyDomain.Code;
                await _dbContextClasses.SaveChangesAsync();
            }

            return existingDifficulty;
        }

        public async Task<DifficultyDomain> DeleteAsync(Guid UserId)
        {
            DifficultyDomain? DifficultyDomain = await _dbContextClasses.DifficultyTable.FindAsync(UserId);

            if (DifficultyDomain != null)
            {
                _dbContextClasses.DifficultyTable.Remove(DifficultyDomain);
                await _dbContextClasses.SaveChangesAsync();
            }
            return DifficultyDomain;
        }
    }
}
 