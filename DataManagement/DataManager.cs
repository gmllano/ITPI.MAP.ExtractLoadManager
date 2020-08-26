using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Class responsible to communicate with the database server.
	/// </summary>
	public class DataManager : IDataManager
	{
		#region variables

		/// <summary>
		/// The database source connection string.
		/// </summary>
		private readonly string sourceConnection = string.Empty;

		/// <summary>
		/// The database target connection string.
		/// </summary>
		private readonly string targetConnection = string.Empty;

		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger log = null;

		#endregion

		#region constructors

		/// <summary>
		/// The constructor for the data extract manager.
		/// </summary>
		/// <param name="connectionStr">The connection string.</param>
		/// <param name="log">The log file.</param>
		public DataManager(string sourceConnection, string targetConnection, ILogger log)
		{
			this.sourceConnection = sourceConnection;
			this.targetConnection = targetConnection;
			this.log = log;
		}

		#endregion

		#region public methods 
		
		/// <summary>
		/// Insert program data.
		/// </summary>
		/// <param name="program">The program information.</param>
		/// <param name="programCnt">The count of program records.</param>
		/// <returns>A value indicating success of inserting program data.</returns>
		public bool InsertProgram(Programs program, ref int programCnt)
		{
			bool result = false;

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			if (program != null)
			{
				try
				{
					programCnt += 1;
					var connection = new SqlConnection(this.sourceConnection);
					var parameters = new DynamicParameters(program);
					
					using (var conn = connection)
					{
						conn.Open();
						var sproc = "dbo.Programs_Ins";
						var value = connection.Execute(sproc,
							parameters,
							commandType: CommandType.StoredProcedure);

						result = true;
					}
				}
				catch (Exception exp)
				{
					log.Error($"Failed to insert Program. Program Title {program.ProgramTitle}, Program ID {program.ProgramID},  exception {exp.Message}");
				}
			}
			else
			{
				throw new ApplicationException("No program data to load.");
			}

			return result;
		}

		/// <summary>
		/// Insert program requirement data.
		/// </summary>
		/// <param name="programRequirement">The program requirement object.</param>
		/// <param name="programCnt">The program requirement count.</param>
		/// <returns>A value indicating the success of the insert.</returns>
		public bool InsertProgramRequirements(ProgramRequirements programRequirement, ref int programCnt)
		{
			bool result = false;

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			if (programRequirement != null)
			{
				try
				{
					programCnt += 1;
					var connection = new SqlConnection(this.sourceConnection);
					var parameters = new DynamicParameters(programRequirement);

					using (var conn = connection)
					{
						conn.Open();
						var sproc = "dbo.ProgramRequirements_Ins";
						var value = connection.Execute(sproc,
							parameters,
							commandType: CommandType.StoredProcedure);

						result = true;
					}
				}
				catch (Exception exp)
				{
					log.Error($"Failed to insert the program requirement. Program Title {programRequirement.ProgramTitle}, Program ID {programRequirement.ProgramID},  exception {exp.Message}");
				}
			}
			else
			{
				throw new ApplicationException("No program requirement data to load.");
			}

			return result;
		}

		/// <summary>
		/// Insert program catalog credit requirements.
		/// </summary>
		/// <param name="programCatalogs">The program catalog object.</param>
		/// <param name="programCatalogCnt">The program catalog count.</param>
		/// <returns>Returns a value indicating success of the insert.</returns>
		public bool InsertProgramCatalogCreditReq(ProgramsCatalogsCreditReq programCatalogs, ref int programCatalogCnt)
		{
			bool result = false;

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			if (programCatalogs != null)
			{
				try
				{
					programCatalogCnt += 1;
					var connection = new SqlConnection(this.sourceConnection);
					var parameters = new DynamicParameters(programCatalogs);

					using (var conn = connection)
					{
						conn.Open();
						var sproc = "dbo.ProgramsCatalogsCreditReq_Ins";
						var value = connection.Execute(sproc,
							parameters,
							commandType: CommandType.StoredProcedure);

						result = true;
					}
				}
				catch (Exception exp)
				{
					log.Error($"Failed to insert the program catalog. Program Title {programCatalogs.ProgramTitle}, Program ID {programCatalogs.ProgramID},  exception {exp.Message}");
				}
			}
			else
			{
				throw new ApplicationException("No program catalog data to load.");
			}

			return result;
		}

		/// <summary>
		/// Clear out the temporary Program tables.
		/// </summary>
		/// <returns>Returns a value indicating the success of clearing out the program tables.</returns>
		public bool ClearOutProgramTables()
		{
			try
			{
				if (string.IsNullOrEmpty(this.sourceConnection))
				{
					throw new ApplicationException("Connection string is empty or missing.");
				}

				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.ProgramData_Del";
					var value = connection.Execute(sproc, commandType: CommandType.StoredProcedure);

					return Convert.ToBoolean(value);
				}
			}
			catch (Exception exp)
			{
				log.Error($"Error occurred trying to clear out the temporary Program tables, Exception {exp.Message}");
				return false;
			}
		}

		/// <summary>
		/// Clear out the stage tables.
		/// </summary>
		/// <returns>Returns a value indicating the success of clearing out the stage tables.</returns>
		public bool ClearOutStageTables()
		{
			try
			{
				if (string.IsNullOrEmpty(this.sourceConnection))
				{
					throw new ApplicationException("Connection string is empty or missing.");
				}

				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.StageTables_Del";
					var value = connection.Execute(sproc, commandType: CommandType.StoredProcedure);

					return Convert.ToBoolean(value);
				}
			}
			catch (Exception exp)
			{
				log.Error($"Error occurred trying to clear out the staging tables, Exception {exp.Message}");
				return false;
			}
		}

		/// <summary>
		/// Insert staging data.
		/// </summary>
		/// <returns>Return a value indicating success.</returns>
		public bool InsertStagingData()
		{
			bool result = false;

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			try
			{
				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.StageTables_Ins";
					var value = connection.Execute(sproc,
						commandType: CommandType.StoredProcedure);

					result = true;
				}
			}
			catch (Exception exp)
			{
				log.Error($"Failed to populate staging data,  exception {exp.Message}");
			}

			return result;
		}

		/// <summary>
		/// Get all the staged programs.
		/// </summary>
		/// <returns>A list of staged programs.</returns>
		public List<Stage_Program_IssuedForm> GetStagedPrograms()
		{
			var stagedPrograms = new List<Stage_Program_IssuedForm>();

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			try
			{
				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.StageTables_ProgramIssuedForm_Get";
					stagedPrograms = connection.Query<Stage_Program_IssuedForm>(sproc,
						commandType: CommandType.StoredProcedure).ToList();

					return stagedPrograms;
				}
			}
			catch (Exception exp)
			{
				log.Error($"Failed to get staged programs from the Stage_Program_IssuedForm table,  exception {exp.Message}");
			}

			return stagedPrograms;
		}

		/// <summary>
		/// Get a list of program requirements.
		/// </summary>
		/// <param name="programDesc">The program description.</param>
		/// <returns>A list of program requirements.</returns>
		public List<ProgramRequirements> GetProgramRequirementsByProgram(string programDesc)
		{
			var programReqs = new List<ProgramRequirements>();

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			try
			{
				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.ProgramRequirementsByProgramID_Get";
					var param = new DynamicParameters();
					param.Add("ProgramID", programDesc);
					programReqs = connection.Query<ProgramRequirements>(sproc, param,
						commandType: CommandType.StoredProcedure).ToList();

					return programReqs;
				}
			}
			catch (Exception exp)
			{
				log.Error($"Failed to get program requirement records from the program_requirements table,  exception {exp.Message}");
			}

			return programReqs;
		}

		/// <summary>
		/// Insert program course data.
		/// </summary>
		/// <param name="programCourses">A list of program courses.</param>
		/// <returns>A value indicating success of inserting program course data.</returns>
		public Int32 InsertProgramCourse(List<Stage_ProgramCourses> programCourses)
		{
			Int32 value = 0;

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			if (programCourses?.Count > 0)
			{
				try
				{
					var dt = Helper.CreateDataTable<Stage_ProgramCourses>(programCourses);

					var connection = new SqlConnection(this.sourceConnection);
					// Insert data.
					using (var conn = connection)
					{
						conn.Open();
						var sproc = "dbo.StageProgramCourses_Ins";
						value = connection.QuerySingle<int>(sproc,
							new { tvpProgramCourse = dt.AsTableValuedParameter("dbo.TVPProgamCourses") },
							commandType: CommandType.StoredProcedure);

						return value;
					}
				}
				catch (Exception exp)
				{
					log.Error($"Failed to insert program courses into database. {exp.Message}");
				}
			}

			return value;
		}

		/// <summary>
		/// Get Course information by course name.
		/// </summary>
		/// <param name="courseName">The course name.</param>
		/// <returns>Return information on the course issued form.</returns>
		public Stage_Course_IssuedForm GetCourse(string courseName)
		{
			if (courseName == string.Empty)
				return null;

			Stage_Course_IssuedForm courseIssuedForm = new Stage_Course_IssuedForm();

			if (string.IsNullOrEmpty(this.sourceConnection))
			{
				throw new ApplicationException("Connection string is empty or missing.");
			}

			try
			{
				var connection = new SqlConnection(this.sourceConnection);

				using (var conn = connection)
				{
					conn.Open();
					var sproc = "dbo.Stage_Course_IssuedForm_Get";
					var param = new DynamicParameters();
					param.Add("CourseName", courseName);
					courseIssuedForm = connection.QueryFirstOrDefault<Stage_Course_IssuedForm>(sproc, param,
						commandType: CommandType.StoredProcedure);

					return courseIssuedForm;
				}
			}
			catch (Exception exp)
			{
				log.Error($"Failed to get course information,  exception {exp.Message}");
			}

			return courseIssuedForm;
		}

		#endregion

		#region private methods

		/// <summary>
		/// Prepare and Load program course data table.
		/// </summary>
		/// <param name="programCourses">An instance of the program course class.</param>
		/// <returns>Returns a data table.</returns>
		private DataTable LoadProgramCourse(List<Stage_ProgramCourses> programCourses)
		{
			try
			{
				var dt = Helper.CreateDataTable<Stage_ProgramCourses>(programCourses);

				return dt;
			}
			catch (Exception exp)
			{
				this.log.Error($"Unable to map program course data into a data table. Exception message {exp.Message}");
				return null;
			}
		}

		#endregion
	}
}
