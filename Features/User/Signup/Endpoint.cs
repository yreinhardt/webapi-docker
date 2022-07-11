namespace Signup
{
    public class Endpoint : Endpoint<Request, Response, SignupMapper>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/user/signup");
            AllowAnonymous(); // unauthenticated users are allowed to use this endpoint
        }

        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            User entity = Map.ToEntity(r);
            Response = Map.FromEntity(entity);

            await SendAsync(Response);

            /*await SendAsync(new Response()
            {
                Message = "Successfully created user!",
                FirstName = r.FirstName,
                LastName = r.LastName,
                Email = r.Email,
                UserName = r.UserName,
                //PasswordHash = passwordHash,
                //PasswordSalt = passwordSalt

            });*/
        }
    }
}