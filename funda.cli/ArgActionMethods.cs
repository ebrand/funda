using funda.cli.viewmodels;
using funda.common;
using PowerArgs;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace funda.cli
{
	[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling), TabCompletion]
    public class ArgActionMethods
    {
		[ArgActionMethod, ArgDescription("Quit the CLI."), ArgShortcut("X")]
		public void Exit()
		{
			Console.WriteLine("bye");
			Environment.Exit(0);
		}

		[ArgActionMethod, ArgDescription("Retrieve a single post."), ArgShortcut("P")]
		public async Task Post(RetrievePostArg arg)
		{
			Console.WriteLine($"Retrieving post {arg.Identifier.ToString()}...");

			var url = Environment.GetEnvironmentVariable("API_URL");
			var client = new RestClient(url);
			var request = new RestRequest(arg.Identifier.ToString());
			var response = await client.ExecuteGetTaskAsync<Post>(request);

			Console.WriteLine(Utilities.SerializeToJson<Post>(response.Data));
		}
    }
}