using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDirector.Client.Model
{
	public class ResponseData
	{
		Dictionary<string, QualityTime> QualityProcess { get; set; }
		Dictionary<QualityTime, TimeSpan> UserQualityTime { get; set; }
	}
}
