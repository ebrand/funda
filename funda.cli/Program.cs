using PowerArgs;
using System;

namespace funda.cli
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(155, 45);
			Console.WriteLine();
			Console.WriteLine("Funda CLI");
			Console.WriteLine();
			Console.WriteLine("Version: {0}", Environment.GetEnvironmentVariable("CLI_VERSION"));
			Console.WriteLine();

			while (true)
			{
				try
				{
					Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] >");
					Args.InvokeAction<ArgActionMethods>(args);
					Console.WriteLine();
				}
				catch (Exception exc)
				{
					Console.WriteLine(exc.Message);
				}
			}
		}
	}
}