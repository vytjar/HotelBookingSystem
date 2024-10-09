namespace HotelManagementSystem.Interfaces.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
