using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Models.Repository
{
	interface IClientInfoRepository : IRepository<ClientInfo> { }

	public sealed class ClientInfoRepository : BaseRepository<ClientInfo>, IClientInfoRepository { }
}
