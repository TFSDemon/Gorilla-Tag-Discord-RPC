using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200002F RID: 47
	public struct LobbyTransaction
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002758 File Offset: 0x00000958
		private LobbyTransaction.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyTransaction.FFIMethods));
				}
				return (LobbyTransaction.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027A0 File Offset: 0x000009A0
		public void SetType(LobbyType type)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.SetType(this.MethodsPtr, type);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027EC File Offset: 0x000009EC
		public void SetOwner(long ownerId)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.SetOwner(this.MethodsPtr, ownerId);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002838 File Offset: 0x00000A38
		public void SetCapacity(uint capacity)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.SetCapacity(this.MethodsPtr, capacity);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002884 File Offset: 0x00000A84
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

		// Token: 0x06000025 RID: 37 RVA: 0x000028D4 File Offset: 0x00000AD4
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

		// Token: 0x06000026 RID: 38 RVA: 0x00002920 File Offset: 0x00000B20
		public void SetLocked(bool locked)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.SetLocked(this.MethodsPtr, locked);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x040000DF RID: 223
		internal IntPtr MethodsPtr;

		// Token: 0x040000E0 RID: 224
		internal object MethodsStructure;

		// Token: 0x0200004E RID: 78
		internal struct FFIMethods
		{
			// Token: 0x04000151 RID: 337
			internal LobbyTransaction.FFIMethods.SetTypeMethod SetType;

			// Token: 0x04000152 RID: 338
			internal LobbyTransaction.FFIMethods.SetOwnerMethod SetOwner;

			// Token: 0x04000153 RID: 339
			internal LobbyTransaction.FFIMethods.SetCapacityMethod SetCapacity;

			// Token: 0x04000154 RID: 340
			internal LobbyTransaction.FFIMethods.SetMetadataMethod SetMetadata;

			// Token: 0x04000155 RID: 341
			internal LobbyTransaction.FFIMethods.DeleteMetadataMethod DeleteMetadata;

			// Token: 0x04000156 RID: 342
			internal LobbyTransaction.FFIMethods.SetLockedMethod SetLocked;

			// Token: 0x020000AE RID: 174
			// (Invoke) Token: 0x06000275 RID: 629
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetTypeMethod(IntPtr methodsPtr, LobbyType type);

			// Token: 0x020000AF RID: 175
			// (Invoke) Token: 0x06000279 RID: 633
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetOwnerMethod(IntPtr methodsPtr, long ownerId);

			// Token: 0x020000B0 RID: 176
			// (Invoke) Token: 0x0600027D RID: 637
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetCapacityMethod(IntPtr methodsPtr, uint capacity);

			// Token: 0x020000B1 RID: 177
			// (Invoke) Token: 0x06000281 RID: 641
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000B2 RID: 178
			// (Invoke) Token: 0x06000285 RID: 645
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key);

			// Token: 0x020000B3 RID: 179
			// (Invoke) Token: 0x06000289 RID: 649
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetLockedMethod(IntPtr methodsPtr, bool locked);
		}
	}
}
