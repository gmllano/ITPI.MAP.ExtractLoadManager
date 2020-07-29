using log4net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Interface responsible to retrieve, update, and insert data to database server.
	/// </summary>
	public interface IDataManager
	{
		/// <summary>
		/// Insert program.
		/// </summary>
		/// <param name="program">A program record.</param>
		/// <param name="courseCnt">Number of meetings inserted per file.</param>
		/// <returns>A value indicating if the operation succeeded.</returns>
		bool InsertProgram(Programs program, ref int programCnt);

		/// <summary>
		/// Insert program requirement.
		/// </summary>
		/// <param name="programRequirement">A program requirement.</param>
		/// <param name="programCnt">Number of program requirements inserted.</param>
		/// <returns>A value indicating if the operation succeeded.</returns>
		bool InsertProgramRequirements(ProgramRequirements programRequirement, ref int programCnt);

		/// <summary>
		/// Insert program catalog.
		/// </summary>
		/// <param name="programCatalogs">A program catalog.</param>
		/// <param name="programCatalogCnt">Number of program catalogs inserted.</param>
		/// <returns>A value indicating if the operation succeeded.</returns>
		bool InsertProgramCatalogCreditReq(ProgramsCatalogsCreditReq programCatalogs, ref int programCatalogCnt);

		/// <summary>
		/// Clear out Program tables.
		/// </summary>
		/// <returns>A value indicating the success of clearing out the program tables.</returns>
		bool ClearOutProgramTables();

		/// <summary>
		/// Clear out stage tables.
		/// </summary>
		/// <returns>A value indicating the success of clearing out the staging tables.</returns>
		bool ClearOutStageTables();
	}
}
