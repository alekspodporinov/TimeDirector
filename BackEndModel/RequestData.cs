using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDirector.Client.Model
{
	public class RequestData
	{
		List<ClientProcess> Processes { get; set; }
		public TimeSpan PauseTime { get; set; }
		public string ClientKey { get; set; }
	}
}
