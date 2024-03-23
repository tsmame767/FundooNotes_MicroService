using Microsoft.AspNetCore.Mvc;
using Users.Interface;
using Users.Models;

namespace User.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a route template for your controller
    public class UserController : ControllerBase
    {
        private readonly IUsers service;

        public UserController(IUsers service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<Model> GetAll(int UserId)
        {
            try
            {
                var list = await service.GetAll(UserId); // Assuming you have a method GetAllAsync in your service
                if (list == null)
                {
                    return null;
                }

                return list;
            }
            catch
            {
                // It's good practice to handle potential errors
                return new Model { };
            }
        }

        [HttpPost]
        public async Task<Response> UserRegister(RegisterModel Credentials)
        {
            var res = await service.UserRegisteration(Credentials);
            if (res == 0)
            {
                return new Response { IsSuccess = false, Status=404, StatusMessage="not found" };
            }
            return new Response { IsSuccess = true, Status = 201, StatusMessage = "User Registered" };     

        }

        [HttpPost("UserLogin")]
        public async Task<LoginResponse> UserLogin(LoginModel Credentials)
        {
            var res = await service.UserLogin(Credentials);
            if (res == null)
            {
                return new LoginResponse { IsSuccess = false, Status = 404, StatusMessage = "not found" };
            }
            return new LoginResponse { IsSuccess = true, Status = 200, StatusMessage = "User Logged In",Data=res.Data,Token=res.Token };
        }
    }

}
    


