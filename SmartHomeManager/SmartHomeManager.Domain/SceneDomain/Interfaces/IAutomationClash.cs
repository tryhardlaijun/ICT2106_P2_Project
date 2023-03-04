using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface  IAutomationClash
	{
        // get all rules on startup
        Task<Boolean> CheckForScheduleClash(Rule rule);
        Task<IEnumerable<Rule>> GetAllRules();

    }
}

