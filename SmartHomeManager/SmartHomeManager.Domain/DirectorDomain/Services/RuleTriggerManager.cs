using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class RuleTriggerManager 
    {
        private ICollection<IRuleTriggerObserver> triggerObservers;
        public RuleTriggerManager() {
            triggerObservers = new List<IRuleTriggerObserver>();
        }
        public void Attach(IRuleTriggerObserver observer)
        {
            triggerObservers.Add(observer);
        }

        public void Detach(IRuleTriggerObserver observer)
        {
            triggerObservers.Remove(observer);
        }

        public void Notify(Guid deviceId, string configKey, int configVal)
        {
            foreach (IRuleTriggerObserver observer in triggerObservers)
            {
                observer.Update(deviceId, configKey, configVal);
            }
        }

    }
}
