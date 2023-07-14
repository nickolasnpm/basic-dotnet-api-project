namespace UdemyProject.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Models.Domain.RegionDomain>> GetAllAsync();
        Task<Models.Domain.RegionDomain> GetAsync(Guid Id);
        Task<Models.Domain.RegionDomain> AddAsync(Models.Domain.RegionDomain regionDomain);
        Task<Models.Domain.RegionDomain> DeleteAsync(Guid Id);
        Task<Models.Domain.RegionDomain> UpdateAsync(Guid Id, Models.Domain.RegionDomain regionDomain);
    }
}
