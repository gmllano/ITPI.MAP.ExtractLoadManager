using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Class to store course issued form information.
	/// </summary>
	public class Stage_Course_IssuedForm
	{
		public int Outline_Id { get; set; }

		public string CourseName { get; set; }

		public int IssuedFormId { get; set; }

		public int Subject_Id { get; set; }

		public int Division_Id { get; set; }

		public int Department_Id { get; set; }

		public string Course_Number { get; set; }

		public string Course_Title { get; set; }

		public int Unit_Id { get; set; }
	}
}
