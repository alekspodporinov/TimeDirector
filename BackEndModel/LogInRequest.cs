using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDirector.Client.Model
{
	class LogInRequest
	{
		public UserHost Host { get; set; }
		public string LicenseKey { get; set; }
	}
}
