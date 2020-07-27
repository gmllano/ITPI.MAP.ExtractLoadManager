using FileHelpers;
using System;

namespace ITPI.MAP.ExtractLoadManager
{
	[DelimitedRecord(",")]
	public class ProgramsCatalogsCreditReq
	{
		[FieldOptional()]
		public string ProgramID { get; set; }

		[FieldOptional()]
		public string ProgramTitle { get; set; }

		[FieldOptional()]
		public string CatalogYear { get; set; }

		[FieldOptional()]
		public string Minimum_Institutional_credits { get; set; }

		[FieldOptional()]
		public float? Minimum_credits { get; set; }

		[FieldOptional()]
		public string Minimum_Institutional_GPA { get; set; }

		[FieldOptional()]
		public string ACPG_STATUS { get; set; }
	}
}