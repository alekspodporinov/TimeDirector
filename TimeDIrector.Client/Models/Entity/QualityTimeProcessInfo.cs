using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models.Entity
{
	public class QualityTimeProcessInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public QualityTime QualityTime { get; set; }
	}
}
