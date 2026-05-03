using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000023 RID: 35
	public struct Activity
	{
		// Token: 0x040000AE RID: 174
		public ActivityType Type;

		// Token: 0x040000AF RID: 175
		public long ApplicationId;

		// Token: 0x040000B0 RID: 176
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Name;

		// Token: 0x040000B1 RID: 177
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string State;

		// Token: 0x040000B2 RID: 178
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Details;

		// Token: 0x040000B3 RID: 179
		public ActivityTimestamps Timestamps;

		// Token: 0x040000B4 RID: 180
		public ActivityAssets Assets;

		// Token: 0x040000B5 RID: 181
		public ActivityParty Party;

		// Token: 0x040000B6 RID: 182
		public ActivitySecrets Secrets;

		// Token: 0x040000B7 RID: 183
		public bool Instance;

		// Token: 0x040000B8 RID: 184
		public uint SupportedPlatforms;
	}
}
