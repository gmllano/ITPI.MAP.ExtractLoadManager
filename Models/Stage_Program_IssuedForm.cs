using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	public class Stage_Program_IssuedForm
	{
		public int Program_Id { get; set; }

		public Int16? Status { get; set; }

		public string Archive_Info { get; set; }

		public string Program { get; set; }

		public string Description { get; set; }

		public int? Degree_Id { get; set; }

		public int? Division_Id { get; set; }

		public string Core_Units { get; set; }

		public string Elect_Units { get; set; }

		public string Tot_Units { get; set; }

		public DateTime? Approv_Date { get; set; }

		public int? IssuedFormId { get; set; }

		public string Reqelec_Text { get; set; }

		public decimal? Reqelec_Units { get; set; }

		public string Semester { get; set; }

		public string Comments { get; set; }

		public DateTime? Implementation_Date { get; set; }

		public int? College_Id { get; set; }

		public DateTime? Effective_Start_Date { get; set; }

		public DateTime? Effective_End_Date { get; set; }

		public int? Author_User_Id { get; set; }

		public int? Division_User_Id { get; set; }

		public int? Department_User_Id { get; set; }

		public string ProgramCoursesText { get; set; }

		public int? Department_Id { get; set; }
	}
}
