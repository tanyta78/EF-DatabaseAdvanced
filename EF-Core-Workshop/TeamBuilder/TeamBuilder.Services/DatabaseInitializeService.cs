namespace TeamBuilder.Services
{
    using Contracts;
    using Data;

    public class DatabaseInitializeService:IDatabaseInitializeService
    {
        private readonly TeamBuilderContext db;

        public DatabaseInitializeService(TeamBuilderContext db)
        {
            this.db = db;
        }

        public void DatabaseInitialize()
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //InitialSeed(db);
        }
    }
}
