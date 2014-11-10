using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SFY_OCR.Properties;

namespace SFY_OCR.Untilities
{
	public class CommandLineProcess
	{
		protected string _commandPath;
		protected string _arguments;
		protected string _workingDirectory;

		public void Process()
		{
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.FileName = _commandPath;
					process.StartInfo.Arguments = _arguments;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.WorkingDirectory = _workingDirectory;
					process.Start();
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
