#region

using System;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace SFY_OCR
{
	internal static class Program
	{
		/// <summary>
		///     应用程序的主入口点。
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//只允许运行一个实例

			#region

			Process pr = Process.GetCurrentProcess();
			Process[] prlist = Process.GetProcessesByName(pr.ProcessName);

			if (prlist.Length >= 2)
			{
				return;
			}

			#endregion

			Application.Run(new frmMain());
		}
	}
}