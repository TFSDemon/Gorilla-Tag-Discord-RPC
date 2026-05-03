using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000038 RID: 56
	public class RelationshipManager
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003C90 File Offset: 0x00001E90
		private RelationshipManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(RelationshipManager.FFIMethods));
				}
				return (RelationshipManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000065 RID: 101 RVA: 0x00003CD8 File Offset: 0x00001ED8
		// (remove) Token: 0x06000066 RID: 102 RVA: 0x00003D10 File Offset: 0x00001F10
	
		public event RelationshipManager.RefreshHandler OnRefresh;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000067 RID: 103 RVA: 0x00003D48 File Offset: 0x00001F48
		// (remove) Token: 0x06000068 RID: 104 RVA: 0x00003D80 File Offset: 0x00001F80

		public event RelationshipManager.RelationshipUpdateHandler OnRelationshipUpdate;

		// Token: 0x06000069 RID: 105 RVA: 0x00003DB8 File Offset: 0x00001FB8
		internal RelationshipManager(IntPtr ptr, IntPtr eventsPtr, ref RelationshipManager.FFIEvents events)
		{
			bool flag = eventsPtr == IntPtr.Zero;
			if (flag)
			{
				throw new ResultException(Result.InternalError);
			}
			this.InitEvents(eventsPtr, ref events);
			this.MethodsPtr = ptr;
			bool flag2 = this.MethodsPtr == IntPtr.Zero;
			if (flag2)
			{
				throw new ResultException(Result.InternalError);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E10 File Offset: 0x00002010
		private void InitEvents(IntPtr eventsPtr, ref RelationshipManager.FFIEvents events)
		{
			events.OnRefresh = new RelationshipManager.FFIEvents.RefreshHandler(RelationshipManager.OnRefreshImpl);
			events.OnRelationshipUpdate = new RelationshipManager.FFIEvents.RelationshipUpdateHandler(RelationshipManager.OnRelationshipUpdateImpl);
			Marshal.StructureToPtr<RelationshipManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E48 File Offset: 0x00002048
		[MonoPInvokeCallback]
		private static bool FilterCallbackImpl(IntPtr ptr, ref Relationship relationship)
		{
			RelationshipManager.FilterHandler filterHandler = (RelationshipManager.FilterHandler)GCHandle.FromIntPtr(ptr).Target;
			return filterHandler(ref relationship);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E78 File Offset: 0x00002078
		public void Filter(RelationshipManager.FilterHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.Filter(this.MethodsPtr, GCHandle.ToIntPtr(value), new RelationshipManager.FFIMethods.FilterCallback(RelationshipManager.FilterCallbackImpl));
			value.Free();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003EC0 File Offset: 0x000020C0
		public int Count()
		{
			int result = 0;
			Result result2 = this.Methods.Count(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F00 File Offset: 0x00002100
		public Relationship Get(long userId)
		{
			Relationship result = default(Relationship);
			Result result2 = this.Methods.Get(this.MethodsPtr, userId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F48 File Offset: 0x00002148
		public Relationship GetAt(uint index)
		{
			Relationship result = default(Relationship);
			Result result2 = this.Methods.GetAt(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F90 File Offset: 0x00002190
		[MonoPInvokeCallback]
		private static void OnRefreshImpl(IntPtr ptr)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.RelationshipManagerInstance.OnRefresh != null;
			if (flag)
			{
				discord.RelationshipManagerInstance.OnRefresh();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003FD8 File Offset: 0x000021D8
		[MonoPInvokeCallback]
		private static void OnRelationshipUpdateImpl(IntPtr ptr, ref Relationship relationship)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.RelationshipManagerInstance.OnRelationshipUpdate != null;
			if (flag)
			{
				discord.RelationshipManagerInstance.OnRelationshipUpdate(ref relationship);
			}
		}

		// Token: 0x04000117 RID: 279
		private IntPtr MethodsPtr;

		// Token: 0x04000118 RID: 280
		private object MethodsStructure;

		// Token: 0x02000061 RID: 97
		internal struct FFIEvents
		{
			// Token: 0x04000195 RID: 405
			internal RelationshipManager.FFIEvents.RefreshHandler OnRefresh;

			// Token: 0x04000196 RID: 406
			internal RelationshipManager.FFIEvents.RelationshipUpdateHandler OnRelationshipUpdate;

			// Token: 0x020000DC RID: 220
			// (Invoke) Token: 0x0600032D RID: 813
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void RefreshHandler(IntPtr ptr);

			// Token: 0x020000DD RID: 221
			// (Invoke) Token: 0x06000331 RID: 817
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void RelationshipUpdateHandler(IntPtr ptr, ref Relationship relationship);
		}

		// Token: 0x02000062 RID: 98
		internal struct FFIMethods
		{
			// Token: 0x04000197 RID: 407
			internal RelationshipManager.FFIMethods.FilterMethod Filter;

			// Token: 0x04000198 RID: 408
			internal RelationshipManager.FFIMethods.CountMethod Count;

			// Token: 0x04000199 RID: 409
			internal RelationshipManager.FFIMethods.GetMethod Get;

			// Token: 0x0400019A RID: 410
			internal RelationshipManager.FFIMethods.GetAtMethod GetAt;

			// Token: 0x020000DE RID: 222
			// (Invoke) Token: 0x06000335 RID: 821
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate bool FilterCallback(IntPtr ptr, ref Relationship relationship);

			// Token: 0x020000DF RID: 223
			// (Invoke) Token: 0x06000339 RID: 825
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FilterMethod(IntPtr methodsPtr, IntPtr callbackData, RelationshipManager.FFIMethods.FilterCallback callback);

			// Token: 0x020000E0 RID: 224
			// (Invoke) Token: 0x0600033D RID: 829
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result CountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x020000E1 RID: 225
			// (Invoke) Token: 0x06000341 RID: 833
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMethod(IntPtr methodsPtr, long userId, ref Relationship relationship);

			// Token: 0x020000E2 RID: 226
			// (Invoke) Token: 0x06000345 RID: 837
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetAtMethod(IntPtr methodsPtr, uint index, ref Relationship relationship);
		}

		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x06000182 RID: 386
		public delegate bool FilterHandler(ref Relationship relationship);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000186 RID: 390
		public delegate void RefreshHandler();

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x0600018A RID: 394
		public delegate void RelationshipUpdateHandler(ref Relationship relationship);
	}
}
