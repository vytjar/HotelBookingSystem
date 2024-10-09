using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class Error
    {
        public required string Message { get; set; }

        public required HttpStatusCode StatusCode { get; set; }
    }
}
