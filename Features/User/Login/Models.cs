namespace Login
{
    public class Request
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required!");
        }
    }

    public class Response
    {
        public string UserName { get; set; }
        public string Jwt { get; set; }
    }

    // user class only for local testing without db connection. will be deleted later
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}