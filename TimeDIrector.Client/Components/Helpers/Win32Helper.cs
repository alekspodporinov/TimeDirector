using System;
using System.Runtime.InteropServices;

namespace TimeDIrector.Client.Components.Helpers
{
	static class Win32Helper
	{
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
		[DllImport("user32.dll")]
		public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		[StructLayout(LayoutKind.Sequential)]
		public struct LASTINPUTINFO
		{
			public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

			[MarshalAs(UnmanagedType.U4)]
			public UInt32 cbSize;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 dwTime;
		}
	}
}
