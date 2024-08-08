
using KashewCheese.API;
using KashewCheese.API.Extensions;
using KashewCheese.API.Middlewares;
using KashewCheese.Application;
using KashewCheese.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
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
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

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

/*builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:7220").AllowAnyMethod().AllowAnyHeader();
    }));*/

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");


/*app.UseHttpsRedirection();*/
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PermissionMiddleware>();
app.UseRouter(app.UseRouterMiddleware());
app.MapControllers();
app.Run();
