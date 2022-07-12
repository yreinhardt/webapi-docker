namespace Signup
{
    public class Request
    {
        // firstname, lastname optional can be null
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Your name is required!")
            .MinimumLength(2).WithMessage("Your name is too short!")
            .MaximumLength(25).WithMessage("Your name is too long!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your email address is required!")
                .EmailAddress().WithMessage("The format of your email address is wrong!");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("a username is required!")
                .MinimumLength(2).WithMessage("Your username is too short!")
                .MaximumLength(15).WithMessage("Your username is too long!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("a password is required!")
                .MinimumLength(6).WithMessage("Your password is too short!")
                .MaximumLength(25).WithMessage("Your password is too long!")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            
        }
    }

    public class Response
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string PasswordHash { get; set; }

    }

    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}