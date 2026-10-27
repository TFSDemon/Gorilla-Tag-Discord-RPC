using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200002E RID: 46
	public struct UserAchievement
	{
		// Token: 0x040000DB RID: 219
		public long UserId;

		// Token: 0x040000DC RID: 220
		public long AchievementId;

		// Token: 0x040000DD RID: 221
		public byte PercentComplete;

		// Token: 0x040000DE RID: 222
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string UnlockedAt;
	}
}
