using System.Net.Mime;
using Application.DataModels;
using Application.Interfaces;
using Application.Queries.WeatherForecast;
using Application.Services.OpenWeatherAPI;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IWeatherRequest, CurrentWeatherData>();
builder.Services.AddScoped<IRequestHandler<GetCurrentForecastQuery, WeatherDataResponse>, GetCurrentWeatherForecastQueryHandler>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(MediaTypeNames.Application).Assembly);
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endPoints =>
{
    endPoints.MapControllers();
});



app.Run();