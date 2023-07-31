using Bravi.Application.Person;
using Bravi.Application.Person.Interfaces;
using Bravi.Domain.Core.Repository;
using Bravi.Domain.Person.Command;
using Bravi.Domain.Person.Repository;
using Bravi.Infrastructure.Repository;
using Bravi.Infrastructure.Session;
using Bravi.Infrastructure.Uow;
using MediatR;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<DbSession>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://localhost:4200",
            "http://localhost:80",
            "http://localhost:5000",
            "http://localhost:443")
             .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((x) => true)
               .AllowCredentials();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CreatePersonCommand));

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

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
