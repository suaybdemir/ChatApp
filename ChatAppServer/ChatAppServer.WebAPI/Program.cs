using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Hubs;
using DefaultCorsPolicyNugetPackage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddDefaultCors();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")

));
builder.Services.AddControllers();
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

var filesPath = Path.Combine(builder.Environment.ContentRootPath, "Files");
if (!Directory.Exists(filesPath))
{
    Directory.CreateDirectory(filesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
        
FileProvider = new PhysicalFileProvider(
           filesPath),
    RequestPath = "/StaticFiles"
}

);


app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");
app.Run();
