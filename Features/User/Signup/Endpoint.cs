namespace Signup
{
    public class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/user/signup");
            AllowAnonymous(); // unauthenticated users are allowed to use this endpoint
        }

        public override async Task HandleAsync(Request r, CancellationToken c)
        {

            await SendAsync(new Response()
            {
                Message = "Successfully created user!",
                FirstName = r.FirstName,
                LastName = r.LastName,
                Email = r.Email,
                UserName = r.UserName,
                Password = r.Password

            });
        }
    }
}