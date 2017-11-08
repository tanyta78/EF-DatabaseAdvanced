namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
            
        }

        public HospitalContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Patient> Patients { get; set; }
        
        public DbSet<Visitation> Visitations { get; set; }
        
        public DbSet<Diagnose> Diagnoses { get; set; }
        
        public DbSet<Medicament> Medicaments { get; set; }
        
        public DbSet<Doctor> Doctors { get; set; }
        
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<Patient>().Property(p => p.FirstName).IsUnicode().HasMaxLength(50);

            builder.Entity<Patient>(
                ep =>
                {
                    ep.Property(p => p.FirstName).HasColumnType("nvarchar(50)");
                    ep.Property(p => p.LastName).HasColumnType("nvarchar(50)");
                    ep.Property(p => p.Address).HasColumnType("nvarchar(250)");
                    ep.Property(p => p.Email).HasColumnType("varchar(80)");
                });

            builder.Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId);

            builder.Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient)
                .HasForeignKey(d => d.PatientId);


            builder.Entity<Visitation>(ev =>
            {
                ev.Property(p => p.Comments).HasColumnType("nvarchar(250)");
            });
            
            builder.Entity<Diagnose>(ed =>
            {
                ed.Property(p => p.Name).HasColumnType("nvarchar(50)");
                ed.Property(p => p.Comments).HasColumnType("nvarchar(250)");
            });

            builder.Entity<Medicament>(em =>
            {
                em.Property(p => p.Name).HasColumnType("nvarchar(50)");
            });
            
            builder.Entity<PatientMedicament>()
                .ToTable("PatientsMedicaments")
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            builder.Entity<Doctor>(ed =>
            {
                ed.Property(p => p.Name).HasColumnType("nvarchar(100)");
                ed.Property(p => p.Specialty).HasColumnType("nvarchar(100)");
            });
            
            builder.Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor)
                .HasForeignKey(v => v.DoctorId);
        }
    }
}
