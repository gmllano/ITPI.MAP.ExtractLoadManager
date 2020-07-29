using Autofac;
using Autofac.Core;
using System;
using System.Configuration;

namespace ITPI.MAP.ExtractLoadManager
{
	public class Program
	{
		private static IContainer Container { get; set; }

		public static void Main(string[] args)
		{
			var builder = new ContainerBuilder();

			try
			{
				string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
				var sourceConnection = ConfigurationManager.AppSettings["SourceConnection"];
				var targetConnection = ConfigurationManager.AppSettings["TargetConnection"];
				var college = ConfigurationManager.AppSettings["College"];
				var loadStaging = Convert.ToBoolean(ConfigurationManager.AppSettings["LoadStaging"]);
				var loadTarget = Convert.ToBoolean(ConfigurationManager.AppSettings["LoadTarget"]);

				if (string.IsNullOrEmpty(sourcePath))
				{
					throw new ApplicationException("Source directory path is missing from configuration.");
				}

				if (string.IsNullOrEmpty(sourceConnection))
				{
					throw new ApplicationException("Connection string is missing from configuration.");
				}
				
				builder.RegisterType<Logger>().As<ILogger>();
				builder.RegisterType<DataManager>().As<IDataManager>()
				.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "sourceConnection",
							   (pi, ctx) => sourceConnection))
				.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "targetConnection",
							   (pi, ctx) => targetConnection));
				builder.RegisterType<ChaffeyOrchestrationManager>().As<IOrchestrationBase>()
					.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "sourcePath",
							   (pi, ctx) => sourcePath))
					.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(EnumManager.FileTypes) && pi.Name == "fileType",
							   (pi, ctx) => EnumManager.FileTypes.CSV))
					.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(bool) && pi.Name == "loadStaging",
							   (pi, ctx) => loadStaging))
					.WithParameter(new ResolvedParameter(
							   (pi, ctx) => pi.ParameterType == typeof(bool) && pi.Name == "loadTarget",
							   (pi, ctx) => loadTarget));

				Container = builder.Build();

				Run(college);
			}
			catch (Exception exp)
			{
				Console.WriteLine($"Program.Main() failed to complete. Exception {exp.Message}");
				throw exp;
			}
		}

		/// <summary>
		/// Gather the files from the source location to be deserialize and loaded into the database.
		/// </summary>
		private static void Run(string college)
		{
			using (var scope = Container)
			{				
				try
				{
					if (college.Equals("Chaffey", StringComparison.InvariantCultureIgnoreCase))
					{
						var orchestration = scope.Resolve<IOrchestrationBase>();
						orchestration.Log.Info("BEGIN - Extract and load all files from folder.");
						var extractLoad = new ChaffeyExtractLoad(orchestration);
						extractLoad.RunOrchestration();
						orchestration.Log.Info("DONE - Extract and load has completed.");
					}
				}
				catch (Exception exp)
				{
					var result = scope.Resolve<Logger>();
					result.Error($"Program() | Run() method failed., {exp.Message}");
				}
			}
		}
	}
}
