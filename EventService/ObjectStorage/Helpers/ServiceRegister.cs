using EventService.Models.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;

using EventService.Identity;
using IdentityModel.Client;
using IdentityServer4.Services;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventService.Infrastructure.InterfaceImplements;

namespace EventService.ObjectStorage.Helpers;

/// <summary>
/// Класс расширения, предназначенный для регистрации сервисов
/// </summary>
public static class ServiceRegister
{
    /// <summary>
    /// Метод расширения, предназначенный для регистрации сервисов
    /// </summary>
    public static void AddServices(this IServiceCollection services)
    {



#pragma warning disable CS0618
        services.AddControllers().AddFluentValidation(options => { options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
#pragma warning restore CS0618
        services.AddSingleton(new MongoClient("mongodb://127.0.0.1:27018"));
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
#pragma warning disable CS0618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore CS0618
        services.AddIdentityServer(options =>
            {
                options.IssuerUri = "http://127.0.0.1:5000";
            })
            .AddDeveloperSigningCredential() // use a valid signing cert in production
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddInMemoryClients(Config.GetClients());
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
                options.Authority = "http://localhost:5000";
                // ReSharper disable once StringLiteralTypo решарпер хочет myApi
                options.Audience = "myapi";
                options.RequireHttpsMetadata = false;
                options.ForwardDefaultSelector = Selector.ForwardReferenceToken();
            }).AddOAuth2Introspection("introspection", options =>
        {
            options.Authority = "http://localhost:5000";
            // ReSharper disable once StringLiteralTypo решарпер хочет hardToGuess
            // ReSharper disable once StringLiteralTypo решарпер хочет myApi
            options.ClientSecret = "hardtoguess"; options.ClientId = "myapi";
            options.DiscoveryPolicy = new DiscoveryPolicy { RequireHttps = false };
        });
        services.AddSingleton<ICorsPolicyService>(serviceProvider =>
            new DefaultCorsPolicyService(serviceProvider.GetService<ILogger<DefaultCorsPolicyService>>())
            {
                AllowAll = true
            });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(opt => { opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddAutoMapper(typeof(Program));
        services.AddSingleton<IBaseEventService, EventMongoDbService>();
        services.AddSingleton<IBaseImageService, BaseImageService>();
        services.AddSingleton<IBaseSpaceService, BaseSpaceService>();
        services.AddSingleton<IBaseUserService, BaseUserService>();




   

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }

}