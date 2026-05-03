using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000022 RID: 34
	public struct ActivitySecrets
	{
		// Token: 0x040000AB RID: 171
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Match;

		// Token: 0x040000AC RID: 172
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Join;

		// Token: 0x040000AD RID: 173
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Spectate;
	}
}
