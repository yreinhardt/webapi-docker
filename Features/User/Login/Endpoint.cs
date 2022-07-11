

namespace Login
{
    public class Endpoint : Endpoint<Request, Response>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/user/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            var user = new User {
                Email = "test@gmail.com",
                FirstName = "Peter",
                LastName = "Martin",
                UserName = "foo",
                PasswordHash = "$2a$11$9CapNuJCcfih2kGbBtx9auj/BGrKSzMaKmDmlNw/JDLhUwoltiyA2" //password: "string"
            };

            if (!BCrypt.Net.BCrypt.Verify(r.Password, user.PasswordHash))
                ThrowError("Password is incorrect!");

            // create jwt
            var token = JWTBearer.CreateToken(
                signingKey: "JwtSigningKey",
                expireAt: DateTime.UtcNow.AddDays(1));

            Response.UserName = user.UserName;
            Response.Jwt= token;

            await SendAsync(Response);
            
        }
    }
}