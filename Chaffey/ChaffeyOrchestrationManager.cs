using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ITPI.MAP.ExtractLoadManager
{
	public class ChaffeyOrchestrationManager : OrchestrationBase
	{
		#region constants
		
		private const string CSV = "*Program*.csv";
		private const string XML = "*Program*.xml";

		#endregion

		#region variables

		private readonly IDataManager dataManager;
		private readonly EnumManager.FileTypes fileType;

		#endregion

		#region constructors

		/// <summary>
		/// Constructor for Chaffey Orchestration Manager.
		/// </summary>
		/// <param name="sourcePath">The source path of the files.</param>
		/// <param name="dataManager">The data manager.</param>
		/// <param name="logger">The log.</param>
		public ChaffeyOrchestrationManager(string sourcePath, EnumManager.FileTypes fileType, IDataManager dataManager, ILogger logger)
		{
			this.SourcePath = sourcePath;
			this.Log = logger;
			this.fileType = fileType;
			this.dataManager = dataManager;
		}

		#endregion

		#region public methods

		/// <summary>
		/// Get the files from the source location.
		/// </summary>
		/// <returns>Return a dictionary of files.</returns>
		public override Dictionary<EnumManager.programType, FileInfo> GetFiles()
		{
			Dictionary<EnumManager.programType, FileInfo > files = new Dictionary<EnumManager.programType, FileInfo>();
			
			string searchPattern = string.Empty;

			try
			{
				if (string.IsNullOrEmpty(this.SourcePath))
				{
					Log.Error("Source directory path is missing.");
					return null;
				}

				if (!Directory.Exists(this.SourcePath))
				{
					Log.Error($"Directory {this.SourcePath} does not exists.");
					return null;
				}

				if (this.fileType == EnumManager.FileTypes.CSV)
				{
					searchPattern = CSV;
				}
				else
				{
					searchPattern = XML;
				}

				// Get directory files.
				Log.Info($"Source {this.SourcePath} with pattern match {searchPattern}");
				var dirFiles = Directory.GetFiles(this.SourcePath, searchPattern);
				Log.Info($".csv file count {dirFiles?.Count()}");

				if (dirFiles == null)
				{
					Log.Info($"No files located in directory {this.SourcePath}");
					return null;
				}

				foreach (var file in dirFiles)
				{
					if (file != null)
					{
						var fileInfo = new FileInfo(file);

						var fileName = fileInfo?.Name.ToLower();

						if (fileName.Contains("catalog"))
						{
							files.Add(EnumManager.programType.ProgramsCatalogsCreditReq, fileInfo);
						}
						else if (fileName.Contains("requirements"))
						{
							files.Add(EnumManager.programType.ProgramRequirements, fileInfo);
						}
						else if (fileName.Contains("programs"))
						{
							files.Add(EnumManager.programType.Programs, fileInfo);
						}
						else
						{
							this.Log.Info($"file {fileName} found in the directory but will NOT be loaded.");
						}
					}
				}
			}
			catch (Exception exp)
			{
				Log.Error($"An error occurred trying to get the files. {exp.Message}");
			}

			return files;
		}

		/// <summary>
		/// Map file to a list of programs.
		/// </summary>
		/// <param name="file">The file information.</param>
		/// <returns>A list of programs.</returns>
		public override List<Programs> MapPrograms(FileInfo file)
		{
			try
			{
				List<Programs> programs = new List<Programs>();
				
				programs = this.ReadProgramFile(file.FullName);
				
				this.Log.Info($"{programs?.Count} Program type records mapped in file {file.Name}.");
				
				return programs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Unable to map the file {file.FullName}. Exception {exp.Message}");
				return null;
			}
		}

		/// <summary>
		/// Map file to a list of program requirements.
		/// </summary>
		/// <param name="file">The file information.</param>
		/// <returns>Return a list of program requirements.</returns>
		public override List<ProgramRequirements> MapProgramRequirements(FileInfo file)
		{
			try
			{
				List<ProgramRequirements> programReqs = new List<ProgramRequirements>();

				programReqs = this.ReadProgramRequirementFile(file.FullName);

				this.Log.Info($"{programReqs?.Count} program requirement type records mapped in file {file.Name}.");

				return programReqs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Unable to map the file {file.FullName}. Exception {exp.Message}");
				return null;
			}
			
		}

		/// <summary>
		/// Map file to a list of program catalog credit requirements.
		/// </summary>
		/// <param name="file">The file to parse.</param>
		/// <returns>Return a list of program catalog credit requirements.</returns>
		public override List<ProgramsCatalogsCreditReq> MapProgramsCatalog(FileInfo file)
		{
			try
			{
				List<ProgramsCatalogsCreditReq> programCatalogs = new List<ProgramsCatalogsCreditReq>();

				programCatalogs = this.ReadProgramCatalogFile(file.FullName);

				this.Log.Info($"{programCatalogs?.Count} program catalog credit requirement type records mapped in file {file.Name}.");
				
				return programCatalogs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Unable to map the file {file.FullName}. Exception {exp.Message}");
				return null;
			}
		}

		/// <summary>
		/// Clear out program tables.
		/// </summary>
		/// <returns>A value indicating the success of clearning out the tables.</returns>
		public override bool ClearOutProgramTables()
		{
			try
			{
				return this.dataManager.ClearOutProgramTables();
			}
			catch (Exception exp)
			{
				this.Log.Error($"Unable to clear out program tables. Exception {exp.Message}");
				return false;
			}
		}

		/// <summary>
		/// Insert a list of programs.
		/// </summary>
		/// <param name="programs">The programs to insert.</param>
		/// <param name="programCnt">The row count.</param>
		/// <returns>A value indicating the result.</returns>
		public override bool InsertPrograms(List<Programs> programs, ref int programCnt)
		{
			try
			{
				foreach (var program in programs)
				{
					var result = this.dataManager.InsertProgram(program, ref programCnt);

					if (result == false)
					{
						Log.Warn($"Failed to insert program. Program title {program.ProgramTitle}, ID {program.ProgramID}");
					}
				}


				return true;
			}
			catch (Exception exp)
			{
				Log.Error($"An error occurred trying to insert a program. exception {exp.Message}");
				return false;
			}
		}

		/// <summary>
		/// Insert program requirements information.
		/// </summary>
		/// <param name="programRequirements">The program prequirements.</param>
		/// <param name="programCnt">The row count.</param>
		/// <returns>A value indicating the result.</returns>
		public override bool InsertProgramRequirements(List<ProgramRequirements> programRequirements, ref int programCnt)
		{
			try
			{
				foreach (var programReq in programRequirements)
				{
					var result = this.dataManager.InsertProgramRequirements(programReq, ref programCnt);

					if (result == false)
					{
						Log.Warn($"Failed to insert program requirement. Program title {programReq.ProgramTitle}, ID {programReq.ProgramID}");
					}
				}

				return true;
			}
			catch (Exception exp)
			{
				Log.Error($"An error occurred trying to insert a program requirement. exception {exp.Message}");
				return false;
			}
		}

		/// <summary>
		/// Insert program information.
		/// </summary>
		/// <param name="program">The program.</param>
		/// <param name="programCnt">The row count.</param>
		/// <returns>A value indicating the result.</returns>
		public override bool InsertProgramCatalogCreditReq(List<ProgramsCatalogsCreditReq> programCatalogs, ref int programCnt)
		{
			try
			{
				foreach (var programCatalog in programCatalogs)
				{
					var result = this.dataManager.InsertProgramCatalogCreditReq(programCatalog, ref programCnt);

					if (result == false)
					{
						Log.Warn($"Failed to insert program catalog credit requirement. Program title {programCatalog.ProgramTitle}, ID {programCatalog.ProgramID}");
					}
				}

				return true;
			}
			catch (Exception exp)
			{
				Log.Error($"An error occurred trying to insert a program catalog credit requirement. exception {exp.Message}");
				return false;
			}
		}

		#endregion

		#region private methods

		/// <summary>
		/// Map Program data.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public List<Programs> ReadProgramFile(string fileName)
		{
			List<Programs> programs = new List<Programs>();

			try
			{
				using (TextFieldParser parser = new TextFieldParser(fileName))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");
					bool firstfield = true;

					while (!parser.EndOfData)
					{
						//Process field
						string[] fields = parser.ReadFields();

						if (firstfield)
						{
							firstfield = false;
							continue;
						}

						var program = new Programs();

						DateTime startDate;
						DateTime endDate;
						var startDateResult = DateTime.TryParse(fields[10]?.ToString(), out startDate);
						var endDateResult = DateTime.TryParse(fields[11]?.ToString(), out endDate);

						program.ProgramTitle = fields[0].ToString() ?? "";
						program.ProgramStatus = fields[1].ToString() ?? "";
						program.ProgramID = fields[2].ToString() ?? "";
						program.Division = fields[3].ToString() ?? "";
						program.DepartmentID = fields[4].ToString() ?? "";
						program.DepartmentName = fields[5].ToString() ?? "";
						program.Locataion = fields[6].ToString() ?? "";
						program.DegreeType = fields[7].ToString() ?? "";
						program.ProgramCatalogDescription = fields[8]?.ToString().Replace('�', ' ') ?? "";
						program.ACPG_LOCAL_GOVT_CODES = fields[9]?.ToString() ?? "";
						if (startDateResult) { program.ACPG_START_DATE = startDate; }
						if (endDateResult) { program.ACPG_END_DATE = endDate; }
						programs.Add(program);
					}
				}

				return programs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Error occurred reading the program csv file. file {fileName}, exception {exp.Message}");
				return null;
			}
		}

		/// <summary>
		/// Map Program Requirement data.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public List<ProgramRequirements> ReadProgramRequirementFile(string fileName)
		{
			List<ProgramRequirements> programsReqs = new List<ProgramRequirements>();

			try
			{
				using (TextFieldParser parser = new TextFieldParser(fileName))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");
					bool firstfield = true;

					while (!parser.EndOfData)
					{
						//Process field
						string[] fields = parser.ReadFields();

						if (firstfield)
						{
							firstfield = false;
							continue;
						}

						int topBlockId;
						int subBlockId;
						var programReq = new ProgramRequirements();
						var topBlockIdResult = Int32.TryParse(fields[5]?.ToString(), out topBlockId);
						var subBlockIdResult = Int32.TryParse(fields[6]?.ToString(), out subBlockId);

						programReq.ProgramID = fields[0]?.ToString() ?? "";
						programReq.CatalogYear = fields[1]?.ToString() ?? "";
						programReq.ProgramTitle = fields[2]?.ToString() ?? "";
						programReq.RequirementDescription = fields[3]?.ToString() ?? "";
						programReq.RequirementID = fields[4]?.ToString() ?? "";
						if (topBlockIdResult) { programReq.topBlockId = topBlockId; }
						if (subBlockIdResult) { programReq.subBlock = subBlockId; }
						programReq.ACRB_PRINTED_SPEC = fields[7]?.ToString().Replace('�', ' ') ?? "";
						programReq.ACRB_COURSES_CRS_NAME = fields[8]?.ToString() ?? "";
						programReq.ACRB_COURSES = fields[9]?.ToString() ?? "";
						programReq.ACRB_FROM_COURSES_CRS_NAME = fields[10]?.ToString() ?? "";
						programReq.ACRB_FROM_COURSES = fields[11]?.ToString() ?? "";
						programReq.ACRB_FROM_SUBJECTS = fields[12]?.ToString() ?? "";
						programReq.ACRB_ACAD_CRED_RULES = fields[13]?.ToString() ?? "";
						programReq.ACRB_MIN_CRED = fields[14]?.ToString() ?? "";
						programReq.ACRB_MIN_NO_COURSES = fields[15]?.ToString() ?? "";
						programsReqs.Add(programReq);
					}
				}
				return programsReqs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Error occurred reading the program requirement csv file. file {fileName}, exception {exp.Message}");
				return null;
			}
		}

		/// <summary>
		/// Map Program Catalog Credit Requirements data.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public List<ProgramsCatalogsCreditReq> ReadProgramCatalogFile(string fileName)
		{
			List<ProgramsCatalogsCreditReq> programCatalogs = new List<ProgramsCatalogsCreditReq>();

			try
			{
				using (TextFieldParser parser = new TextFieldParser(fileName))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");
					bool firstfield = true;

					while (!parser.EndOfData)
					{
						//Process field
						string[] fields = parser.ReadFields();

						if (firstfield)
						{
							firstfield = false;
							continue;
						}

						float minCredits;
						var programCatalog = new ProgramsCatalogsCreditReq();
						var minCreditsResult = float.TryParse(fields[4]?.ToString(), out minCredits);

						programCatalog.ProgramID = fields[0]?.ToString() ?? "";
						programCatalog.ProgramTitle = fields[1]?.ToString() ?? "";
						programCatalog.CatalogYear = fields[2]?.ToString() ?? "";
						programCatalog.Minimum_Institutional_credits = fields[3]?.ToString() ?? "";
						if (minCreditsResult) { programCatalog.Minimum_credits = minCredits; }
						programCatalog.Minimum_Institutional_GPA = fields[5]?.ToString() ?? "";
						programCatalog.ACPG_STATUS = fields[6]?.ToString() ?? "";
						programCatalogs.Add(programCatalog);
					}
				}
				return programCatalogs;
			}
			catch (Exception exp)
			{
				this.Log.Error($"Error occurred reading the program csv file. file {fileName}, exception {exp.Message}");
				return null;
			}
		}

		#endregion
	}
}
