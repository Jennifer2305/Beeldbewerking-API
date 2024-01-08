using Microsoft.AspNetCore.DataProtection.Repositories;
using DogMatch_web_api.Repository;
using DogMatch_web_api.Service;
using DogMatch_web_api.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPictureEditStrategy, GrayscaleEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, BlackWhiteEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, InvertColorsEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, LightEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, DarkEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, SizeEditStrategy>();
builder.Services.AddScoped<IPictureEditStrategy, FormatPngEditStrategy>();
builder.Services.AddScoped<IBeeldbewerkingRepository, BeeldbewerkingRepository>();
builder.Services.AddScoped<IBeeldbewerkingService, BeeldbewerkingService>();

var client = new HttpClient();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
