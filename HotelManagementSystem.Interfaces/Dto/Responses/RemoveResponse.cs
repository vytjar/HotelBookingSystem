using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class RemoveResponse
    {
        public Error? Error { get; set; }

        public bool Success { get; set; }
    }
}
