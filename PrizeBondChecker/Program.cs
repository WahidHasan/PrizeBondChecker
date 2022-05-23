using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Models;
using PrizeBondChecker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PrizebondDbSettings>(builder.Configuration.GetSection(nameof(PrizebondDbSettings)));
//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().
//    AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(

//    );
builder.Services.AddServiceDependency();
builder.Services.AddHostedService<SchedulerService>();
builder.Services.AddScoped<IPrizebondService, PrizebondService>();

builder.Services.AddSingleton<IPrizebondDbSettings>(sp =>
    sp.GetRequiredService<IOptions<PrizebondDbSettings>>().Value);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
