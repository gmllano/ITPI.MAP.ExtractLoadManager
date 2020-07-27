using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	public abstract class OrchestrationBase : IOrchestrationBase
	{
		public abstract Dictionary<EnumManager.programType, FileInfo> GetFiles();

		public abstract bool InsertProgramRequirements(List<ProgramRequirements> programRequirements, ref int programCnt);

		public abstract bool InsertProgramCatalogCreditReq(List<ProgramsCatalogsCreditReq> programCatalogs, ref int programCnt);

		public abstract bool InsertPrograms(List<Programs> programs, ref int programCnt);

		public abstract List<Programs> MapPrograms(FileInfo file);

		public abstract List<ProgramRequirements> MapProgramRequirements(FileInfo file);

		public abstract List<ProgramsCatalogsCreditReq> MapProgramsCatalog(FileInfo file);

		public abstract bool ClearOutProgramTables();

		public virtual  ILogger Log { get; set; }

		public virtual string SourcePath { get; set; }
	}
}
