using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200001A RID: 26
	public struct User
	{
		// Token: 0x04000093 RID: 147
		public long Id;

		// Token: 0x04000094 RID: 148
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Username;

		// Token: 0x04000095 RID: 149
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
		public string Discriminator;

		// Token: 0x04000096 RID: 150
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Avatar;

		// Token: 0x04000097 RID: 151
		public bool Bot;
	}
}
