using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IRuleTriggerSubject
    {
        void Attach(IRuleTriggerObserver observer);

        // Detach an observer from the subject.
        void Detach(IRuleTriggerObserver observer);

        // Notify all observers of a trigger.
        void Notify(Guid deviceId, string configKey, int configVal);
    }
}
