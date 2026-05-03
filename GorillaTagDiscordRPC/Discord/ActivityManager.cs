using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000002 RID: 2
	public class ActivityManager
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void RegisterCommand()
		{
			this.RegisterCommand(null);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		private ActivityManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ActivityManager.FFIMethods));
				}
				return (ActivityManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		
		public event ActivityManager.ActivityJoinHandler OnActivityJoin;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x0000214C File Offset: 0x0000034C
		
		public event ActivityManager.ActivitySpectateHandler OnActivitySpectate;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002184 File Offset: 0x00000384
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public event ActivityManager.ActivityJoinRequestHandler OnActivityJoinRequest;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000009 RID: 9 RVA: 0x000021F4 File Offset: 0x000003F4
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x0000222C File Offset: 0x0000042C
		public event ActivityManager.ActivityInviteHandler OnActivityInvite;

		// Token: 0x0600000B RID: 11 RVA: 0x00002264 File Offset: 0x00000464
		internal ActivityManager(IntPtr ptr, IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
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

		// Token: 0x0600000C RID: 12 RVA: 0x000022BC File Offset: 0x000004BC
		private void InitEvents(IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
		{
			events.OnActivityJoin = new ActivityManager.FFIEvents.ActivityJoinHandler(ActivityManager.OnActivityJoinImpl);
			events.OnActivitySpectate = new ActivityManager.FFIEvents.ActivitySpectateHandler(ActivityManager.OnActivitySpectateImpl);
			events.OnActivityJoinRequest = new ActivityManager.FFIEvents.ActivityJoinRequestHandler(ActivityManager.OnActivityJoinRequestImpl);
			events.OnActivityInvite = new ActivityManager.FFIEvents.ActivityInviteHandler(ActivityManager.OnActivityInviteImpl);
			Marshal.StructureToPtr<ActivityManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002320 File Offset: 0x00000520
		public void RegisterCommand(string command)
		{
			Result result = this.Methods.RegisterCommand(this.MethodsPtr, command);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002358 File Offset: 0x00000558
		public void RegisterSteam(uint steamId)
		{
			Result result = this.Methods.RegisterSteam(this.MethodsPtr, steamId);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002390 File Offset: 0x00000590
		[MonoPInvokeCallback]
		private static void UpdateActivityCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ActivityManager.UpdateActivityHandler updateActivityHandler = (ActivityManager.UpdateActivityHandler)gchandle.Target;
			gchandle.Free();
			updateActivityHandler(result);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C4 File Offset: 0x000005C4
		public void UpdateActivity(Activity activity, ActivityManager.UpdateActivityHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.UpdateActivity(this.MethodsPtr, ref activity, GCHandle.ToIntPtr(value), new ActivityManager.FFIMethods.UpdateActivityCallback(ActivityManager.UpdateActivityCallbackImpl));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002404 File Offset: 0x00000604
		[MonoPInvokeCallback]
		private static void ClearActivityCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ActivityManager.ClearActivityHandler clearActivityHandler = (ActivityManager.ClearActivityHandler)gchandle.Target;
			gchandle.Free();
			clearActivityHandler(result);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002438 File Offset: 0x00000638
		public void ClearActivity(ActivityManager.ClearActivityHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ClearActivity(this.MethodsPtr, GCHandle.ToIntPtr(value), new ActivityManager.FFIMethods.ClearActivityCallback(ActivityManager.ClearActivityCallbackImpl));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002478 File Offset: 0x00000678
		[MonoPInvokeCallback]
		private static void SendRequestReplyCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ActivityManager.SendRequestReplyHandler sendRequestReplyHandler = (ActivityManager.SendRequestReplyHandler)gchandle.Target;
			gchandle.Free();
			sendRequestReplyHandler(result);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024AC File Offset: 0x000006AC
		public void SendRequestReply(long userId, ActivityJoinRequestReply reply, ActivityManager.SendRequestReplyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SendRequestReply(this.MethodsPtr, userId, reply, GCHandle.ToIntPtr(value), new ActivityManager.FFIMethods.SendRequestReplyCallback(ActivityManager.SendRequestReplyCallbackImpl));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024EC File Offset: 0x000006EC
		[MonoPInvokeCallback]
		private static void SendInviteCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ActivityManager.SendInviteHandler sendInviteHandler = (ActivityManager.SendInviteHandler)gchandle.Target;
			gchandle.Free();
			sendInviteHandler(result);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002520 File Offset: 0x00000720
		public void SendInvite(long userId, ActivityActionType type, string content, ActivityManager.SendInviteHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SendInvite(this.MethodsPtr, userId, type, content, GCHandle.ToIntPtr(value), new ActivityManager.FFIMethods.SendInviteCallback(ActivityManager.SendInviteCallbackImpl));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002564 File Offset: 0x00000764
		[MonoPInvokeCallback]
		private static void AcceptInviteCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ActivityManager.AcceptInviteHandler acceptInviteHandler = (ActivityManager.AcceptInviteHandler)gchandle.Target;
			gchandle.Free();
			acceptInviteHandler(result);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002598 File Offset: 0x00000798
		public void AcceptInvite(long userId, ActivityManager.AcceptInviteHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.AcceptInvite(this.MethodsPtr, userId, GCHandle.ToIntPtr(value), new ActivityManager.FFIMethods.AcceptInviteCallback(ActivityManager.AcceptInviteCallbackImpl));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025D8 File Offset: 0x000007D8
		[MonoPInvokeCallback]
		private static void OnActivityJoinImpl(IntPtr ptr, string secret)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.ActivityManagerInstance.OnActivityJoin != null;
			if (flag)
			{
				discord.ActivityManagerInstance.OnActivityJoin(secret);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002620 File Offset: 0x00000820
		[MonoPInvokeCallback]
		private static void OnActivitySpectateImpl(IntPtr ptr, string secret)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.ActivityManagerInstance.OnActivitySpectate != null;
			if (flag)
			{
				discord.ActivityManagerInstance.OnActivitySpectate(secret);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002668 File Offset: 0x00000868
		[MonoPInvokeCallback]
		private static void OnActivityJoinRequestImpl(IntPtr ptr, ref User user)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.ActivityManagerInstance.OnActivityJoinRequest != null;
			if (flag)
			{
				discord.ActivityManagerInstance.OnActivityJoinRequest(ref user);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026B0 File Offset: 0x000008B0
		[MonoPInvokeCallback]
		private static void OnActivityInviteImpl(IntPtr ptr, ActivityActionType type, ref User user, ref Activity activity)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.ActivityManagerInstance.OnActivityInvite != null;
			if (flag)
			{
				discord.ActivityManagerInstance.OnActivityInvite(type, ref user, ref activity);
			}
		}

		// Token: 0x04000001 RID: 1
		private IntPtr MethodsPtr;

		// Token: 0x04000002 RID: 2
		private object MethodsStructure;

		// Token: 0x02000043 RID: 67
		internal struct FFIEvents
		{
			// Token: 0x04000146 RID: 326
			internal ActivityManager.FFIEvents.ActivityJoinHandler OnActivityJoin;

			// Token: 0x04000147 RID: 327
			internal ActivityManager.FFIEvents.ActivitySpectateHandler OnActivitySpectate;

			// Token: 0x04000148 RID: 328
			internal ActivityManager.FFIEvents.ActivityJoinRequestHandler OnActivityJoinRequest;

			// Token: 0x04000149 RID: 329
			internal ActivityManager.FFIEvents.ActivityInviteHandler OnActivityInvite;

			// Token: 0x0200009E RID: 158
			// (Invoke) Token: 0x06000235 RID: 565
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ActivityJoinHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

			// Token: 0x0200009F RID: 159
			// (Invoke) Token: 0x06000239 RID: 569
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ActivitySpectateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

			// Token: 0x020000A0 RID: 160
			// (Invoke) Token: 0x0600023D RID: 573
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ActivityJoinRequestHandler(IntPtr ptr, ref User user);

			// Token: 0x020000A1 RID: 161
			// (Invoke) Token: 0x06000241 RID: 577
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ActivityInviteHandler(IntPtr ptr, ActivityActionType type, ref User user, ref Activity activity);
		}

		// Token: 0x02000044 RID: 68
		internal struct FFIMethods
		{
			// Token: 0x0400014A RID: 330
			internal ActivityManager.FFIMethods.RegisterCommandMethod RegisterCommand;

			// Token: 0x0400014B RID: 331
			internal ActivityManager.FFIMethods.RegisterSteamMethod RegisterSteam;

			// Token: 0x0400014C RID: 332
			internal ActivityManager.FFIMethods.UpdateActivityMethod UpdateActivity;

			// Token: 0x0400014D RID: 333
			internal ActivityManager.FFIMethods.ClearActivityMethod ClearActivity;

			// Token: 0x0400014E RID: 334
			internal ActivityManager.FFIMethods.SendRequestReplyMethod SendRequestReply;

			// Token: 0x0400014F RID: 335
			internal ActivityManager.FFIMethods.SendInviteMethod SendInvite;

			// Token: 0x04000150 RID: 336
			internal ActivityManager.FFIMethods.AcceptInviteMethod AcceptInvite;

			// Token: 0x020000A2 RID: 162
			// (Invoke) Token: 0x06000245 RID: 581
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result RegisterCommandMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string command);

			// Token: 0x020000A3 RID: 163
			// (Invoke) Token: 0x06000249 RID: 585
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result RegisterSteamMethod(IntPtr methodsPtr, uint steamId);

			// Token: 0x020000A4 RID: 164
			// (Invoke) Token: 0x0600024D RID: 589
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateActivityCallback(IntPtr ptr, Result result);

			// Token: 0x020000A5 RID: 165
			// (Invoke) Token: 0x06000251 RID: 593
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateActivityMethod(IntPtr methodsPtr, ref Activity activity, IntPtr callbackData, ActivityManager.FFIMethods.UpdateActivityCallback callback);

			// Token: 0x020000A6 RID: 166
			// (Invoke) Token: 0x06000255 RID: 597
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ClearActivityCallback(IntPtr ptr, Result result);

			// Token: 0x020000A7 RID: 167
			// (Invoke) Token: 0x06000259 RID: 601
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ClearActivityMethod(IntPtr methodsPtr, IntPtr callbackData, ActivityManager.FFIMethods.ClearActivityCallback callback);

			// Token: 0x020000A8 RID: 168
			// (Invoke) Token: 0x0600025D RID: 605
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendRequestReplyCallback(IntPtr ptr, Result result);

			// Token: 0x020000A9 RID: 169
			// (Invoke) Token: 0x06000261 RID: 609
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendRequestReplyMethod(IntPtr methodsPtr, long userId, ActivityJoinRequestReply reply, IntPtr callbackData, ActivityManager.FFIMethods.SendRequestReplyCallback callback);

			// Token: 0x020000AA RID: 170
			// (Invoke) Token: 0x06000265 RID: 613
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendInviteCallback(IntPtr ptr, Result result);

			// Token: 0x020000AB RID: 171
			// (Invoke) Token: 0x06000269 RID: 617
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendInviteMethod(IntPtr methodsPtr, long userId, ActivityActionType type, [MarshalAs(UnmanagedType.LPStr)] string content, IntPtr callbackData, ActivityManager.FFIMethods.SendInviteCallback callback);

			// Token: 0x020000AC RID: 172
			// (Invoke) Token: 0x0600026D RID: 621
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void AcceptInviteCallback(IntPtr ptr, Result result);

			// Token: 0x020000AD RID: 173
			// (Invoke) Token: 0x06000271 RID: 625
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void AcceptInviteMethod(IntPtr methodsPtr, long userId, IntPtr callbackData, ActivityManager.FFIMethods.AcceptInviteCallback callback);
		}

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000142 RID: 322
		public delegate void UpdateActivityHandler(Result result);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x06000146 RID: 326
		public delegate void ClearActivityHandler(Result result);

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x0600014A RID: 330
		public delegate void SendRequestReplyHandler(Result result);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x0600014E RID: 334
		public delegate void SendInviteHandler(Result result);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x06000152 RID: 338
		public delegate void AcceptInviteHandler(Result result);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x06000156 RID: 342
		public delegate void ActivityJoinHandler(string secret);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x0600015A RID: 346
		public delegate void ActivitySpectateHandler(string secret);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x0600015E RID: 350
		public delegate void ActivityJoinRequestHandler(ref User user);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x06000162 RID: 354
		public delegate void ActivityInviteHandler(ActivityActionType type, ref User user, ref Activity activity);
	}
}
