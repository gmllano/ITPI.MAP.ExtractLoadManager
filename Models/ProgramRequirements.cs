namespace ITPI.MAP.ExtractLoadManager
{	
	public class ProgramRequirements
	{
		public string ProgramID { get; set; }	
		
		public string CatalogYear { get; set; }
		
		public string ProgramTitle { get; set; }
		
		public string RequirementDescription { get; set; }
		
		public string RequirementID { get; set; }

		public int? topBlockId { get; set; }
		
		public int? subBlock { get; set; }
		
		public string ACRB_PRINTED_SPEC { get; set; }
		
		public string ACRB_COURSES_CRS_NAME { get; set; }
		
		public string ACRB_COURSES { get; set; }
		
		public string ACRB_FROM_COURSES_CRS_NAME { get; set; }
		
		public string ACRB_FROM_COURSES { get; set; }
		
		public string ACRB_FROM_SUBJECTS { get; set; }
		
		public string ACRB_ACAD_CRED_RULES { get; set; }
		
		public string ACRB_MIN_CRED { get; set; }
		
		public string ACRB_MIN_NO_COURSES { get; set; }
	}
}