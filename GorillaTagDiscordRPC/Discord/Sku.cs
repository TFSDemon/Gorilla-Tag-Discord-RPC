using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200002C RID: 44
	public struct Sku
	{
		// Token: 0x040000D5 RID: 213
		public long Id;

		// Token: 0x040000D6 RID: 214
		public SkuType Type;

		// Token: 0x040000D7 RID: 215
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Name;

		// Token: 0x040000D8 RID: 216
		public SkuPrice Price;
	}
}
