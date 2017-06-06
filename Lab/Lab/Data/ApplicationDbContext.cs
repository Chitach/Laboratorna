using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lab.Models;

namespace Lab.Data {
	public class ApplicationDbContext : DbContext {
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<Specialization> Specializations { get; set; }
		public DbSet<Conducting> Conductings { get; set; }
		public DbSet<Demand> Demands { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<User> Users { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) {
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			builder.Entity<DoctorsPatiens>()
				.HasKey(x => new { x.DoctorId, x.PatientId });

			builder.Entity<DoctorsPatiens>()
				.HasOne(x => x.Doctor)
				.WithMany(y => y.Patients)
				.HasForeignKey(y => y.PatientId);

			builder.Entity<DoctorsPatiens>()
				.HasOne(x => x.Patient)
				.WithMany(y => y.Doctors)
				.HasForeignKey(y => y.DoctorId);

			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
	}
}
