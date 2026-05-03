using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000021 RID: 33
	public struct ActivityParty
	{
		// Token: 0x040000A8 RID: 168
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Id;

		// Token: 0x040000A9 RID: 169
		public PartySize Size;

		// Token: 0x040000AA RID: 170
		public ActivityPartyPrivacy Privacy;
	}
}
