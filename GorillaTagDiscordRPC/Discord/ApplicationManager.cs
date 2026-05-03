using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x02000035 RID: 53
	public class ApplicationManager
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003560 File Offset: 0x00001760
		private ApplicationManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ApplicationManager.FFIMethods));
				}
				return (ApplicationManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000035A8 File Offset: 0x000017A8
		internal ApplicationManager(IntPtr ptr, IntPtr eventsPtr, ref ApplicationManager.FFIEvents events)
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

		// Token: 0x06000047 RID: 71 RVA: 0x00003600 File Offset: 0x00001800
		private void InitEvents(IntPtr eventsPtr, ref ApplicationManager.FFIEvents events)
		{
			Marshal.StructureToPtr<ApplicationManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003614 File Offset: 0x00001814
		[MonoPInvokeCallback]
		private static void ValidateOrExitCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ApplicationManager.ValidateOrExitHandler validateOrExitHandler = (ApplicationManager.ValidateOrExitHandler)gchandle.Target;
			gchandle.Free();
			validateOrExitHandler(result);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003648 File Offset: 0x00001848
		public void ValidateOrExit(ApplicationManager.ValidateOrExitHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ValidateOrExit(this.MethodsPtr, GCHandle.ToIntPtr(value), new ApplicationManager.FFIMethods.ValidateOrExitCallback(ApplicationManager.ValidateOrExitCallbackImpl));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003688 File Offset: 0x00001888
		public string GetCurrentLocale()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.Methods.GetCurrentLocale(this.MethodsPtr, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036C4 File Offset: 0x000018C4
		public string GetCurrentBranch()
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			this.Methods.GetCurrentBranch(this.MethodsPtr, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003700 File Offset: 0x00001900
		[MonoPInvokeCallback]
		private static void GetOAuth2TokenCallbackImpl(IntPtr ptr, Result result, ref OAuth2Token oauth2Token)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ApplicationManager.GetOAuth2TokenHandler getOAuth2TokenHandler = (ApplicationManager.GetOAuth2TokenHandler)gchandle.Target;
			gchandle.Free();
			getOAuth2TokenHandler(result, ref oauth2Token);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003734 File Offset: 0x00001934
		public void GetOAuth2Token(ApplicationManager.GetOAuth2TokenHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.GetOAuth2Token(this.MethodsPtr, GCHandle.ToIntPtr(value), new ApplicationManager.FFIMethods.GetOAuth2TokenCallback(ApplicationManager.GetOAuth2TokenCallbackImpl));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003774 File Offset: 0x00001974
		[MonoPInvokeCallback]
		private static void GetTicketCallbackImpl(IntPtr ptr, Result result, ref string data)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ApplicationManager.GetTicketHandler getTicketHandler = (ApplicationManager.GetTicketHandler)gchandle.Target;
			gchandle.Free();
			getTicketHandler(result, ref data);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000037A8 File Offset: 0x000019A8
		public void GetTicket(ApplicationManager.GetTicketHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.GetTicket(this.MethodsPtr, GCHandle.ToIntPtr(value), new ApplicationManager.FFIMethods.GetTicketCallback(ApplicationManager.GetTicketCallbackImpl));
		}

		// Token: 0x04000110 RID: 272
		private IntPtr MethodsPtr;

		// Token: 0x04000111 RID: 273
		private object MethodsStructure;

		// Token: 0x02000055 RID: 85
		internal struct FFIEvents
		{
		}

		// Token: 0x02000056 RID: 86
		internal struct FFIMethods
		{
			// Token: 0x04000188 RID: 392
			internal ApplicationManager.FFIMethods.ValidateOrExitMethod ValidateOrExit;

			// Token: 0x04000189 RID: 393
			internal ApplicationManager.FFIMethods.GetCurrentLocaleMethod GetCurrentLocale;

			// Token: 0x0400018A RID: 394
			internal ApplicationManager.FFIMethods.GetCurrentBranchMethod GetCurrentBranch;

			// Token: 0x0400018B RID: 395
			internal ApplicationManager.FFIMethods.GetOAuth2TokenMethod GetOAuth2Token;

			// Token: 0x0400018C RID: 396
			internal ApplicationManager.FFIMethods.GetTicketMethod GetTicket;

			// Token: 0x020000CA RID: 202
			// (Invoke) Token: 0x060002E5 RID: 741
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ValidateOrExitCallback(IntPtr ptr, Result result);

			// Token: 0x020000CB RID: 203
			// (Invoke) Token: 0x060002E9 RID: 745
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ValidateOrExitMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.ValidateOrExitCallback callback);

			// Token: 0x020000CC RID: 204
			// (Invoke) Token: 0x060002ED RID: 749
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetCurrentLocaleMethod(IntPtr methodsPtr, StringBuilder locale);

			// Token: 0x020000CD RID: 205
			// (Invoke) Token: 0x060002F1 RID: 753
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetCurrentBranchMethod(IntPtr methodsPtr, StringBuilder branch);

			// Token: 0x020000CE RID: 206
			// (Invoke) Token: 0x060002F5 RID: 757
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetOAuth2TokenCallback(IntPtr ptr, Result result, ref OAuth2Token oauth2Token);

			// Token: 0x020000CF RID: 207
			// (Invoke) Token: 0x060002F9 RID: 761
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetOAuth2TokenMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.GetOAuth2TokenCallback callback);

			// Token: 0x020000D0 RID: 208
			// (Invoke) Token: 0x060002FD RID: 765
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetTicketCallback(IntPtr ptr, Result result, [MarshalAs(UnmanagedType.LPStr)] ref string data);

			// Token: 0x020000D1 RID: 209
			// (Invoke) Token: 0x06000301 RID: 769
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetTicketMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.GetTicketCallback callback);
		}

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x0600016A RID: 362
		public delegate void ValidateOrExitHandler(Result result);

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x0600016E RID: 366
		public delegate void GetOAuth2TokenHandler(Result result, ref OAuth2Token oauth2Token);

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x06000172 RID: 370
		public delegate void GetTicketHandler(Result result, ref string data);
	}
}
