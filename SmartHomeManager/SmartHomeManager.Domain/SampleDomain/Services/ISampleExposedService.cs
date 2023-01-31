namespace SmartHomeManager.Domain.SampleDomain.Services
{
    public interface ISampleExposedService
    {
        public Task<string> GetNameByIdAsync(Guid id);
    }
}
