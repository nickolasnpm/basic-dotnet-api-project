namespace UdemyProject.Repositories
{
    public interface IUserRepository
    {
        Task<Models.Domain.UsersDomain> RegisterUser(Models.Domain.UsersDomain user);
        Task<Models.Domain.UsersDomain> AuthenticateAsync(string username);
        Task<Models.Domain.RolesDomain> RegisterRole(Models.Domain.RolesDomain role);
        Task<Models.Domain.UsersRolesDomain> RegisterUserRole(Models.Domain.UsersRolesDomain userRole);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        
    }
}
