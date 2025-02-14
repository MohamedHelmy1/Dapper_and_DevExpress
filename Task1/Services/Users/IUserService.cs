namespace Task1.Services.Users
{
    public interface IUserService
    {
        Task<bool> Login(string userName, string password);
    }
}
