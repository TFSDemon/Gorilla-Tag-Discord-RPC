using System;

namespace Discord
{
	// Token: 0x0200001C RID: 28
	public struct ImageHandle
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000026FC File Offset: 0x000008FC
		public static ImageHandle User(long id)
		{
			return ImageHandle.User(id, 128U);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000271C File Offset: 0x0000091C
		public static ImageHandle User(long id, uint size)
		{
			return new ImageHandle
			{
				Type = ImageType.User,
				Id = id,
				Size = size
			};
		}

		// Token: 0x0400009B RID: 155
		public ImageType Type;

		// Token: 0x0400009C RID: 156
		public long Id;

		// Token: 0x0400009D RID: 157
		public uint Size;
	}
}
