using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDirector.Client.Model
{
	public class StartResponse
	{
		public string NewUserToken { get; set; }
		public ResponseData ResponseDataProcess { get; set; }
	}
}
