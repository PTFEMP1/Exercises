using Microsoft.Extensions.Configuration;
using Serilog;

namespace MultiploDeOnze
{
    class Program
    {
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder();
			BuildConfig(builder);
			//Configurar o Serilog
			Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(builder.Build())
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateLogger();

			Log.Logger.Information("App MultiploOnze Starting");
		}
		//Configurar o Builder
		static void BuildConfig(IConfigurationBuilder builder)
		{
			builder.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile("appsettings.{Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Production\"}.json", optional: false, reloadOnChange: true)
			.AddEnvironmentVariables();
		}
	}
}