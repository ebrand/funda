using PowerArgs;
using System;

namespace funda.cli
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine();
			Console.WriteLine("Funda CLI");
			Console.WriteLine("Version: {0}", Environment.GetEnvironmentVariable("CLI_VERSION"));
			Console.WriteLine();

			while (true)
			{
				try
				{
					Console.Write($"[{DateTime.Now.ToShortTimeString()}] >");
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