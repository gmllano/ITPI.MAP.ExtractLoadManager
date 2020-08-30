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

		/// <summary>
		/// A value indicating whether to load staging tables.
		/// </summary>
		bool LoadStaging { get; set; }

		/// <summary>
		/// A value indicating whether to load target tables.
		/// </summary>
		bool LoadTarget { get; set; }

		bool LoadTemps { get; set; }

		/// <summary>
		/// Get the files from the source directory.
		/// </summary>
		/// <returns>Return a dictionary of file information objects.</returns>
		Dictionary<EnumManager.programType, FileInfo> GetFiles();

		/// <summary>
		/// Insert program requirements.
		/// </summary>
		/// <param name="programRequirements">A list of program requirements.</param>
		/// <param name="programCnt">The row count of program requirements in the file.</param>
		/// <returns>A value indicating success.</returns>
		bool InsertProgramRequirements(List<ProgramRequirements> programRequirements, ref int programCnt);

		/// <summary>
		/// Insert program catalog credit requirements.
		/// </summary>
		/// <param name="programCatalogs">The list of program catalogs.</param>
		/// <param name="programCnt">The count of program catalogs.</param>
		/// <returns>A value indicating success.</returns>
		bool InsertProgramCatalogCreditReq(List<ProgramsCatalogsCreditReq> programCatalogs, ref int programCnt);

		/// <summary>
		/// Insert programs.
		/// </summary>
		/// <param name="programs">A list of program items.</param>
		/// <param name="programCnt">The number of program records in the file.</param>
		/// <returns>A value indicating success.</returns>
		bool InsertPrograms(List<Programs> programs, ref int programCnt);

		/// <summary>
		/// Map the records to a collection of programs.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>Return a list of programs.</returns>
		List<Programs> MapPrograms(FileInfo file);

		/// <summary>
		/// Map the program requirements.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>A value indicating success.</returns>
		List<ProgramRequirements> MapProgramRequirements(FileInfo file);

		/// <summary>
		/// Map program catalog information to a collection of program catalog credit requirements.
		/// </summary>
		/// <param name="file">The file to map.</param>
		/// <returns>A value indicating success.</returns>
		List<ProgramsCatalogsCreditReq> MapProgramsCatalog(FileInfo file);

		/// <summary>
		/// Clear out the program tables (temporary tables).
		/// </summary>
		/// <returns>A value indicating success.</returns>
		bool ClearOutProgramTables();

		/// <summary>
		/// Clear out the staging tables.
		/// </summary>
		/// <returns>A value indicating success.</returns>
		bool ClearOutStagingTables();

		/// <summary>
		/// Load data into the staging program and course issued form tables only.
		/// </summary>
		/// <returns>A value indicating success.</returns>
		bool InsertStagingIssuedFormData();

		/// <summary>
		/// Load the required courses into staging table stage_tblProgramCourses.
		/// </summary>
		void InsertStagingProgramCourses();

		/// <summary>
		/// Insert issued form data into production tables. Both programs and courses.
		/// </summary>
		/// <returns>A value indicating success.</returns>
		bool InsertIssuedFormData();

		/// <summary>
		/// Load the required courses into production table tblProgramCourses.
		/// </summary>
		void InsertProgramCoursesData();
	}
}
