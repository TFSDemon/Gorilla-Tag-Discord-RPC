using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000029 RID: 41
	public struct FileStat
	{
		// Token: 0x040000CD RID: 205
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string Filename;

		// Token: 0x040000CE RID: 206
		public ulong Size;

		// Token: 0x040000CF RID: 207
		public ulong LastModified;
	}
}
