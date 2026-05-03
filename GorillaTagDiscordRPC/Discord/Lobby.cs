using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000026 RID: 38
	public struct Lobby
	{
		// Token: 0x040000BE RID: 190
		public long Id;

		// Token: 0x040000BF RID: 191
		public LobbyType Type;

		// Token: 0x040000C0 RID: 192
		public long OwnerId;

		// Token: 0x040000C1 RID: 193
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Secret;

		// Token: 0x040000C2 RID: 194
		public uint Capacity;

		// Token: 0x040000C3 RID: 195
		public bool Locked;
	}
}
