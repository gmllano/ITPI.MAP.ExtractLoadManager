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
		/// <summary>
		/// Get the files from the source directory.
		/// </summary>
		/// <returns>Return a dictionary of file information objects.</returns>
		public abstract Dictionary<EnumManager.programType, FileInfo> GetFiles();

		/// <summary>
		/// Insert program requirements.
		/// </summary>
		/// <param name="programRequirements">A list of program requirements.</param>
		/// <param name="programCnt">The row count of program requirements in the file.</param>
		/// <returns>A value indicating success.</returns>
		public abstract bool InsertProgramRequirements(List<ProgramRequirements> programRequirements, ref int programCnt);

		/// <summary>
		/// Insert program catalog credit requirements.
		/// </summary>
		/// <param name="programCatalogs">The list of program catalogs.</param>
		/// <param name="programCnt">The count of program catalogs.</param>
		/// <returns>A value indicating success.</returns>
		public abstract bool InsertProgramCatalogCreditReq(List<ProgramsCatalogsCreditReq> programCatalogs, ref int programCnt);

		/// <summary>
		/// Insert programs.
		/// </summary>
		/// <param name="programs">A list of program items.</param>
		/// <param name="programCnt">The number of program records in the file.</param>
		/// <returns>A value indicating success.</returns>
		public abstract bool InsertPrograms(List<Programs> programs, ref int programCnt);

		/// <summary>
		/// Map the records to a collection of programs.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>Return a list of programs.</returns>
		public abstract List<Programs> MapPrograms(FileInfo file);

		/// <summary>
		/// Map the program requirements.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>A value indicating success.</returns>
		public abstract List<ProgramRequirements> MapProgramRequirements(FileInfo file);

		/// <summary>
		/// Map program catalog information to a collection of program catalog credit requirements.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>A value indicating success.</returns>
		public abstract List<ProgramsCatalogsCreditReq> MapProgramsCatalog(FileInfo file);

		/// <summary>
		/// Clear out the program tables (temp tables).
		/// </summary>
		/// <returns>A value indicating success.</returns>
		public abstract bool ClearOutProgramTables();

		/// <summary>
		/// Clear out the staging tables.
		/// </summary>
		/// <returns>A value indicating success.</returns>
		public abstract bool ClearOutStagingTables();
		
		/// <summary>
		/// Insert staging issued form data.
		/// </summary>
		/// <returns></returns>
		public abstract bool InsertStagingIssuedFormData();

		/// <summary>
		/// Insert required courses into the staging table stage_tblProgramCourses.
		/// </summary>
		public abstract void InsertStagingProgramCourses();

		/// <summary>
		/// The log.
		/// </summary>
		public virtual  ILogger Log { get; set; }

		/// <summary>
		/// The source path of records.
		/// </summary>
		public virtual string SourcePath { get; set; }

		/// <summary>
		/// A value indicating whether to load staging tables.
		/// </summary>
		public virtual bool LoadStaging { get; set; }

		/// <summary>
		/// A value indicating whether to load target tables.
		/// </summary>
		public virtual bool LoadTarget { get; set; }

		/// <summary>
		/// A value indicating whether to load temporary tables.
		/// </summary>
		public virtual bool LoadTemps { get; set; }
	}
}
