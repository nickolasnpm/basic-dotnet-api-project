namespace UdemyProject.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(Models.Domain.UsersDomain userDomain);
    }
}
