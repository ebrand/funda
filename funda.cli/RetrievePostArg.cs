using PowerArgs;
using System;

namespace funda.cli
{
    public class RetrievePostArg
    {
		[ArgRequired, ArgDescription("The post ID to display."), ArgPosition(1)]
		public int Identifier { get; set; }
    }
}