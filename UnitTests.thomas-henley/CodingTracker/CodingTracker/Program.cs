using CodingTracker;

using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .Build();

DapperHelper dapper = new(config);

SpectreValidation validation = new(config);
SpectreConsole console = new(config, validation);

CodingController controller = new(config, dapper, console);
controller.Run();