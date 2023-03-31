using EventService.Identity;
using IdentityModel.Client;
using ImageService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using SC.Internship.Common.ScResult;
var builder = WebApplication.CreateBuilder(args);
var appConfiguration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.Authority = appConfiguration["Identity:Authority"];
        // ReSharper disable once StringLiteralTypo решарпер хочет myApi
        options.Audience = appConfiguration["Identity:Audience"];
        options.RequireHttpsMetadata = false;
        options.ForwardDefaultSelector = Selector.ForwardReferenceToken();
    }).AddOAuth2Introspection("introspection", options =>
{
    options.Authority = appConfiguration["Identity:Authority"];
    // ReSharper disable once StringLiteralTypo решарпер хочет hardToGuess
    // ReSharper disable once StringLiteralTypo решарпер хочет myApi
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

