using System;
using System.Diagnostics;
using System.IO;
using Leonardo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables().AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true).Build();

var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();

var services = new ServiceCollection();
services.AddTransient<Fibonacci>();
services.AddDbContext<FibonacciDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddLogging(configure => configure.AddConsole());

using (var serviceProvider = services.BuildServiceProvider())
{
    var logger =serviceProvider.GetService<ILogger<Program>>();
    logger.LogInformation($"Application Name : {applicationConfig.Name}");
    logger.LogInformation($"Application Message : {applicationConfig.Message}");

    var stopwatch = new Stopwatch();

    stopwatch.Start();
   // await using var dataContext = serviceProvider.GetService<FibonacciDataContext>();
    var listOfResults = await serviceProvider.GetService<Fibonacci>().RunAsync(args);

    foreach (var listOfResult in listOfResults) Console.WriteLine($"Result : {listOfResult}");
    stopwatch.Stop();

    Console.WriteLine("time elapsed in seconds : " + stopwatch.Elapsed.Seconds);
}


/*
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning)
        .AddFilter("Demo", LogLevel.Debug).AddConsole();
});
var logger = loggerFactory.CreateLogger("Demo.Program");*/


// int Fib(int i)
// {
//     if (i <= 2) return 1;
//     return Fib(i - 1) + Fib(i - 2);
// }  