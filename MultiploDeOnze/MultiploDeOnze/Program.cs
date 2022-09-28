using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
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

			var host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddTransient<ICalcularMultiploOnzeService, CalcularMultiploOnzeService>();
				})
				.UseSerilog()
				.Build();

			var svc = ActivatorUtilities.CreateInstance<CalcularMultiploOnzeService>(host.Services);
			svc.Run();
		}
		/// <summary>
		/// Configurar o Builder
		/// </summary>
		static void BuildConfig(IConfigurationBuilder builder)
		{
			builder.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			//Não é necessário para este Exercicio mas importante em qualquer API.
			//.AddJsonFile("appsettings.{Environment.GetEnvironmentVariable(\"ASPNETCORE_ENVIRONMENT\") ?? \"Production\"}.json", optional: false, reloadOnChange: true)
			.AddEnvironmentVariables();
		}
	}
}