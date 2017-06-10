using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models.Repository
{
	public interface IRepository<T> : IDisposable where T : class
	{
		IEnumerable<T> GetList();
		T Get(T id);
		void Create(T item);
		void CreateRange(IEnumerable<T> items);
		void Update(T item);
		void Delete(T id);
		void Save();
		Task SaveAsync();
	}
}