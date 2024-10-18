
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Program));
var assemblies = new[] { typeof(Program).Assembly };
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblies);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")
    ?? throw new ArgumentException("Database connection string is required"));
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assemblies[0]);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });



await app.RunAsync();