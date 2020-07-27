namespace ITPI.MAP.ExtractLoadManager
{
	
	public class ProgramsCatalogsCreditReq
	{
		public string ProgramID { get; set; }

		public string ProgramTitle { get; set; }

		public string CatalogYear { get; set; }

		public string Minimum_Institutional_credits { get; set; }

		public float? Minimum_credits { get; set; }

		public string Minimum_Institutional_GPA { get; set; }

		public string ACPG_STATUS { get; set; }
	}
}