namespace Healthcheck
{
    public class Endpoint : EndpointWithoutRequest<Response>
    {
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/healthcheck");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken c)
        {
            // sending a new instance of a response dto populated with a custom message.
            await SendAsync(new Response());
        }
    }
}