using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;
using RestAPI.Models.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(opt => 
    {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer=AuthOptions.ISSUER,
            ValidateAudience =true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime =true,
            IssuerSigningKey = AuthOptions.GetSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddScoped<ICommonService<Good>,GoodService>();
builder.Services.AddScoped<ICommonService<Person>,PersonService>();
builder.Services.AddScoped<ICommonService<Sale>, SaleService>();
builder.Services.AddScoped<ICommonService<Service>, ServiceService>();
builder.Services.AddScoped<ICommonService<VendingMachine>, VendingMachineService>();
var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Run();
