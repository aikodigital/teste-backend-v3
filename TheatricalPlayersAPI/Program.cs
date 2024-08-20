var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddControllers();

// Register the StatementProcessor service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Setup the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Use the Swagger middleware
    app.UseSwaggerUI(c =>
    {
        // Configure the Swagger endpoint
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();