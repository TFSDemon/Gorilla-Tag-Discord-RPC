using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000030 RID: 48
	public struct LobbyMemberTransaction
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000296C File Offset: 0x00000B6C
		private LobbyMemberTransaction.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyMemberTransaction.FFIMethods));
				}
				return (LobbyMemberTransaction.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void SetMetadata(string key, string value)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.SetMetadata(this.MethodsPtr, key, value);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A04 File Offset: 0x00000C04
		public void DeleteMetadata(string key)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.DeleteMetadata(this.MethodsPtr, key);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x040000E1 RID: 225
		internal IntPtr MethodsPtr;

		// Token: 0x040000E2 RID: 226
		internal object MethodsStructure;

		// Token: 0x0200004F RID: 79
		internal struct FFIMethods
		{
			// Token: 0x04000157 RID: 343
			internal LobbyMemberTransaction.FFIMethods.SetMetadataMethod SetMetadata;

			// Token: 0x04000158 RID: 344
			internal LobbyMemberTransaction.FFIMethods.DeleteMetadataMethod DeleteMetadata;

			// Token: 0x020000B4 RID: 180
			// (Invoke) Token: 0x0600028D RID: 653
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000B5 RID: 181
			// (Invoke) Token: 0x06000291 RID: 657
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key);
		}
	}
}
