using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeDirector.Client.Model;

namespace TimeDIrector.Client.Components.Helpers
{
	static public class ProcessHelper
	{
		public static Process GetForegroundProcess()
		{
			IntPtr hWnd;
			Process currentProcess = null;
			uint pid = 0;
			try
			{
				hWnd = Win32Helper.GetForegroundWindow();
				Win32Helper.GetWindowThreadProcessId(hWnd, out pid);

				currentProcess = Process.GetProcessById((int)pid);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			} finally
			{
				hWnd = IntPtr.Zero;
			}
			return currentProcess;
		}
	}
}

