using Users.Models;

namespace Users.Interface
{
    public interface IUsers
    {
        Task<Model> GetAll(int UserId);
        Task<int> UserRegisteration(RegisterModel Credentials);
        Task<LoginResponse> UserLogin(LoginModel Credentials);
    }
}
