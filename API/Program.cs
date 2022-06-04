using API.Middleware;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.ApplicationServiceExtensions();
builder.Services.AddGenericRepository();
builder.Services.AddSeederService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
await app.Seed(app.Services.CreateScope().ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithExposedHeaders();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
