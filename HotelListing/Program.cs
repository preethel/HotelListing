using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//MyAdding
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);
});
builder.Host.UseSerilog();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" })
    );



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   
}
app.UseSwaggerUI();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();





Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "F:\\Project\\WebAPI\\HotelListing\\HotelListing\\log\\log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();
try
{
    Log.Information("Application Is starting");
    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Application is failed to start");

}
finally
{
    Log.CloseAndFlush();

}


