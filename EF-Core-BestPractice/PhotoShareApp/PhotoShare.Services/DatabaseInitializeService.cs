namespace PhotoShare.Services
{
    using Contracts;
    using Data;

    public class DatabaseInitializeService:IDatabaseInitializeService
    {
        private readonly PhotoShareContext context;

        public DatabaseInitializeService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void DatabaseInitialize()
        {
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
        }
    }
}
