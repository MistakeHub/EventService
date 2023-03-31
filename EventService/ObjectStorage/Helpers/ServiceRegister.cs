using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using EventService.Features.Filters;
using EventService.Identity;
using IdentityModel.Client;
using IdentityServer4.Services;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventService.Infrastructure.InterfaceImplements;
using Polly;
using Polly.Extensions.Http;
using EventService.ObjectStorage.HttpService;
using EventService.ObjectStorage.RabbitMqService;
using MediatR;
using EventService.Infrastructure.Interfaces;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

namespace EventService.ObjectStorage.Helpers;

/// <summary>
/// Класс расширения, предназначенный для регистрации сервисов
/// </summary>
public static class ServiceRegister
{
    /// <summary>
    /// Метод расширения, предназначенный для регистрации сервисов
    /// </summary>
    public static void AddServices(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddSwaggerGen(setup =>
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

#pragma warning disable CS0618
        services.AddControllers().AddFluentValidation(options => { options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
#pragma warning restore CS0618
        services.AddTransient(typeof(IPipelineBehavior<,>) ,typeof(ValidationBehavior<,>));
        services.AddSingleton(new MongoClient(appConfiguration["Mongodb"]));
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
#pragma warning disable CS0618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore CS0618

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
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
        services.AddSingleton<ICorsPolicyService>(serviceProvider =>
            new DefaultCorsPolicyService(serviceProvider.GetService<ILogger<DefaultCorsPolicyService>>())
            {
                AllowAll = true
            });
 
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(opt => { opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddAutoMapper(typeof(Program));
        services.AddSingleton<HttpServiceClient>();
        services.AddSingleton<IBaseRabbitMqService>(_=>new BaseRabbitMqService(appConfiguration["RabbitMq:Hostname"]!, appConfiguration["RabbitMq:Username"]!, appConfiguration["RabbitMq:Password"]!, int.Parse(appConfiguration["RabbitMq:Port"]!), appConfiguration["RabbitMq:Virtualhost"]!));
        services.AddSingleton<IBaseEventService, EventMongoDbService>();
        services.AddSingleton<IBaseUserService, BaseUserService>();
  
        services.AddHttpClient();
        services.AddHttpClient("image", client =>
        {
            client.BaseAddress = new Uri(appConfiguration["Httpclient:Image"]!);
           client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddPolicyHandler(_ =>
        {
            return HttpPolicyExtensions.HandleTransientHttpError().OrResult(v => !v.IsSuccessStatusCode)
                .WaitAndRetryAsync(2, attempt => TimeSpan.FromSeconds(3 * attempt)); 
        });

        services.AddHttpClient("space",client=>
        {
            client.BaseAddress = new Uri(appConfiguration["Httpclient:Space"]!);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddPolicyHandler(_ =>
        {
            
            return HttpPolicyExtensions.HandleTransientHttpError().OrResult(v => !v.IsSuccessStatusCode)
                .WaitAndRetryAsync(2, attempt => TimeSpan.FromSeconds(3 * attempt)); 
        });

        services.AddHttpClient("payment", client =>
        {
            client.BaseAddress = new Uri(appConfiguration["Httpclient:Payment"]!);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddPolicyHandler(_ =>
        {
            return HttpPolicyExtensions.HandleTransientHttpError().OrResult(v => !v.IsSuccessStatusCode)
                .WaitAndRetryAsync(6, attempt => TimeSpan.FromSeconds(3 * attempt)); 
        });


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