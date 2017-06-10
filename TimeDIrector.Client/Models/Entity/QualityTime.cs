using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TimeDIrector.Client.Models.Entity
{
	public class QualityTime
	{
		public QualityTime()
		{
			ClientProcesses = new HashSet<ClientProcess>();
			QualityTimeProcessInfoes = new HashSet<QualityTimeProcessInfo>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		[NotMapped]
		public TypeOfQualityTime ProcessTime
		{
			get { return (TypeOfQualityTime)Enum.Parse(typeof(TypeOfQualityTime), Name, true); ; }
			set { Name = value.ToString("G"); }
		}
		public ICollection<ClientProcess> ClientProcesses { get; set; }
		public ICollection<QualityTimeProcessInfo> QualityTimeProcessInfoes { get; set; }
	}
}
