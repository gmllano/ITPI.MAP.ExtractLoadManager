using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	public class ChaffeyExtractLoad
	{

		private readonly IOrchestrationBase orchestration;
		private int programCnt;
		private int programReqCnt;
		private int programCatalogCnt;

		public ChaffeyExtractLoad(IOrchestrationBase orchestration)
		{
			this.orchestration = orchestration;
		}

		public bool RunOrchestration()
		{
			try
			{
				var files = orchestration.GetFiles();
				List<Programs> programs = new List<Programs>();
				List<ProgramRequirements> programRequirements = new List<ProgramRequirements>();
				List<ProgramsCatalogsCreditReq> programCatalogCredits = new List<ProgramsCatalogsCreditReq>();

				if (files != null)
				{
					if (this.orchestration.LoadTemps)
					{
						// Clear out program tables.
						var result = orchestration.ClearOutProgramTables();

						if (!result)
						{
							orchestration.Log.Error("Unable to clear out program tables.");
							throw new ApplicationException("Unable to clear out program tables.");
						}

						// Map data.
						foreach (var file in files)
						{
							switch ((EnumManager.programType)file.Key)
							{
								case EnumManager.programType.Programs:
									programs = orchestration.MapPrograms(file.Value);
									break;
								case EnumManager.programType.ProgramRequirements:
									programRequirements = orchestration.MapProgramRequirements(file.Value);
									break;
								case EnumManager.programType.ProgramsCatalogsCreditReq:
									programCatalogCredits = orchestration.MapProgramsCatalog(file.Value);
									break;
								default:
									break;
							}
						}

						// I'm assuming that all three files will always be present. Not sure how you would
						// load just one file into MAP without the others.
						if (programs?.Count <= 0 || programRequirements?.Count <= 0 || programCatalogCredits?.Count <= 0)
						{
							orchestration.Log.Error("One or more files were not mapped correctly. Load can't be completed.");
							return false;
						}

						// Insert data.
						this.orchestration.Log.Info("Inserting into TEMPORARY PROGRAM tables...Please wait.");
						var programResult = orchestration.InsertPrograms(programs, ref programCnt);
						var programReqResult = orchestration.InsertProgramRequirements(programRequirements, ref programReqCnt);
						var programCatalogResult = orchestration.InsertProgramCatalogCreditReq(programCatalogCredits, ref programCatalogCnt);

						if (programResult && programReqResult && programCatalogResult)
						{
							this.orchestration.Log.Info($"{programCnt} program records inserted.");
							this.orchestration.Log.Info($"{programReqCnt} program requirements records inserted.");
							this.orchestration.Log.Info($"{programCatalogCnt} program catalog records inserted.");
						}
					}

					if (orchestration.LoadStaging)  // Load staging tables.
					{
						var stageCleared = this.orchestration.ClearOutStagingTables();
						if (!stageCleared) 
						{
							orchestration.Log.Error("Unable to clear out staging tables. Stage update has stopped.");
							return false;
						}

						this.orchestration.Log.Info("Populating STAGING ISSUED FORM tables.");
						var insertResult = orchestration.InsertStagingIssuedFormData();
						if (!insertResult)
						{
							orchestration.Log.Error("Unable to populate staging issued form tables. Stage update has stopped.");
							return false;
						}
						else
						{
							this.orchestration.Log.Info("Populating STAGING PROGRAM COURSES table.  Please wait, this may take several minutes.");
							orchestration.InsertStagingProgramCourses();
						}
					}

					if (orchestration.LoadTarget) // Load MAP target tables.
					{
						this.orchestration.Log.Info("Populating Issued Form Production tables...");
						var result = orchestration.InsertIssuedFormData();

						if (!result)
						{
							orchestration.Log.Error("Unable to populate the production issued form tables for both program and courses.");
							return false;
						}
						else
						{
							this.orchestration.Log.Info("Populating Production Program Courses table.  Please wait, this may take several minutes.");
							orchestration.InsertProgramCoursesData();
						}
					}
					
					return true;
				}
				else
				{
					this.orchestration.Log.Warn("No files found to load.");
					return false;
				}
			}
			catch (Exception exp)
			{
				orchestration.Log.Error($"Exception {exp.Message}");
				return false;
			}
		}
	}
}
