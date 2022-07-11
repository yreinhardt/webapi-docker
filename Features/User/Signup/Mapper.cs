using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Signup
{
    public class UserMapper : Mapper<Request, Response, User>
    {
        
        // mapping from request to entity user 
        public override User ToEntity(Request r) => new()
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            UserName = r.UserName,
            PasswordHash = BCryptNet.HashPassword(r.Password) // using BCrypt to secure user password as hash
        };

        // create a response object from user class
        public override Response FromEntity(User e) => new()
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            UserName = e.UserName,
            PasswordHash = e.PasswordHash
        };
    };

}
