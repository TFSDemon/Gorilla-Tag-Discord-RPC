using System;

namespace Discord
{
	// Token: 0x0200001E RID: 30
	public struct ActivityTimestamps
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000274F File Offset: 0x0000094F
		public static implicit operator ActivityTimestamps(long v)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000A0 RID: 160
		public long Start;

		// Token: 0x040000A1 RID: 161
		public long End;
	}
}
