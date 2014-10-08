using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SFY_OCR
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
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
			
			Application.Run(new Main());
		}
	}
}
