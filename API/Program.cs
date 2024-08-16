using static API.Extensions.ApplicationServiceExtensions;
using static API.Extensions.IdentityServiceExtensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseAuthentication(); // Nr. 1
app.UseAuthorization(); // Nr. 2
app.MapControllers();
app.Run();
