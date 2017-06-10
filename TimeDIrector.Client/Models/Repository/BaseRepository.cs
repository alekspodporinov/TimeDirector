using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Autofac;
using TimeDIrector.Client.Models.Context;

namespace TimeDIrector.Client.Models.Repository
{
	public abstract class BaseRepository<T> : IDisposable where T : class
	{
		protected readonly TimeDirectorDataContext _db;

		private bool _disposed;

		public BaseRepository()
		{
			_db = Injection.Resolve<TimeDirectorDataContext>();
		}

		public virtual IEnumerable<T> GetList()
		{
			return _db.Set<T>();
		}

		public virtual T Get(T source)
		{
			return _db.Set<T>().Find(source);
		}

		public void Create(T source)
		{
			_db.Set<T>().Add(source);
		}

		public void CreateRange(IEnumerable<T> items)
		{
			_db.Set<T>().AddRange(items);
		}

		public void Update(T source)
		{
			_db.Entry(source).State = EntityState.Modified;
		}

		public void Delete(T deleteSource)
		{
			var source = _db.Set<T>().Find(deleteSource);
			if (source != null)
				_db.Set<T>().Remove(source);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_db.Dispose();
				}
			}
			_disposed = true;
		}
	}
}
