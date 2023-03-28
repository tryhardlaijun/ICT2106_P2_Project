using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    /// <summary>
    /// Custom Repository that only allows getting by device type for HomeSecurityDeviceDefinitions.
    /// </summary>
    /// <typeparam name="T">The entity that the repository will handle.</typeparam>
    public interface IHomeSecurityDeviceDefinitionRepository
    {
        public Task<IEnumerable<HomeSecurityDeviceDefinition>?> GetAllAsync();
    }
}