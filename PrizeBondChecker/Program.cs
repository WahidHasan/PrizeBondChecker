using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using PrizeBondChecker.Extensions;
using PrizeBondChecker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.TokenAuthentication(builder.Configuration);
builder.Services.AddServiceDependency();
builder.Services.AddInfrastructureService(builder.Configuration).AddMongoIdentity(builder.Configuration);

//builder.Services.Configure<PrizebondDbSettings>(builder.Configuration.GetSection(nameof(PrizebondDbSettings)));

//builder.Services.AddIdentity<Users, ApplicationRole>().
//    AddMongoDbStores<Users, ApplicationRole, Guid>(

//    );

//builder.Services.AddScoped<IPrizebondDbSettings>(sp =>
//    sp.GetRequiredService<IOptions<PrizebondDbSettings>>().Value);


// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<IAuthService, AuthService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
//builder.Services.AddSwaggerGen();

var app = builder.Build();
app.ConfigureExceptionHandler();
app.ConfigureCustomExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
