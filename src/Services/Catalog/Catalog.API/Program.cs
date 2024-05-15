var builder = WebApplication.CreateBuilder(args);

// before building the application
// Add services to the container.

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
				.AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// after building the application
// configure the HTTP request pipeline


app.MapCarter();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(opts => {
		opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		opts.RoutePrefix = string.Empty;
	});
}
app.UseExceptionHandler(option => { });
app.UseHealthChecks("/health",new HealthCheckOptions
{ 
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
