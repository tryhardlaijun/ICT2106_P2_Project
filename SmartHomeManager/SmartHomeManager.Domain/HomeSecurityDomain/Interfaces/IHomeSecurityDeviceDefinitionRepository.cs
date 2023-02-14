using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    /// <summary>
    /// Custom Repository that only allows getting by device type for HomeSecurityDeviceDefinitions.
    /// </summary>
    /// <typeparam name="T">The entity that the repository will handle.</typeparam>
    public interface IHomeSecurityDeviceDefinitionRepository<T> where T : class
    {
        public Task<T?> GetByDeviceGroup(string deviceGroup);
    }
}