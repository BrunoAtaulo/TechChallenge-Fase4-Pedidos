
using Api.Middleware;
using App.Infra.CrossCutting.IoC;
using Application.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
// Needed for minimal APIs

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo

        {
            Title = "Lambda FIAP04 Pedidos -- https://docs.aws.amazon.com/pt_br/lambda/latest/dg/csharp-package-asp.html",
            Version = "v1",
            Description = "API for FIAP Lambda Pedidos Fase 04- Gerenciamento de Pedidos.",
            Contact = new OpenApiContact
            {
                Name = "G24",  // Replace with your name or team
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",  // Replace with your desired license
                Url = new Uri("https://opensource.org/licenses/MIT")  // Link to the license
            }
        });

    c.EnableAnnotations();
});

// Configure HttpClient to communicate API PAGAMENTOS
builder.Services.AddHttpClient<PedidosService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:6002/");  // URL da API de Pagamentos (ALTERAR)
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

//IOC
NativeInjectorBootStrapper.RegisterServices(builder.Services, builder.Configuration);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    string swaggerJsonbasePatch = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{swaggerJsonbasePatch}/swagger/v1/swagger.json", "Lambda V1");
    c.OAuthAppName("FIAP Lambda auth Fase 04");
    c.OAuthScopeSeparator(" ");
    c.OAuthUsePkce();
});



app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
