using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ITPI.MAP.ExtractLoadManager
{
	public interface IOrchestrationBase
	{
		/// <summary>
		/// The logger.
		/// </summary>
		ILogger Log { get; set; }

		bool LoadStaging { get; set; }

		bool LoadTarget { get; set; }

		Dictionary<EnumManager.programType, FileInfo> GetFiles();

		bool InsertProgramRequirements(List<ProgramRequirements> programRequirements, ref int programCnt);

		bool InsertProgramCatalogCreditReq(List<ProgramsCatalogsCreditReq> programCatalogs, ref int programCnt);

		bool InsertPrograms(List<Programs> programs, ref int programCnt);

		List<Programs> MapPrograms(FileInfo file);

		List<ProgramRequirements> MapProgramRequirements(FileInfo file);

		List<ProgramsCatalogsCreditReq> MapProgramsCatalog(FileInfo file);

		bool ClearOutProgramTables();

		bool ClearOutStagingTables();
	}
}
