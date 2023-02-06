namespace SmartHomeManager.API.Controllers.DeviceLogAPI.ViewModels
{
    public class DeviceLogViewModel
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int DeviceActivity { get; set; }
        public int DeviceEnergyUsage { get; set; }
        
    }
}