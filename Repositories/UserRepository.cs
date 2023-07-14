using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using UdemyProject.Data;
using UdemyProject.Models.Domain;

namespace UdemyProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContextClass _dBContextClasses;
        public UserRepository(DBContextClass dBContextClasses)
        {
            _dBContextClasses = dBContextClasses;
        }

        public async Task<UsersDomain> RegisterUser(UsersDomain userDomain)
        {
            userDomain.Id = Guid.NewGuid();
            await _dBContextClasses.UsersTable.AddAsync(userDomain);
            await _dBContextClasses.SaveChangesAsync();
            return userDomain;
        }
        public async Task<RolesDomain> RegisterRole(RolesDomain roleDomain)
        {
            roleDomain.Id = Guid.NewGuid();
            await _dBContextClasses.RolesTable.AddAsync(roleDomain);
            await _dBContextClasses.SaveChangesAsync();
            return roleDomain;
        }

        public async Task<UsersRolesDomain> RegisterUserRole(UsersRolesDomain userRoleDomain)
        {
            userRoleDomain.Id = Guid.NewGuid();
            await _dBContextClasses.UsersRolesTable.AddAsync(userRoleDomain);
            await _dBContextClasses.SaveChangesAsync();
            return userRoleDomain;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<UsersDomain> AuthenticateAsync(string username)
        {

            UsersDomain? userDomain = await _dBContextClasses.UsersTable.FirstOrDefaultAsync(x =>
            EF.Functions.Collate(x.Username, "SQL_Latin1_General_CP1_CS_AS") == username);

            if (userDomain != null)
            {
                List<UsersRolesDomain> userRoles = await _dBContextClasses.UsersRolesTable.
                Where(x => x.UserID == userDomain.Id).ToListAsync();

                if (userRoles.Any())
                {
                    userDomain.Roles = new List<string>();

                    foreach (var userRole in userRoles)
                    {
                        RolesDomain? role = await _dBContextClasses.RolesTable.FirstOrDefaultAsync(x => x.Id == userRole.RoleID);

                        if (role != null)
                        {
                            userDomain.Roles.Add(role.Title);
                        }
                    }
                }
            }

            return userDomain;
        }
    }
}
