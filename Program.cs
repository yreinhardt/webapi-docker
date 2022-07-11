global using FastEndpoints;
global using FluentValidation;
global using FastEndpoints.Security; 
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAuthenticationJWTBearer("JwtSigningKey");
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi(); 
app.UseSwaggerUi3(c => c.ConfigureDefaults());
app.Run();
