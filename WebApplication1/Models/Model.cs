namespace Users.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public string Email { get; set; }
    }

    public class RegisterModel
    {
        
        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public string Email { get; set; }
        public string Hashpassword { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public bool IsSuccess { get; set; }
        public Model Data {  get; set; }
        public string Token {  get; set; }

    }


    public class Response
    {
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
