using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities.DTOs;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.Services;

namespace SmartHomeManager.API.Controllers.RoomAPI;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly RoomReadService _roomReadService;
    private readonly RoomWriteService _roomWriteService;

    public RoomsController(IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _roomReadService = new RoomReadService(roomRepository, deviceInformationService);
        _roomWriteService = new RoomWriteService(roomRepository);
    }

    // GET: api/Rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRoomWebRequest>>> GetRooms()
    {
        var result = await _roomReadService.GetAllRooms();
        // if (!result.Any()) return NotFound();

        var resp = result.Select(room => new GetRoomWebRequest
        {
            RoomId = room.RoomId,
            Name = room.Name,
            AccountId = room.AccountId
        }).ToList();

        return Ok(resp);
    }

    // GET: api/Rooms/5
    [HttpGet("{roomId}")]
    public async Task<ActionResult<GetRoomWebRequest>> GetRoom(Guid roomId)
    {
        var result = await _roomReadService.GetRoomById(roomId);
        if (result == null) return NotFound();

        var resp = new GetRoomWebRequest
        {
            RoomId = result.RoomId,
            Name = result.Name,
            AccountId = result.AccountId
        };

        return resp;
    }

    // PUT: api/Rooms/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{roomId}")]
    public async Task<IActionResult> PutRoom(Guid roomId, EditRoomWebRequest roomWebRequest)
    {
        try
        {
            var res = await _roomReadService.GetRoomById(roomId);

            if (res == null) return BadRequest();

            res.Name = roomWebRequest.Name ?? res.Name;

            _roomWriteService.UpdateRoom(res);
            await _roomWriteService.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            var isExist = RoomExists(roomId);
            if (!isExist.Result)
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Rooms
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetRoomWebRequest>> PostRoom(CreateRoomWebRequest roomWebRequest)
    {
        var newRoom = new Room
        {
            Name = roomWebRequest.Name,
            AccountId = roomWebRequest.AccountId
        };

        _roomWriteService.AddRoom(newRoom);
        await _roomWriteService.SaveChangesAsync();

        var resp = new GetRoomWebRequest
        {
            Name = newRoom.Name,
            AccountId = newRoom.AccountId
        };

        // the route values specifies the action to be called and the route values to be used for that action
        // for example new { roomId = xxx } must match [HttpGet("{roomId}")]
        // as in the parameter names must match, roomId with roomId
        return CreatedAtAction("GetRoom", new { roomId = newRoom.RoomId }, resp);
    }

    // DELETE: api/Rooms/5
    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom(Guid roomId)
    {
        var room = await _roomReadService.GetRoomById(roomId);
        if (room == null) return NotFound();

        _roomWriteService.RemoveRoom(room);
        await _roomWriteService.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Rooms/GetRoomsRelatedToAccount/accountId
    [HttpGet("GetRoomsRelatedToAccount/{accountId}")]
    public ActionResult<IEnumerable<GetRoomWebRequest>> GetRoomsRelatedToAccount(Guid accountId)
    {
        var result = _roomReadService.GetRoomsRelatedToAccount(accountId);

        var resp = result.Select(room => new GetRoomWebRequest
        {
            RoomId = room.RoomId,
            Name = room.Name,
            AccountId = room.AccountId
        }).ToList();

        return Ok(resp);
    }

    // GET: api/Rooms/GetRoomsRelatedToAccount/roomId
    [HttpGet("GetDevicesInRoom/{roomId}")]
    public ActionResult<IEnumerable<Device>> GetDevicesInRoom(Guid roomId)
    {
        var result = _roomReadService.GetDevicesInRoom(roomId);
        if (!result.Any()) return NotFound();

        return Ok(result);
    }

    private async Task<bool> RoomExists(Guid id)
    {
        var result = await _roomReadService.GetRoomById(id);
        return result == null;
    }
}