namespace UdemyProject.Repositories
{
    public interface IImageRepository
    {
        Task<Models.Domain.ImageDomain> Upload (Models.Domain.ImageDomain imageDomain);
        Task<IEnumerable<Models.Domain.ImageDomain>> DownloadAll();
        Task<Models.Domain.ImageDomain> DownloadById(Guid Id);
        Task<Models.Domain.ImageDomain> Update(Guid Id, Models.Domain.ImageDomain imageDomain);
        Task<Models.Domain.ImageDomain> Delete(Guid Id);
    }
}