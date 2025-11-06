using AppSpeakers.Api.Models;
using AppSpeakers.Domain;
using Azure.Monitor.OpenTelemetry.Exporter;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Speakers.Api.Models;
using Speakers.Api.Repositories;
using Speakers.Api.Services;

var builder = WebApplication.CreateBuilder(args);

IAppSettings appSettings = new AppSettings();
builder.Configuration.GetSection("AppSettings").Bind(appSettings);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IAppSettings>(appSettings);

if (builder.Environment.IsDevelopment())
{
    //Seed data only in Development environment
    builder.Services.AddDbContext<SpeakerDbContext>(options =>
    {
        options.UseSqlServer(appSettings.ConnectionString)

        .UseAsyncSeeding(async (context, _, ct) =>
        {
            var faker = new Faker<Speaker>()
                        .UseSeed(300)
                        .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
                        .RuleFor(x => x.Name, f => f.Person.FullName)
                        .RuleFor(x => x.Bio, f => f.Company.Random.Words(20))
                        .RuleFor(x => x.WebSite, f => f.Person.Website);

            var speakers = faker.Generate(100);

            if (!await context.Set<Speaker>().ContainsAsync(speakers[0], cancellationToken: default))
            {
                await context.Set<Speaker>().AddRangeAsync(speakers);
                await context.SaveChangesAsync();
            }
        });
    });
}
else
{
    builder.Services.AddDbContext<SpeakerDbContext>(options => options.UseSqlServer(appSettings.ConnectionString));
}
builder.Services.AddScoped<ISpeakerRepository, SpeakerRepository>();
builder.Services.AddScoped<ISpeakerService, SpeakerService>();


/* Setup OpenTelemetry */

//Logging
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.ParseStateValues = true;
    options.IncludeFormattedMessage = true;
    options.AddAzureMonitorLogExporter(options =>
    {
        options.ConnectionString = appSettings.ApplicationInsightConnectionString;
    });
});

//Tracing
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddAzureMonitorTraceExporter(options =>
            {
                options.ConnectionString = appSettings.ApplicationInsightConnectionString;
            });
    });


var app = builder.Build();


//For React Client
app.UseCors(options =>
{
    options.WithOrigins("https://localhost:7070", "http://localhost:5173", "http://localhost:5106").AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await using (var serviceScope = app.Services.CreateAsyncScope())
    await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<SpeakerDbContext>())
    {
        await dbContext.Database.EnsureCreatedAsync();
    }

    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "AppSpeakers API";
        options.EnableDarkMode();
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
