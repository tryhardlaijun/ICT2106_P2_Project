using Microsoft.Extensions.Hosting;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class Director : IHostedService, IDisposable
    {
        //private readonly IGenericRepository<RuleHistory> _ruleHistoryRepository;
        private Timer? _timer = null;

        /*public Director(IGenericRepository<RuleHistory> ruleHistoryRepository)
        {
            _ruleHistoryRepository = ruleHistoryRepository;
        }*/

        private void DoWork(object? state)
        {
            Console.WriteLine("Ping!");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("On!");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Off!");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
