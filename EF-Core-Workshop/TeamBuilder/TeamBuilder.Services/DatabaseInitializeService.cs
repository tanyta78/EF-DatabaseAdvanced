namespace TeamBuilder.Services
{
    using System;
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
            Console.WriteLine("Database successfully created!");
            //InitialSeed(db);
        }
    }
}
