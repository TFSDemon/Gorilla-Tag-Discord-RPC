using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000036 RID: 54
	public class UserManager
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000037E8 File Offset: 0x000019E8
		private UserManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(UserManager.FFIMethods));
				}
				return (UserManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000051 RID: 81 RVA: 0x00003830 File Offset: 0x00001A30
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x00003868 File Offset: 0x00001A68

		public event UserManager.CurrentUserUpdateHandler OnCurrentUserUpdate;

		// Token: 0x06000053 RID: 83 RVA: 0x000038A0 File Offset: 0x00001AA0
		internal UserManager(IntPtr ptr, IntPtr eventsPtr, ref UserManager.FFIEvents events)
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

		// Token: 0x06000054 RID: 84 RVA: 0x000038F8 File Offset: 0x00001AF8
		private void InitEvents(IntPtr eventsPtr, ref UserManager.FFIEvents events)
		{
			events.OnCurrentUserUpdate = new UserManager.FFIEvents.CurrentUserUpdateHandler(UserManager.OnCurrentUserUpdateImpl);
			Marshal.StructureToPtr<UserManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000391C File Offset: 0x00001B1C
		public User GetCurrentUser()
		{
			User result = default(User);
			Result result2 = this.Methods.GetCurrentUser(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003960 File Offset: 0x00001B60
		[MonoPInvokeCallback]
		private static void GetUserCallbackImpl(IntPtr ptr, Result result, ref User user)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			UserManager.GetUserHandler getUserHandler = (UserManager.GetUserHandler)gchandle.Target;
			gchandle.Free();
			getUserHandler(result, ref user);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003994 File Offset: 0x00001B94
		public void GetUser(long userId, UserManager.GetUserHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.GetUser(this.MethodsPtr, userId, GCHandle.ToIntPtr(value), new UserManager.FFIMethods.GetUserCallback(UserManager.GetUserCallbackImpl));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000039D4 File Offset: 0x00001BD4
		public PremiumType GetCurrentUserPremiumType()
		{
			PremiumType result = PremiumType.None;
			Result result2 = this.Methods.GetCurrentUserPremiumType(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A14 File Offset: 0x00001C14
		public bool CurrentUserHasFlag(UserFlag flag)
		{
			bool result = false;
			Result result2 = this.Methods.CurrentUserHasFlag(this.MethodsPtr, flag, ref result);
			bool flag2 = result2 > Result.Ok;
			if (flag2)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003A54 File Offset: 0x00001C54
		[MonoPInvokeCallback]
		private static void OnCurrentUserUpdateImpl(IntPtr ptr)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.UserManagerInstance.OnCurrentUserUpdate != null;
			if (flag)
			{
				discord.UserManagerInstance.OnCurrentUserUpdate();
			}
		}

		// Token: 0x04000112 RID: 274
		private IntPtr MethodsPtr;

		// Token: 0x04000113 RID: 275
		private object MethodsStructure;

		// Token: 0x0200005A RID: 90
		internal struct FFIEvents
		{
			// Token: 0x0400018D RID: 397
			internal UserManager.FFIEvents.CurrentUserUpdateHandler OnCurrentUserUpdate;

			// Token: 0x020000D2 RID: 210
			// (Invoke) Token: 0x06000305 RID: 773
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CurrentUserUpdateHandler(IntPtr ptr);
		}

		// Token: 0x0200005B RID: 91
		internal struct FFIMethods
		{
			// Token: 0x0400018E RID: 398
			internal UserManager.FFIMethods.GetCurrentUserMethod GetCurrentUser;

			// Token: 0x0400018F RID: 399
			internal UserManager.FFIMethods.GetUserMethod GetUser;

			// Token: 0x04000190 RID: 400
			internal UserManager.FFIMethods.GetCurrentUserPremiumTypeMethod GetCurrentUserPremiumType;

			// Token: 0x04000191 RID: 401
			internal UserManager.FFIMethods.CurrentUserHasFlagMethod CurrentUserHasFlag;

			// Token: 0x020000D3 RID: 211
			// (Invoke) Token: 0x06000309 RID: 777
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetCurrentUserMethod(IntPtr methodsPtr, ref User currentUser);

			// Token: 0x020000D4 RID: 212
			// (Invoke) Token: 0x0600030D RID: 781
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetUserCallback(IntPtr ptr, Result result, ref User user);

			// Token: 0x020000D5 RID: 213
			// (Invoke) Token: 0x06000311 RID: 785
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetUserMethod(IntPtr methodsPtr, long userId, IntPtr callbackData, UserManager.FFIMethods.GetUserCallback callback);

			// Token: 0x020000D6 RID: 214
			// (Invoke) Token: 0x06000315 RID: 789
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetCurrentUserPremiumTypeMethod(IntPtr methodsPtr, ref PremiumType premiumType);

			// Token: 0x020000D7 RID: 215
			// (Invoke) Token: 0x06000319 RID: 793
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result CurrentUserHasFlagMethod(IntPtr methodsPtr, UserFlag flag, ref bool hasFlag);
		}

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x06000176 RID: 374
		public delegate void GetUserHandler(Result result, ref User user);

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x0600017A RID: 378
		public delegate void CurrentUserUpdateHandler();
	}
}
