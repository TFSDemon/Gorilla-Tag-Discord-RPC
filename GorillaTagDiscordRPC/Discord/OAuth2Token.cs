using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200001B RID: 27
	public struct OAuth2Token
	{
		// Token: 0x04000098 RID: 152
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AccessToken;

		// Token: 0x04000099 RID: 153
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
		public string Scopes;

		// Token: 0x0400009A RID: 154
		public long Expires;
	}
}
