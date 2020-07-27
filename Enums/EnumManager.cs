using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Enumerator types.
	/// </summary>
	public static class EnumManager
	{
		/// <summary>
		/// File Type enumerator.
		/// </summary>
		public enum FileTypes
		{
			CSV = 1,
			XML = 2
		}

		/// <summary>
		/// The program type.
		/// </summary>
		public enum programType
		{
			Programs = 1,
			ProgramRequirements = 2,
			ProgramsCatalogsCreditReq = 3
		}
	}
}
