namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class UpdateResponse<T>
    {
        public Error? Error { get; set; }

        public T? Model { get; set; }
    }
}
