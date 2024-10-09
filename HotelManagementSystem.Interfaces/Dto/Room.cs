﻿namespace HotelManagementSystem.Interfaces.Dto
{
    public class Room
    {
        public required int Capacity { get; set; }

        public Hotel? Hotel { get; set; }

        public required int HotelId { get; set; }

        public int Id { get; set; }

        public ICollection<Reservation> Registrations { get; set; } = [];

        public required string RoomNumber { get; set; }
    }
}
