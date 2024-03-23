using Azure.Core;
using Dapper;
using User.ContextDB;
using User.JWT;
using Users.Interface;
using Users.Models;

namespace Users.Service
{
    public class UsersService : IUsers
    {
        private readonly DBContext context;
        private readonly IConfiguration _config;
        public UsersService(DBContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }
        //public async Task<List<Model>> GetAll(int UserId)
        //{
        //    var query = "select * from User_Details where id = @id";

        //    using (var connect = this.context.CreateConnection())
        //    {
        //        var res = await connect.QueryAsync<Model>(query, new { id = UserId });
        //        return res.ToList();
        //    }
        //}

        public async Task<Model> GetAll(int UserId)
        {
            var query = "select id,first_name,last_name,email from User_Details where id = @id";

            using (var connect = this.context.CreateConnection())
            {
                var res = await connect.QueryFirstOrDefaultAsync<Model>(query, new { id = UserId });
                
                return new Model { Id=res.Id, Email=res.Email, first_Name=res.first_Name, last_Name=res.last_Name};
            }
        }

        public async Task<int> UserRegisteration(RegisterModel Credentials)
        {
            var query = "insert into User_Details (first_name,last_name,email,hashpassword) values(@firstname,@lastname,@email,@hashpassword)";

            using(var connect = this.context.CreateConnection())
            {
                var res = await connect.ExecuteAsync(query, new {firstName = Credentials.first_Name, lastName = Credentials.last_Name, email = Credentials.Email, hashpassword= BCrypt.Net.BCrypt.HashPassword(Credentials.Hashpassword)});
                return res;
            }
        }

        public async Task<LoginResponse> UserLogin(LoginModel Credentials)
        {
            var query = "select * from User_Details where email=@email";

            using(var connect = this.context.CreateConnection())
            {   
                var res = await connect.QueryFirstOrDefaultAsync<Model>(query, new { email = Credentials.Email });
                var queryres = await connect.QueryFirstOrDefaultAsync<RegisterModel>(query, new { email = Credentials.Email });
                if (queryres != null && BCrypt.Net.BCrypt.Verify(Credentials.password, queryres.Hashpassword))
                {
                    TokenGenerator Token = new TokenGenerator(_config);
                    var LoginToken = Token.generateJwtToken(res.Id, res.Email);
                    
                    
                    return new LoginResponse { Token = LoginToken, Data = new Model { Id = res.Id, Email = res.Email, first_Name = res.first_Name, last_Name = res.last_Name } };
                    
                }
                return null;
            }
        }

        //public async Task<List<Model>> GetAll()
        //{
        //    var query = "SELECT * FROM Employee";
        //    using (var connect = this.context.CreateConnection())
        //    {
        //        var emplist = await connect.QueryAsync<Model>(query);
        //        return emplist.ToList();
        //    }
        //}
    }
}
