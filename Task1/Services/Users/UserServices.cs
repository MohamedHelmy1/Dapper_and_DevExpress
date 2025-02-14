using Dapper;
using System.Data;
using System.Data.Common;
using Task1.DataModel;

namespace Task1.Services.Users
{
    public class UserServices: IUserService
    {
        private readonly IDbConnection db;

        public UserServices(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<bool> Login(string userName, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            var result = db.QueryFirstOrDefault<User>(sql, new { Username = userName, Password = password });
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
