using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class Response<T>
    {
        public Error? Error { get; set; }

        public T? Model { get; set; }
    }
}
