using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OrderService;

public class Worker : BackgroundService
{
    readonly IBus _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(IBus bus, ILogger<Worker> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            var eventObj = new Contracts.OrderPlacedEvent() { Id= Guid.NewGuid(), Value = $"The time is {DateTimeOffset.Now}" };
            _logger.LogInformation("Worker publishing {event}, Id: {id} at: {time}", nameof(Contracts.OrderPlacedEvent), eventObj.Id,  DateTimeOffset.Now);
            await _bus.Publish(eventObj, stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}