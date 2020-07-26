using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    internal sealed class TimerWorkerService : IHostedService, IDisposable
    {
        private readonly ILogger<TimerWorkerService> _logger;
        private Timer _timer;

        public TimerWorkerService(ILogger<TimerWorkerService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(OnTimer, cancellationToken, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void OnTimer(object state)
        {
            _logger.LogInformation("OnTimer event called");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync Called");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Dispose Called");
            _timer?.Dispose();
        }
    }
}
