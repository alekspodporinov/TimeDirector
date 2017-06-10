using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDirector.Client.Model
{
    public class ClientProcess
    {
		public int ProcessId { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public TimeSpan Time { get; set; }
		//public QualityTime QualityTimeProcess { get; set; }
	}
}
