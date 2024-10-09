using HotelManagementSystem.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateAsync(Room room);

        Task<Room> GetAsync(int roomId);

        Task<IEnumerable<Registration>> GetRegistrations(int roomId);

        Task<bool> RemoveAsync(int roomId);

        Task<Room> UpdateAsync(Room room);
    }
}
