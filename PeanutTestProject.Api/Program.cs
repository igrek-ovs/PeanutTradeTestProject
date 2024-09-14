using PeanutTestProject.Core.DI;

namespace PeanutTestProject.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();

        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddProjectServices();
        
        builder.Services.AddHttpClient();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseCors(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
        
        app.MapControllers();
        
        app.Run();
    }
}