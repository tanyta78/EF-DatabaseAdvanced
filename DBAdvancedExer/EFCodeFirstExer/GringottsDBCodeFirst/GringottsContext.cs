namespace GringottsDBCodeFirst
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GringottsContext : DbContext
    {
        
        public GringottsContext()
            : base("name=GringottsContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GringottsContext>());
        }

      public virtual DbSet<WizardDeposit> WizzardDeposits { get; set; }
    }

}