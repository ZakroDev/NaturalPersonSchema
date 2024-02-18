using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NaturalPerson.Api.Middlewares;
using NaturalPerson.Core.Person;
using NaturalPerson.Infra.Db;
using NaturalPerson.Infra.Person;
using NaturalPersonl.Infra.Person;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IFileService, FileRepository>();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<PersonContext>(x => x.UseSqlServer(connectionString));

//builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>(); 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseMiddleware<MultiLanguageMiddleware>();

app.MapControllers();

app.Run();
