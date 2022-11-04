using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MarketScraper.API.Models;
using MarketScraper.API.Repository;
using MarketScraper.API.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHangfireServer();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddHangfire(x => x.UseMongoStorage(builder.Configuration["MongoDB:ConnectionString"], new MongoStorageOptions
{
    MigrationOptions = new MongoMigrationOptions
    {
        MigrationStrategy = new MigrateMongoMigrationStrategy(),
        BackupStrategy = new CollectionMongoBackupStrategy(),
    },
    CheckConnection = true,
    CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
}));



builder.Services.AddSingleton<IProductRepository, ProductRespository>();
builder.Services.AddSingleton<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IMarketService, OlxMarketService>();
builder.Services.AddScoped<IMarketService, VintedMarketService>();


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

app.UseHangfireDashboard();

var someService = app.Services.GetService<IScheduleService>();

someService?.RunAtTimeOf(DateTime.Now);


app.Run();

