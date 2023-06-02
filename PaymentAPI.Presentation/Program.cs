using Mapster;
using MediatR;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Infastructure.Data.Models;
using PaymentAPI.Infastructure.Data.Repositories;
using PaymentAPI.Infastructure.Factories;
using PaymentAPI.Presentation.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses 
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var applicationAssembly = AppDomain.CurrentDomain.Load("PaymentAPI.Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<ISqlConnectionFactory>(_ => new SqliteConnectionFactory(connectionString));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionMiddleware<,>));

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
TypeAdapterConfig<PaymentDTO, Payment>
    .NewConfig()
    .ConstructUsing(src => new Payment(
        src.Amount,
        src.Currency,
        src.CardholderNumber,
        src.HolderName,
        src.ExpirationMonth,
        src.ExpirationYear,
        src.CVV,
        src.OrderReference
    ));


var app = builder.Build();

// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var factory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
    await factory.Init();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(options => options.AllowAnyOrigin());

app.Run();
