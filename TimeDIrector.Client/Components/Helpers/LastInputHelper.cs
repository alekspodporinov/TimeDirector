using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace TimeDIrector.Client.Components.Helpers
{
	public static class LastInputHelper
	{
		public static int GetLastInputTime()
		{
			int idleTime = 0;
			Win32Helper.LASTINPUTINFO lastInputInfo = new Win32Helper.LASTINPUTINFO();
			lastInputInfo.cbSize = (UInt32)Marshal.SizeOf(lastInputInfo);
			lastInputInfo.dwTime = 0;

			int envTicks = Environment.TickCount;

			if (Win32Helper.GetLastInputInfo(ref lastInputInfo))
			{
				int lastInputTick = (Int32)lastInputInfo.dwTime;

				idleTime = envTicks - lastInputTick;
			}

			return ((idleTime > 0) ? (idleTime / 1000) : 0);
		}
	}
}
