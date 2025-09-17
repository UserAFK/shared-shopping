var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker.PriceFetcher.Worker>();

var host = builder.Build();
host.Run();
