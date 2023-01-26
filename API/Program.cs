using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Scopes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Also commonly known as Middleware
// A way to manipulate HTTP requests in their way IN or OUT
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// When we are done with the scope everything inside the scope will be disposed
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try {
    var context = services.GetRequiredService<DataContext>();
    // Migrate to the Database
    context.Database.Migrate();
    // Use our Seed data
    await Seed.SeedData(context);
}
catch (Exception exception)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(exception, "An error occurred during migration");
}

app.Run();
