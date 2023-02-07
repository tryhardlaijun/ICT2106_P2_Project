using Microsoft.Extensions.Hosting;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public interface IScope
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    public class Scope : IScope
    {
        private int executionCount = 0;
        private readonly IGenericRepository<Rule> _ruleRepository;
        private readonly IGenericRepository<RuleHistory> _ruleHistoryRepository;
        private readonly IGenericRepository<History> _historyRepository;

        public Scope(IGenericRepository<Rule> ruleRepository, 
            IGenericRepository<RuleHistory> ruleHistoryRepository,
            IGenericRepository<History> historyRepository)
        {
            _ruleRepository = ruleRepository;
            _ruleHistoryRepository = ruleHistoryRepository;
            _historyRepository = historyRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Scoped Processing Service is working. Count: {++executionCount}");
                Console.WriteLine(DateTime.Now);                

                var rules = await _ruleRepository.GetAllAsync();
                var ruleHistories = await _ruleHistoryRepository.GetAllAsync();
                var rhLength = ruleHistories.Count();
                foreach (var rule in rules)
                {
                    Console.WriteLine((DateTime.Now - Convert.ToDateTime(rule.StartTime)).TotalMinutes);
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
                        rh.RuleNum = rhLength;

                        rh.ScenarioName = "Scenario A";
                        rh.ConfigurationValue = rule.ConfigurationValue;
                        rh.DeviceName = "Light";

                        await _ruleHistoryRepository.AddAsync(rh);

                        History h = new History();
                        h.Message = "Light got triggered";
                        h.Timestamp = DateTime.Now.AddHours(-8); 
                        h.ProfileId = Guid.Parse("6EFC056E-71DA-48F5-A46F-0CB2E0A46919");
                        h.RuleHistoryId = guid;

                        await _historyRepository.AddAsync(h);
                    }
                }
                

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}