using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Components.Infrastructure {
	class SingleAppInstance : IDisposable {
		private readonly Mutex _mutex;

		public bool CanRunApp { get; private set; }

		public SingleAppInstance(string appName) {
			bool createdNew;
			_mutex = new Mutex(true, appName, out createdNew);
			CanRunApp = createdNew;

			if (!CanRunApp) {
				var mainProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)
					.FirstOrDefault(x => x.Id != Process.GetCurrentProcess().Id);

				if (mainProcess != null) {
					//SetForegroundWindow(mainProcess.MainWindowHandle);
				}
			}
		}

		public void Dispose() {
			_mutex.Dispose();
		}

		//[DllImport("user32.dll")]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
