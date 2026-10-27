using System;

namespace Discord
{
	// Token: 0x02000032 RID: 50
	public class ResultException : Exception
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public ResultException(Result result) : base(result.ToString())
		{
		}

		// Token: 0x040000E5 RID: 229
		public readonly Result Result;
	}
}
