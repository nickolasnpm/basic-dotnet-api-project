namespace UdemyProject.Repositories
{
    public interface IDifficultyRepository
    {
        Task<IEnumerable<Models.Domain.DifficultyDomain>> GetAllAsync();
        Task<Models.Domain.DifficultyDomain> GetAsync(Guid Id);
        Task<Models.Domain.DifficultyDomain> AddAsync(Models.Domain.DifficultyDomain DifficultyDomain);
        Task<Models.Domain.DifficultyDomain> UpdateAsync(Guid Id, Models.Domain.DifficultyDomain DifficultyDomain);
        Task<Models.Domain.DifficultyDomain> DeleteAsync(Guid Id);
    }
}
