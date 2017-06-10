using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models.Entity
{
	public class ClientInfo
	{
		public int Id { get; set; }
		public string LicenseKey { get; set; }
		public string Token { get; set; }
		public string StartTime { get; set; }
		public string PauseTime { get; set; }
		public string EndTime { get; set; }
		public string LastConnection { get; set; }
	}
}
