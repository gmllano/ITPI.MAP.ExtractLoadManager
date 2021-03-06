﻿using log4net;
using System;
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
		/// Insert program course into its staging table.
		/// </summary>
		/// <param name="programCourses">A list of program courses.</param>
		/// <returns>The identity insert value.</returns>
		Int32 InsertProgramCourse(List<Stage_ProgramCourses> programCourses);

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

		/// <summary>
		/// Insert staging data.
		/// </summary>
		/// <returns>A value indicating success.</returns>
		bool InsertStagingData();

		/// <summary>
		/// Return the staged programs.
		/// </summary>
		/// <returns>A list of staged programs.</returns>
		List<Stage_Program_IssuedForm> GetStagedPrograms();

		/// <summary>
		/// Return a list of program requirements by program description.
		/// </summary>
		/// <param name="programDesc">The program description.</param>
		/// <returns>A list of program requirements.</returns>
		List<ProgramRequirements> GetProgramRequirementsByProgram(string programDesc);

		/// <summary>
		/// Get course information
		/// </summary>
		/// <param name="courseName">The course name.</param>
		/// <returns>Return the course issued form.</returns>
		Stage_Course_IssuedForm GetCourse(string courseName);

		/// <summary>
		/// Prepare issued form data in production tables.
		/// </summary>
		/// <returns>Return a value indicating success of the insert.</returns>
		bool InsertIssuedFormData();

		/// <summary>
		/// Insert program course object into its production table.
		/// </summary>
		/// <param name="programCourses">A list of program courses.</param>
		/// <returns>The identity insert value.</returns>
		Int32 InsertProgramCourseData(List<Stage_ProgramCourses> programCourses);

		/// <summary>
		/// Get course information from course isssued form table.
		/// </summary>
		/// <param name="courseName">The course name.</param>
		/// <returns>Return the course issued form.</returns>
		Stage_Course_IssuedForm GetCourseData(string courseName);
	}
}
