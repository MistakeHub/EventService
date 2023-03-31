using EventService.ObjectStorage.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.что это
var services = builder.Services;
services.AddCors();
services.AddServices(builder.Configuration);

var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseHttpLogging();
app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();


app.Run();