using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDirector.Client.Model;

namespace TimeDIrector.Client.Services.Interfaces
{
	interface IBackEndService
	{
		bool SendData(RequestData request);
	
	}
}
