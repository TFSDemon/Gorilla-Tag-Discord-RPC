using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200003F RID: 63
	public class AchievementManager
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006B90 File Offset: 0x00004D90
		private AchievementManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(AchievementManager.FFIMethods));
				}
				return (AchievementManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000127 RID: 295 RVA: 0x00006BD8 File Offset: 0x00004DD8
		// (remove) Token: 0x06000128 RID: 296 RVA: 0x00006C10 File Offset: 0x00004E10
		
		public event AchievementManager.UserAchievementUpdateHandler OnUserAchievementUpdate;

		// Token: 0x06000129 RID: 297 RVA: 0x00006C48 File Offset: 0x00004E48
		internal AchievementManager(IntPtr ptr, IntPtr eventsPtr, ref AchievementManager.FFIEvents events)
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

		// Token: 0x0600012A RID: 298 RVA: 0x00006CA0 File Offset: 0x00004EA0
		private void InitEvents(IntPtr eventsPtr, ref AchievementManager.FFIEvents events)
		{
			events.OnUserAchievementUpdate = new AchievementManager.FFIEvents.UserAchievementUpdateHandler(AchievementManager.OnUserAchievementUpdateImpl);
			Marshal.StructureToPtr<AchievementManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006CC4 File Offset: 0x00004EC4
		[MonoPInvokeCallback]
		private static void SetUserAchievementCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			AchievementManager.SetUserAchievementHandler setUserAchievementHandler = (AchievementManager.SetUserAchievementHandler)gchandle.Target;
			gchandle.Free();
			setUserAchievementHandler(result);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public void SetUserAchievement(long achievementId, byte percentComplete, AchievementManager.SetUserAchievementHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SetUserAchievement(this.MethodsPtr, achievementId, percentComplete, GCHandle.ToIntPtr(value), new AchievementManager.FFIMethods.SetUserAchievementCallback(AchievementManager.SetUserAchievementCallbackImpl));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006D38 File Offset: 0x00004F38
		[MonoPInvokeCallback]
		private static void FetchUserAchievementsCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			AchievementManager.FetchUserAchievementsHandler fetchUserAchievementsHandler = (AchievementManager.FetchUserAchievementsHandler)gchandle.Target;
			gchandle.Free();
			fetchUserAchievementsHandler(result);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006D6C File Offset: 0x00004F6C
		public void FetchUserAchievements(AchievementManager.FetchUserAchievementsHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.FetchUserAchievements(this.MethodsPtr, GCHandle.ToIntPtr(value), new AchievementManager.FFIMethods.FetchUserAchievementsCallback(AchievementManager.FetchUserAchievementsCallbackImpl));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006DAC File Offset: 0x00004FAC
		public int CountUserAchievements()
		{
			int result = 0;
			this.Methods.CountUserAchievements(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006DDC File Offset: 0x00004FDC
		public UserAchievement GetUserAchievement(long userAchievementId)
		{
			UserAchievement result = default(UserAchievement);
			Result result2 = this.Methods.GetUserAchievement(this.MethodsPtr, userAchievementId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006E24 File Offset: 0x00005024
		public UserAchievement GetUserAchievementAt(int index)
		{
			UserAchievement result = default(UserAchievement);
			Result result2 = this.Methods.GetUserAchievementAt(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006E6C File Offset: 0x0000506C
		[MonoPInvokeCallback]
		private static void OnUserAchievementUpdateImpl(IntPtr ptr, ref UserAchievement userAchievement)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.AchievementManagerInstance.OnUserAchievementUpdate != null;
			if (flag)
			{
				discord.AchievementManagerInstance.OnUserAchievementUpdate(ref userAchievement);
			}
		}

		// Token: 0x04000135 RID: 309
		private IntPtr MethodsPtr;

		// Token: 0x04000136 RID: 310
		private object MethodsStructure;

		// Token: 0x02000098 RID: 152
		internal struct FFIEvents
		{
			// Token: 0x04000204 RID: 516
			internal AchievementManager.FFIEvents.UserAchievementUpdateHandler OnUserAchievementUpdate;

			// Token: 0x02000164 RID: 356
			// (Invoke) Token: 0x0600054D RID: 1357
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UserAchievementUpdateHandler(IntPtr ptr, ref UserAchievement userAchievement);
		}

		// Token: 0x02000099 RID: 153
		internal struct FFIMethods
		{
			// Token: 0x04000205 RID: 517
			internal AchievementManager.FFIMethods.SetUserAchievementMethod SetUserAchievement;

			// Token: 0x04000206 RID: 518
			internal AchievementManager.FFIMethods.FetchUserAchievementsMethod FetchUserAchievements;

			// Token: 0x04000207 RID: 519
			internal AchievementManager.FFIMethods.CountUserAchievementsMethod CountUserAchievements;

			// Token: 0x04000208 RID: 520
			internal AchievementManager.FFIMethods.GetUserAchievementMethod GetUserAchievement;

			// Token: 0x04000209 RID: 521
			internal AchievementManager.FFIMethods.GetUserAchievementAtMethod GetUserAchievementAt;

			// Token: 0x02000165 RID: 357
			// (Invoke) Token: 0x06000551 RID: 1361
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetUserAchievementCallback(IntPtr ptr, Result result);

			// Token: 0x02000166 RID: 358
			// (Invoke) Token: 0x06000555 RID: 1365
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetUserAchievementMethod(IntPtr methodsPtr, long achievementId, byte percentComplete, IntPtr callbackData, AchievementManager.FFIMethods.SetUserAchievementCallback callback);

			// Token: 0x02000167 RID: 359
			// (Invoke) Token: 0x06000559 RID: 1369
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchUserAchievementsCallback(IntPtr ptr, Result result);

			// Token: 0x02000168 RID: 360
			// (Invoke) Token: 0x0600055D RID: 1373
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchUserAchievementsMethod(IntPtr methodsPtr, IntPtr callbackData, AchievementManager.FFIMethods.FetchUserAchievementsCallback callback);

			// Token: 0x02000169 RID: 361
			// (Invoke) Token: 0x06000561 RID: 1377
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CountUserAchievementsMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x0200016A RID: 362
			// (Invoke) Token: 0x06000565 RID: 1381
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetUserAchievementMethod(IntPtr methodsPtr, long userAchievementId, ref UserAchievement userAchievement);

			// Token: 0x0200016B RID: 363
			// (Invoke) Token: 0x06000569 RID: 1385
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetUserAchievementAtMethod(IntPtr methodsPtr, int index, ref UserAchievement userAchievement);
		}

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x06000226 RID: 550
		public delegate void SetUserAchievementHandler(Result result);

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x0600022A RID: 554
		public delegate void FetchUserAchievementsHandler(Result result);

		// Token: 0x0200009C RID: 156
		// (Invoke) Token: 0x0600022E RID: 558
		public delegate void UserAchievementUpdateHandler(ref UserAchievement userAchievement);
	}
}
