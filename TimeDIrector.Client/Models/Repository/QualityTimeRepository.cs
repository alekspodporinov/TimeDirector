using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Repository
{
	interface IQualityTimeRepository : IRepository<QualityTime> { }

	public sealed class QualityTimeRepository : BaseRepository<QualityTime>, IQualityTimeRepository
	{
		public override IEnumerable<QualityTime> GetList()
		{
			return _db.QualityTimes.Include("ClientProcess").ToList();
		}

		public override QualityTime Get(QualityTime source)
		{
			return _db.QualityTimes.FirstOrDefault(f => f.Name == source.Name);
		}
	}
}
