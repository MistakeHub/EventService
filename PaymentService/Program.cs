using EventService.Identity;
using IdentityModel.Client;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PaymentService;

var builder = WebApplication.CreateBuilder(args);
var appConfiguration= builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddSingleton<ICorsPolicyService>(serviceProvider =>
    new DefaultCorsPolicyService(serviceProvider.GetService<ILogger<DefaultCorsPolicyService>>())
    {
        AllowAll = true
    });

builder.Services.AddSingleton<BasePaymentService>();
var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
