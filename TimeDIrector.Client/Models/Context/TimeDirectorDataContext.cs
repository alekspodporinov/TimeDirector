using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Context
{
	public sealed class TimeDirectorDataContext : DbContext, IDisposable
	{
		public DbSet<ClientInfo> ClinetInfoes { get; set; }
		public DbSet<ClientProcess> ClientProcesses { get; set; }
		public DbSet<QualityTimeProcessInfo> QualityTimeProcessInfoes { get; set; }
		public DbSet<QualityTime> QualityTimes { get; set; }

		private readonly string _dbPath;

		public TimeDirectorDataContext(string path)
			: base(new SQLiteConnection
			{
				ConnectionString = new SQLiteConnectionStringBuilder
				{
					DataSource = path,
					ForeignKeys = true,
					BinaryGUID = false,
				}.ConnectionString
			}, true)
		{
			_dbPath = path;
			Database.Log = Console.Write;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			Database.SetInitializer(new TimeDirectorContextInitializer<TimeDirectorDataContext>(_dbPath, modelBuilder));

			//modelBuilder.Entity<ClientProcess>()
			//	.HasRequired(c => c.QualityTime)
			//	.WithMany(c => c.ClientProcesses);

			//modelBuilder.Entity<QualityTimeProcessInfo>()
			//	.HasRequired(c => c.QualityTime)
			//	.WithMany(c => c.QualityTimeProcessInfoes);
		}

		protected override void Dispose(bool disposing)
		{
			
		}
	}
}
