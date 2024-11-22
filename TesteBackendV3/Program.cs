using Aplication.DTO;
using Aplication.Interfaces;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Aplication.Interfaces.IPlayService, Aplication.Services.PlayService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/plays", (IPlayService playService, IMapper mapper) =>
{
    var plays = playService.GetPlays();
    var playsDtos = mapper.Map<List<PlayDto>>(plays);
    return Results.Ok(playsDtos);
});


app.Run();
