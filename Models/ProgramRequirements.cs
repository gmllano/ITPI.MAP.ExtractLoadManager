using FileHelpers;

namespace ITPI.MAP.ExtractLoadManager
{

	[DelimitedRecord(",")]
	public class ProgramRequirements
	{
		[FieldOptional()]
		public string ProgramID { get; set; }

		[FieldOptional()]
		public string CatalogYear { get; set; }

		[FieldOptional()]
		public string ProgramTitle { get; set; }

		[FieldOptional()]
		public string RequirementDescription { get; set; }

		[FieldOptional()]
		public string RequirementID { get; set; }

		[FieldOptional()]
		public int? topBlockId { get; set; }

		[FieldOptional()]
		public int? subBlock { get; set; }

		[FieldOptional()]
		public string ACRB_PRINTED_SPEC { get; set; }

		[FieldOptional()]
		public string ACRB_COURSES_CRS_NAME { get; set; }

		[FieldOptional()]
		public string ACRB_COURSES { get; set; }

		[FieldOptional()]
		public string ACRB_FROM_COURSES_CRS_NAME { get; set; }

		[FieldOptional()]
		public string ACRB_FROM_COURSES { get; set; }

		[FieldOptional()]
		public string ACRB_FROM_SUBJECTS { get; set; }

		[FieldOptional()]
		public string ACRB_ACAD_CRED_RULES { get; set; }

		[FieldOptional()]
		public string ACRB_MIN_CRED { get; set; }

		[FieldOptional()]
		public string ACRB_MIN_NO_COURSES { get; set; }

	}
}