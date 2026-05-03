using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200002B RID: 43
	public struct SkuPrice
	{
		// Token: 0x040000D3 RID: 211
		public uint Amount;

		// Token: 0x040000D4 RID: 212
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string Currency;
	}
}
