using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using System.Windows.Input;
using TimeDIrector.Client.Models.Entity;
using TimeDIrector.Client.Services;
using TimeDIrector.Client.Services.Interfaces;

namespace TimeDIrector.Client.ViewModels
{
	public class MainWindowViewModel : NotifyViewModel
	{
		IProcessMonitorService _processMonitorService;

		public MainWindowViewModel(IProcessMonitorService processMonitorService)
		{
			_processMonitorService = processMonitorService;
			_processMonitorService.Initialize(new Dictionary<string,QualityTime> { {"devenv", new QualityTime{ProcessTime = TypeOfQualityTime.UsefulTime} }});
		}






		private DateTime _usefulTime;
		public DateTime UsefulTime
		{
			get { return _usefulTime; }
			private set { SetField(ref _usefulTime, value, nameof(UsefulTime)); }
		}
		private DateTime _uselessTime;
		public DateTime UselessTime
		{
			get { return _uselessTime; }
			private set { SetField(ref _uselessTime, value, nameof(UselessTime)); }
		}
		private DateTime _neutralTime;
		public DateTime NeutralTime
		{
			get { return _neutralTime; }
			private set { SetField(ref _neutralTime, value, nameof(NeutralTime)); }
		}
		private DateTime _lastSyncTime;
		public DateTime LastSyncTime
		{
			get { return _lastSyncTime; }
			private set { SetField(ref _lastSyncTime, value, nameof(LastSyncTime)); }
		}

	}
}
