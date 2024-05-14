using dotenv.net;
using GetAidBackend.Web;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfiguration();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(
               options =>
               {
                   options.Run(async context =>
                   {
                       context.Response.ContentType = "text/html";
                       var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                       if (null != exceptionObject)
                       {
                           var exception = exceptionObject.Error.GetType();
                           context.Response.StatusCode = exception.Name switch
                           {
                               "NotFoundException" => (int)HttpStatusCode.NotFound,
                               "UnauthorizedException" or "PasswordMismatchException" => (int)HttpStatusCode.Unauthorized,
                               _ => (int)HttpStatusCode.BadRequest,
                           };

                           var errorMessage = $"{exceptionObject.Error.Message}";
                           await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                       }
                   });
               }
           );

app.UseRouting();

app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();