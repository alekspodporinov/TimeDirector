using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Repository
{
	public interface IQualityTimeProcessInfoRepository : IRepository<QualityTimeProcessInfo> { }

	public sealed class QualityTimeProcessInfoRepository : BaseRepository<QualityTimeProcessInfo>, IQualityTimeProcessInfoRepository
	{
		public override IEnumerable<QualityTimeProcessInfo> GetList()
		{
			return _db.Set<QualityTimeProcessInfo>().Include("QualityTime");
		}

		public override QualityTimeProcessInfo Get(QualityTimeProcessInfo source)
		{
			return _db.Set<QualityTimeProcessInfo>().Include("QualityTime").ToList().FirstOrDefault(f => f.Name == source.Name);
		}
	}
}
