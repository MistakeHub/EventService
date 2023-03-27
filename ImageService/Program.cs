using EventService.Identity;
using IdentityModel.Client;
using ImageService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using SC.Internship.Common.ScResult;
Thread.Sleep(5000);
var builder = WebApplication.CreateBuilder(args);
var appConfiguration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.Authority = "http://localhost:5000";
        // ReSharper disable once StringLiteralTypo �������� ����� myApi
        options.Audience = "myapi";
        options.RequireHttpsMetadata = false;
        options.ForwardDefaultSelector = Selector.ForwardReferenceToken();
    }).AddOAuth2Introspection("introspection", options =>
{
    options.Authority = "http://localhost:5000";
    // ReSharper disable once StringLiteralTypo �������� ����� hardToGuess
    // ReSharper disable once StringLiteralTypo �������� ����� myApi
    options.ClientSecret = "hardtoguess"; options.ClientId = "myapi";
    options.DiscoveryPolicy = new DiscoveryPolicy { RequireHttps = false };
});
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IHostedService>(_ => new RabbitMqBackGround(appConfiguration["RabbitMq:Queyename"]!,
    appConfiguration["RabbitMq:Hostname"]!, appConfiguration["RabbitMq:Username"]!, appConfiguration["RabbitMq:Password"]!,
    int.Parse(appConfiguration["RabbitMq:Port"]!), appConfiguration["RabbitMq:Virtualhost"]!));


var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var images = new[]
{
    new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d") 
};

app.MapGet("/images/isimageexists/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)](string id) =>
    {
       var result= new ScResult<bool>
        {
            Result = images.Any(v => v == Guid.Parse(id))

        };
       return result;

    })
.WithName("IsImageExists")
.WithOpenApi();

app.Run();

