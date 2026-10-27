using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200002D RID: 45
	public struct InputMode
	{
		// Token: 0x040000D9 RID: 217
		public InputModeType Type;

		// Token: 0x040000DA RID: 218
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Shortcut;
	}
}
