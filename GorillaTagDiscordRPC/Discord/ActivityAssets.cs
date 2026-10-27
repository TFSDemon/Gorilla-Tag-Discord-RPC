using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200001F RID: 31
	public struct ActivityAssets
	{
		// Token: 0x040000A2 RID: 162
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string LargeImage;

		// Token: 0x040000A3 RID: 163
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string LargeText;

		// Token: 0x040000A4 RID: 164
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string SmallImage;

		// Token: 0x040000A5 RID: 165
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string SmallText;
	}
}
