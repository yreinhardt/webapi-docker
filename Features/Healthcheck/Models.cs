namespace Healthcheck
{
    //EndpointWithoutRequest<Response>, no request dto but response
    public class Response
    {
        public string Status => "200, Health OK";
    }
}