using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Theatrical Players API",
            Description = "API para calcular valores e créditos para performances teatrais.",
            Contact = new OpenApiContact() { Name = "Lucas Vilar", Email = "lucasvilar-celestino@hotmail.com" }
        });
        c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "TheatricalSwaggerAnnotation.xml"));
    }
);

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

app.Run();
