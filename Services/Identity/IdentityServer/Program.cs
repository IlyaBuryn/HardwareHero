using HardwareHero.Services.Shared.Constants;
using IdentityServer;
using IdentityServer.Extensions;
using IdentityServer.Migrations.ConfigurationDb;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    var isSeed = Environment.GetEnvironmentVariable("SEED").Contains("/seed");
    if (!isSeed)
    {
        isSeed = args.Contains("/seed");
    }

    if (isSeed)
    {
        Log.Information("Seeding database...");
        await SeedData.EnsureSeedData(app);
        Log.Information("Done seeding database. Exiting.");
    }

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}