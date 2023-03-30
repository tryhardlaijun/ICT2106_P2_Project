using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IAdapter<C,R> where C:class where R:class
	{
        public R ToRequest(C concreate);
        public C ToEntity(R request);
        
    }
}
