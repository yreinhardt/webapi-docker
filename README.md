# webapi-docker

Containerize a minimal .NET 6 webapi with docker. Creating a secure docker image with a file size as small as possible. 

## Get started

1. Install docker on your system.

2. Clone repository
```bash 
git clone https://github.com/yreinhardt/webapi-docker.git
```
3. Create docker image. Tagging image as `webdocker-api:user-secure`
```bash 
docker build . -t webdocker-api:user-secure
```
4. Start container. Mapping port 2000 to port 80 within the container.
```bash 
docker run -it -p 2000:80 -e ASPNETCORE_ENVIRONMENT=Development webdocker-api:user-secure
```
5. Enjoy api with swagger. Currently only via http.
```bash 
http://localhost:2000/swagger
```

## Detailed description api

- Based on .NET 6 minimal webapi
- Following RPR Desing Pattern (REPR = Request-Endpoint-Response)
- Using light-weight REST Api framwork FastEndpoints for .NET6 that implements REPR Desing Pattern (https://fast-endpoints.com/index.html)
- Simplifies MVC Pattern to overcome bloated Controllers and not used Views
- Every endpoint class has one single handler with an optional request and response type (https://deviq.com/design-patterns/repr-design-pattern)


## Detailed description docker

- `webdocker-api:basic` consists of `mcr.microsoft.com/dotnet/aspnet:6.0` and `mcr.microsoft.com/dotnet/sdk:6.0` and using as default a debian base. 
- Debian distribution is quite big and shipping unnecessary stuff. 
- Following best practices the attack surface is reduced by using minimal images (`-alpine`).
- `/dotnet/runtime-deps` is used to get only runtime dependencies without installing .NET Core`.
- `webdocker-api:user-secure` consists `/dotnet/runtime-deps:6.0-alpine` and `/dotnet/sdk:6.0-alpine`
- For publishing a self-deployment is chosen. The whole application is bundled together. It does not depend on the installation of .NET Framework. The setup is easier.
```dockerfile
RUN dotnet publish "webapi-docker.csproj" -c Release -o /webapi_app/publish \
   -r alpine-x64 \
   --self-contained true \
   -p:PublishTrimmed=true
```
- A downside of this approach is a larger size. Because a pull does a complete copy of the runtime and framework.
- To overcome this problem trimming is used to reduce the size by trim unused assemblies as part of publishing.
- Follwing best practices unnecessary privileges are avoided by creating a user.
- The container entrypoint is not running as root.
- `webapiuser` has only executable permissions (`chmod -R +x /webapi_app`) over application folder and no ownership. Ownership stays with `root`.

```dockerfile
RUN adduser -u 1234 --disabled-password --gecos "" webapiuser && chmod -R +x /webapi_app
USER webapiuser
```
- Image size was reduced by a factor of 4

<center>

| OPTIMIZATION | REPOSITORY   |      TAG      |  SIZE   |
|----------|-------------:|------:|------:|
| before   | webdocker-api   |  basic | 216MB |
| after  | webdocker-api   |    user-secure   |   52.4MB |

</center>

Sources:

https://andrewlock.net/should-i-use-self-contained-or-framework-dependent-publishing-in-docker-images/

https://hub.docker.com/_/microsoft-dotnet-aspnet/?tab=description

https://docs.docker.com/develop/develop-images/dockerfile_best-practices/

https://devblogs.microsoft.com/dotnet/app-trimming-in-net-5/