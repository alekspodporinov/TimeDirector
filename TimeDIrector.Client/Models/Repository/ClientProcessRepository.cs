using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Repository
{
	interface IClientProcessRepository : IRepository<ClientProcess> { }

	public sealed class ClientProcessRepository : BaseRepository<ClientProcess>, IClientProcessRepository
	{
		public override IEnumerable<ClientProcess> GetList()
		{
			return _db.Set<ClientProcess>().Include("QualityTime");
		}

		public override ClientProcess Get(ClientProcess source)
		{
			return _db.Set<ClientProcess>().Include("QualityTime").ToList().FirstOrDefault(f => f.Name == source.Name && f.Title == source.Title);
		}
	}
}
