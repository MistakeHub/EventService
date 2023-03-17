using EventService.Models.Interfaceimplements;
using EventService.Models.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace EventService.Helpers
{
    public static class ServiceRegister
    {
        public static void AddServices(this IServiceCollection services, IConfiguration appconfig)
        {

            var dasdas = appconfig["Key"];

            services.AddControllers().AddFluentValidation(options => { options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                       
                        ValidateIssuer = true,
                        
                        ValidIssuer = appconfig["Issuer"],

                     
                        ValidateAudience = true,
                        
                        ValidAudience = appconfig["Audience"],
                       
                        ValidateLifetime = true,

                       
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appconfig["Key"])),
                      
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR((opt) => { opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddAutoMapper(typeof(Program));
            services.AddSingleton<IBaseEventService, BaseEventService>();
            services.AddSingleton<IBaseImageService, BaseImageService>();
            services.AddSingleton<IBaseSpaceService, BaseSpaceService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
