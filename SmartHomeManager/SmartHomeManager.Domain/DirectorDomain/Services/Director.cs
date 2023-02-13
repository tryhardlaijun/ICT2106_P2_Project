using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class Director : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IEnumerable<Rule>? rules;
        private IEnumerable<Scenario>? scenarios;

        private IGenericRepository<Rule> _ruleRepository;
        private IGenericRepository<RuleHistory> _ruleHistoryRepository;
        private IGenericRepository<History> _historyRepository;

        public Director(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _ruleHistoryRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<RuleHistory>>();
            _historyRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<History>>();
            _ruleRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Rule>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            rules = await _ruleRepository.GetAllAsync();            

            while (!stoppingToken.IsCancellationRequested)
            {
                DoWork();
                await Task.Delay(30000);
            }
        }

        private async void DoWork()
        {
            Console.WriteLine(string.Format("{0} - {1}", "Director", DateTime.Now.ToString("HH:mm:ss.fff")));           

            //var ruleHistories = await _ruleHistoryRepository.GetAllAsync();
            
            if (rules != null)
            {
                var rLength = rules.Count();
                foreach (var rule in rules)
                {
                    Console.WriteLine(string.Format("Rule {0}: {1} {2}", rule.ScheduleName, rule.StartTime, Math.Floor((DateTime.Now - Convert.ToDateTime(rule.StartTime)).TotalMinutes)));
                    if (Math.Floor((DateTime.Now - Convert.ToDateTime(rule.StartTime)).TotalMinutes) == 0)
                    {
                        Console.WriteLine("Ding! " + rule.ScheduleName);
                        Guid guid = Guid.NewGuid();
                        Console.WriteLine(guid.ToString());

                        RuleHistory rh = new RuleHistory();
                        rh.RuleHistoryId = guid;
                        rh.StartTime = rule.StartTime;
                        rh.EndTime = rule.EndTime;
                        rh.ActionTrigger = rule.ActionTrigger;
                        rh.RuleNum = rLength;

                        rh.ScenarioName = rule.Scenario.ScenarioName;
                        rh.ConfigurationValue = rule.ConfigurationValue;
                        rh.DeviceName = rule.Device.DeviceName;

                        await _ruleHistoryRepository.AddAsync(rh);

                        History h = new History();
                        h.Message = "Light got triggered";
                        h.Timestamp = DateTime.Now.AddHours(-8);
                        h.ProfileId = rule.Device.ProfileId;
                        h.RuleHistoryId = guid;

                        await _historyRepository.AddAsync(h);
                    }
                }

            }
        }

    }
}
