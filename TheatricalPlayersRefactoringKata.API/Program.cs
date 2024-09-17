using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Context;
using TheatricalPlayersRefactoringKata.DTO;
using TheatricalPlayersRefactoringKata.Formatter;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando o canal e serviços necessários
builder.Services.AddSingleton<IStatementFormatter, XmlStatementFormatter>();
builder.Services.AddSingleton<StatementProcessingService>();
builder.Services.AddDbContext<TheaterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TheatricalPlayersRefactoringKata.API")));
WebApplication app = builder.Build();
DatabaseManagementService.MigrationInitialisation(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region  Invoice Endpoint
/// <summary>
/// Creates a new invoice.
/// </summary>
/// <param name="invoiceDto">Data transfer object representing the invoice and related performances and plays.</param>
/// <param name="context">The database context.</param>
/// <returns>A success message if the invoice is saved successfully.</returns>
app.MapPost("/api/invoice", async (InvoiceDto invoiceDto, TheaterDbContext context) =>
{
    var invoice = new Invoice
    {
        Customer = invoiceDto.Customer,
        Performances = invoiceDto.Performances.Select(perf => new Performance
        {
            PlayId = perf.PlayId,
            Audience = perf.Audience,
        }).ToList()
    };

    var plays = invoiceDto.Plays.Select(p => new Play
    {
        Name = p.Name,
        Type = p.Type,
        Lines = p.Lines
    }).ToList();

    foreach (Play? play in plays)
    {
        if (!context.Plays.Any(dbPlay => dbPlay.Name == play.Name))
        {
            context.Plays.Add(play);
        }
    }
    context.Invoices.Add(invoice);
    await context.SaveChangesAsync();

    return Results.Ok("Invoice saved successfully!");
}).WithTags("Invoices");
/// <summary>
/// Retrieves all invoices.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <returns>A list of all invoices in the system.</returns>
app.MapGet("/api/invoices", async (TheaterDbContext dbContext) =>
{
    List<Invoice> invoice = await dbContext.Invoices.Include(d => d.Performances).ToListAsync();
    return Results.Ok(invoice);
}).WithTags("Invoices");
/// <summary>
/// Generates a legacy text statement based on invoice data.
/// </summary>
/// <param name="invoiceDto">Data transfer object representing the invoice and related performances and plays.</param>
/// <returns>A formatted statement.</returns>
app.MapPost("/api/invoice/statement/legacy", (InvoiceDto invoiceDto) =>
{
    var plays = invoiceDto.Plays.ToDictionary(
        play => play.Name.ToLower(),
        play => new Play(play.Name, play.Lines, play.Type)
    );

    var invoice = new Invoice
    {
        Customer = invoiceDto.Customer,
        Performances = invoiceDto.Performances.Select(perf => new Performance
        {
            PlayId = perf.PlayId,
            Audience = perf.Audience
        }).ToList()
    };

    var statementPrinter = new StatementPrinter();
    string result = statementPrinter.Print(invoice, plays);

    return Results.Ok(result);
}).WithTags("Invoices");

/// <summary>
/// Generates an XML formatted statement based on invoice data.
/// </summary>
/// <param name="invoiceDto">Data transfer object representing the invoice and related performances and plays.</param>
/// <returns>An XML formatted statement.</returns>
app.MapPost("/api/invoice/statement/xml", async (InvoiceDto invoiceDto) =>
{
    var plays = invoiceDto.Plays.ToDictionary(
        play => play.Name.ToLower(),
        play => new Play(play.Name, play.Lines, play.Type)
    );

    var invoice = new Invoice
    {
        Customer = invoiceDto.Customer,
        Performances = invoiceDto.Performances.Select(perf => new Performance
        {
            PlayId = perf.PlayId,
            Audience = perf.Audience
        }).ToList()
    };

    IStatementFormatter formatter = new XmlStatementFormatter();
    string result = await formatter.FormatAsync(invoice, plays);

    return Results.Ok(result);
}).WithTags("Invoices");

#endregion Invoice Endpoint
#region Plays Endpoint

/// <summary>
/// Retrieves all plays.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <returns>A list of all plays in the system.</returns>
app.MapGet("/api/plays", async (TheaterDbContext dbContext) =>
{
    List<Play> plays = await dbContext.Plays.ToListAsync();
    return Results.Ok(plays);
}).WithTags("Plays");

/// <summary>
/// Retrieves a specific play by its ID.
/// </summary>
/// <param name="id">The ID of the play to retrieve.</param>
/// <param name="dbContext">The database context.</param>
/// <returns>The play if found, otherwise NotFound.</returns>
app.MapGet("/api/plays/{id}", async (int id, TheaterDbContext dbContext) =>
{
    Play? play = await dbContext.Plays.FindAsync(id);
    if (play == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(play);
}).WithTags("Plays");

/// <summary>
/// Creates a new play.
/// </summary>
/// <param name="play">The play entity to create.</param>
/// <param name="dbContext">The database context.</param>
/// <returns>The created play with its generated ID.</returns>
app.MapPost("/api/plays", async (Play play, TheaterDbContext dbContext) =>
{
    dbContext.Plays.Add(play);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/plays/{play.PlayId}", play);
}).WithTags("Plays");

#endregion Plays Endpoint

#region Performance Endpoint

/// <summary>
/// Retrieves all performances.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <returns>A list of all performances.</returns>
app.MapGet("/api/performances", async (TheaterDbContext dbContext) =>
{
    List<Performance> performances = await dbContext.Performances.ToListAsync();
    return Results.Ok(performances);
}).WithTags("Performances");

/// <summary>
/// Retrieves a specific performance by its Play ID.
/// </summary>
/// <param name="playId">The Play ID associated with the performance.</param>
/// <param name="dbContext">The database context.</param>
/// <returns>The performance if found, otherwise NotFound.</returns>
app.MapGet("/api/performances/{playId}", async (int playId, TheaterDbContext dbContext) =>
{
    Performance? performance = await dbContext.Performances.FindAsync(playId);
    if (performance == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(performance);
}).WithTags("Performances");

/// <summary>
/// Creates a new performance.
/// </summary>
/// <param name="performance">The performance entity to create.</param>
/// <param name="dbContext">The database context.</param>
/// <returns>The created performance with its Play ID.</returns>
app.MapPost("/api/performances", async (Performance performance, TheaterDbContext dbContext) =>
{
    dbContext.Performances.Add(performance);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/api/performances/{performance.PlayId}", performance);
}).WithTags("Performances");

#endregion Performance Endpoint
await app.RunAsync();
