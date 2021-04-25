using ProjectTemplate.Query.Dto.Users;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectTemplate.Query.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly string _dbCconnectionString;

        public UserRepository(string dbConnectionString)
        {
            _dbCconnectionString = dbConnectionString;
        }

        public async Task<UserProfileReadModel> GetProfileInformationAsync(Guid userId)
        {
            using var db = new SqlConnection(_dbCconnectionString);

            return await db.QueryFirstAsync<UserProfileReadModel>("select * from users where id = @id", userId);
        }
    }
}
