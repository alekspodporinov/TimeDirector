using System;
using System.Data.Entity;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Context
{
	public interface ITimeDirectorDataContext
	{
		DbSet<ClientInfo> ClinetInfoes { get; set; }
		DbSet<ClientProcess> ClientProcesses { get; set; }
		DbSet<QualityTimeProcessInfo> QualityTimeProcessInfoes { get; set; }
		DbSet<QualityTime> QualityTimes { get; set; }
	}
}