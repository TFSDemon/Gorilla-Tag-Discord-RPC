using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000031 RID: 49
	public struct LobbySearchQuery
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A50 File Offset: 0x00000C50
		private LobbySearchQuery.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbySearchQuery.FFIMethods));
				}
				return (LobbySearchQuery.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A98 File Offset: 0x00000C98
		public void Filter(string key, LobbySearchComparison comparison, LobbySearchCast cast, string value)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.Filter(this.MethodsPtr, key, comparison, cast, value);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void Sort(string key, LobbySearchCast cast, string value)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.Sort(this.MethodsPtr, key, cast, value);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B38 File Offset: 0x00000D38
		public void Limit(uint limit)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.Limit(this.MethodsPtr, limit);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B84 File Offset: 0x00000D84
		public void Distance(LobbySearchDistance distance)
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				Result result = this.Methods.Distance(this.MethodsPtr, distance);
				bool flag2 = result > Result.Ok;
				if (flag2)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x040000E3 RID: 227
		internal IntPtr MethodsPtr;

		// Token: 0x040000E4 RID: 228
		internal object MethodsStructure;

		// Token: 0x02000050 RID: 80
		internal struct FFIMethods
		{
			// Token: 0x04000159 RID: 345
			internal LobbySearchQuery.FFIMethods.FilterMethod Filter;

			// Token: 0x0400015A RID: 346
			internal LobbySearchQuery.FFIMethods.SortMethod Sort;

			// Token: 0x0400015B RID: 347
			internal LobbySearchQuery.FFIMethods.LimitMethod Limit;

			// Token: 0x0400015C RID: 348
			internal LobbySearchQuery.FFIMethods.DistanceMethod Distance;

			// Token: 0x020000B6 RID: 182
			// (Invoke) Token: 0x06000295 RID: 661
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result FilterMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchComparison comparison, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000B7 RID: 183
			// (Invoke) Token: 0x06000299 RID: 665
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SortMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000B8 RID: 184
			// (Invoke) Token: 0x0600029D RID: 669
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result LimitMethod(IntPtr methodsPtr, uint limit);

			// Token: 0x020000B9 RID: 185
			// (Invoke) Token: 0x060002A1 RID: 673
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result DistanceMethod(IntPtr methodsPtr, LobbySearchDistance distance);
		}
	}
}
