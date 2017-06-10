using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDirector.Client.Model
{
	public class StartRquest
	{
		public string LastUserToken { get; set; }
		public DateTime StartTime { get; set; }
		
	}
}
