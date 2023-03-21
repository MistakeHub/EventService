using EventService.ObjectStorage.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.что это
var services = builder.Services;
services.AddCors();
services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapControllers();


app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.Run();