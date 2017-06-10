using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models.Entity
{
	public class ClientProcess
	{
		public int Id { get; set; }
		public int ProcessId { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Time { get; set; }
		[NotMapped]
		public TimeSpan ProcessTime
		{
			get { return TimeSpan.Parse(Time); }
			set { Time = value.ToString("g"); }
		}

		//public int QualityTimeId { get; set; }
		public QualityTime QualityTime { get; set; }
	}
}
