namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class CreateResponse<T>
    {
        public Error? Error { get; set; }

        public T? Model { get; set; }
    }
}
