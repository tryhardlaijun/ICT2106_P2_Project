using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.RoomDomain;
public interface IRoomRepository
{
    Room? Get(int? id);
    IEnumerable<Room> GetAll();
    IEnumerable<Room> Find(Expression<Func<Room, bool>> predicate);
    void Add(Room entity);
    void AddRange(IEnumerable<Room> entities);
    void Remove(Room entity);
    void RemoveRange(IEnumerable<Room> entities);
}