using System.Diagnostics;

namespace SmartHomeManager.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Device(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void DoSomething()
        {
            Debug.WriteLine("HELLO!");
        }
    }
}
