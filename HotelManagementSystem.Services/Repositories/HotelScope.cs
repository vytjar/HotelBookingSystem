namespace HotelManagementSystem.Services.Repositories
{
    public class HotelScope(HotelDbContext dbContext) : IDisposable
    {
        public HotelDbContext DbContext { get; } = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
