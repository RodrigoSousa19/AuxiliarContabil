using System.Data;
using AuxiliarContabil.API.Extensions;
using AuxiliarContabil.API.Middlewares;
using Serilog;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper(typeof(AuxiliarContabil.Application.Mappings.MappingProfile));

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions
        {
            AdditionalColumns = new[]
            {
                new Serilog.Sinks.MSSqlServer.SqlColumn("ThreadId", SqlDbType.Int)
            }
        })
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer();
app.UseHttpMetrics();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();