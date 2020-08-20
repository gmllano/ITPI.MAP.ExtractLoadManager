using System;

namespace ITPI.MAP.ExtractLoadManager.Models
{
	/// <summary>
	/// Class responsible to store program course information.
	/// </summary>
	public class Stage_ProgramCourses
	{
		public int Program_Id { get; set; }
		public int Outline_Id { get; set; }
		public int Required { get; set; }
		public int? ISubOrder { get; set; }
		public int? IOrder { get; set; }
		public Int16? Or_Group { get; set; }
		public int? C_Group { get; set; }
		public string Group_Desc { get; set; }
		public string Group_Units { get; set; }
		public decimal? VUnits { get; set; }
		public string ICross { get; set; }
		public int? Condition { get; set; }
		public int? Semester { get; set; }
		public int? Group_Units_Min { get; set; }
		public int? Group_Units_Max { get; set; }
		public bool Articulate { get; set; }
		public bool IsElective { get; set; }
	}
}
