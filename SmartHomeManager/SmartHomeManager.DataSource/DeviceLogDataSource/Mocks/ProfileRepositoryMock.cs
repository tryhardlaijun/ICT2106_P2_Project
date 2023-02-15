using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceLogDataSource.Mocks
{
    public class ProfileRepositoryMock : IProfileService
    {
        protected readonly ApplicationDbContext _db;

        public IEnumerable<Device> GetDevicesByProfile(Guid profileId)
        {
            var allDevices = _db.Devices.ToList();
            var result = allDevices.Where(device => device.ProfileId == profileId);
            return result;
        }
    }
}
