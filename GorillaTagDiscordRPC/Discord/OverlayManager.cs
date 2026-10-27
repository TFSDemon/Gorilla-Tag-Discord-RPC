using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200003B RID: 59
	public class OverlayManager
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005718 File Offset: 0x00003918
		private OverlayManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(OverlayManager.FFIMethods));
				}
				return (OverlayManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000CD RID: 205 RVA: 0x00005760 File Offset: 0x00003960
		// (remove) Token: 0x060000CE RID: 206 RVA: 0x00005798 File Offset: 0x00003998

		public event OverlayManager.ToggleHandler OnToggle;

		// Token: 0x060000CF RID: 207 RVA: 0x000057D0 File Offset: 0x000039D0
		internal OverlayManager(IntPtr ptr, IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00005828 File Offset: 0x00003A28
		private void InitEvents(IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
		{
			events.OnToggle = new OverlayManager.FFIEvents.ToggleHandler(OverlayManager.OnToggleImpl);
			Marshal.StructureToPtr<OverlayManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000584C File Offset: 0x00003A4C
		public bool IsEnabled()
		{
			bool result = false;
			this.Methods.IsEnabled(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000587C File Offset: 0x00003A7C
		public bool IsLocked()
		{
			bool result = false;
			this.Methods.IsLocked(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000058AC File Offset: 0x00003AAC
		[MonoPInvokeCallback]
		private static void SetLockedCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.SetLockedHandler setLockedHandler = (OverlayManager.SetLockedHandler)gchandle.Target;
			gchandle.Free();
			setLockedHandler(result);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000058E0 File Offset: 0x00003AE0
		public void SetLocked(bool locked, OverlayManager.SetLockedHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SetLocked(this.MethodsPtr, locked, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.SetLockedCallback(OverlayManager.SetLockedCallbackImpl));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005920 File Offset: 0x00003B20
		[MonoPInvokeCallback]
		private static void OpenActivityInviteCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.OpenActivityInviteHandler openActivityInviteHandler = (OverlayManager.OpenActivityInviteHandler)gchandle.Target;
			gchandle.Free();
			openActivityInviteHandler(result);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005954 File Offset: 0x00003B54
		public void OpenActivityInvite(ActivityActionType type, OverlayManager.OpenActivityInviteHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.OpenActivityInvite(this.MethodsPtr, type, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.OpenActivityInviteCallback(OverlayManager.OpenActivityInviteCallbackImpl));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005994 File Offset: 0x00003B94
		[MonoPInvokeCallback]
		private static void OpenGuildInviteCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.OpenGuildInviteHandler openGuildInviteHandler = (OverlayManager.OpenGuildInviteHandler)gchandle.Target;
			gchandle.Free();
			openGuildInviteHandler(result);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000059C8 File Offset: 0x00003BC8
		public void OpenGuildInvite(string code, OverlayManager.OpenGuildInviteHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.OpenGuildInvite(this.MethodsPtr, code, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.OpenGuildInviteCallback(OverlayManager.OpenGuildInviteCallbackImpl));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005A08 File Offset: 0x00003C08
		[MonoPInvokeCallback]
		private static void OpenVoiceSettingsCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.OpenVoiceSettingsHandler openVoiceSettingsHandler = (OverlayManager.OpenVoiceSettingsHandler)gchandle.Target;
			gchandle.Free();
			openVoiceSettingsHandler(result);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005A3C File Offset: 0x00003C3C
		public void OpenVoiceSettings(OverlayManager.OpenVoiceSettingsHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.OpenVoiceSettings(this.MethodsPtr, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.OpenVoiceSettingsCallback(OverlayManager.OpenVoiceSettingsCallbackImpl));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005A7C File Offset: 0x00003C7C
		public void InitDrawingDxgi(IntPtr swapchain, bool useMessageForwarding)
		{
			Result result = this.Methods.InitDrawingDxgi(this.MethodsPtr, swapchain, useMessageForwarding);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005AB3 File Offset: 0x00003CB3
		public void OnPresent()
		{
			this.Methods.OnPresent(this.MethodsPtr);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005ACD File Offset: 0x00003CCD
		public void ForwardMessage(IntPtr message)
		{
			this.Methods.ForwardMessage(this.MethodsPtr, message);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public void KeyEvent(bool down, string keyCode, KeyVariant variant)
		{
			this.Methods.KeyEvent(this.MethodsPtr, down, keyCode, variant);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005B05 File Offset: 0x00003D05
		public void CharEvent(string character)
		{
			this.Methods.CharEvent(this.MethodsPtr, character);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005B20 File Offset: 0x00003D20
		public void MouseButtonEvent(byte down, int clickCount, MouseButton which, int x, int y)
		{
			this.Methods.MouseButtonEvent(this.MethodsPtr, down, clickCount, which, x, y);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005B41 File Offset: 0x00003D41
		public void MouseMotionEvent(int x, int y)
		{
			this.Methods.MouseMotionEvent(this.MethodsPtr, x, y);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005B5D File Offset: 0x00003D5D
		public void ImeCommitText(string text)
		{
			this.Methods.ImeCommitText(this.MethodsPtr, text);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005B78 File Offset: 0x00003D78
		public void ImeSetComposition(string text, ImeUnderline underlines, int from, int to)
		{
			this.Methods.ImeSetComposition(this.MethodsPtr, text, ref underlines, from, to);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005B98 File Offset: 0x00003D98
		public void ImeCancelComposition()
		{
			this.Methods.ImeCancelComposition(this.MethodsPtr);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005BB4 File Offset: 0x00003DB4
		[MonoPInvokeCallback]
		private static void SetImeCompositionRangeCallbackCallbackImpl(IntPtr ptr, int from, int to, ref Rect bounds)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.SetImeCompositionRangeCallbackHandler setImeCompositionRangeCallbackHandler = (OverlayManager.SetImeCompositionRangeCallbackHandler)gchandle.Target;
			gchandle.Free();
			setImeCompositionRangeCallbackHandler(from, to, ref bounds);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public void SetImeCompositionRangeCallback(OverlayManager.SetImeCompositionRangeCallbackHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SetImeCompositionRangeCallback(this.MethodsPtr, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.SetImeCompositionRangeCallbackCallback(OverlayManager.SetImeCompositionRangeCallbackCallbackImpl));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005C28 File Offset: 0x00003E28
		[MonoPInvokeCallback]
		private static void SetImeSelectionBoundsCallbackCallbackImpl(IntPtr ptr, Rect anchor, Rect focus, bool isAnchorFirst)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			OverlayManager.SetImeSelectionBoundsCallbackHandler setImeSelectionBoundsCallbackHandler = (OverlayManager.SetImeSelectionBoundsCallbackHandler)gchandle.Target;
			gchandle.Free();
			setImeSelectionBoundsCallbackHandler(anchor, focus, isAnchorFirst);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005C5C File Offset: 0x00003E5C
		public void SetImeSelectionBoundsCallback(OverlayManager.SetImeSelectionBoundsCallbackHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SetImeSelectionBoundsCallback(this.MethodsPtr, GCHandle.ToIntPtr(value), new OverlayManager.FFIMethods.SetImeSelectionBoundsCallbackCallback(OverlayManager.SetImeSelectionBoundsCallbackCallbackImpl));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005C9C File Offset: 0x00003E9C
		public bool IsPointInsideClickZone(int x, int y)
		{
			return this.Methods.IsPointInsideClickZone(this.MethodsPtr, x, y);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005CC8 File Offset: 0x00003EC8
		[MonoPInvokeCallback]
		private static void OnToggleImpl(IntPtr ptr, bool locked)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.OverlayManagerInstance.OnToggle != null;
			if (flag)
			{
				discord.OverlayManagerInstance.OnToggle(locked);
			}
		}

		// Token: 0x04000129 RID: 297
		private IntPtr MethodsPtr;

		// Token: 0x0400012A RID: 298
		private object MethodsStructure;

		// Token: 0x0200007F RID: 127
		internal struct FFIEvents
		{
			// Token: 0x040001CE RID: 462
			internal OverlayManager.FFIEvents.ToggleHandler OnToggle;

			// Token: 0x02000121 RID: 289
			// (Invoke) Token: 0x06000441 RID: 1089
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ToggleHandler(IntPtr ptr, bool locked);
		}

		// Token: 0x02000080 RID: 128
		internal struct FFIMethods
		{
			// Token: 0x040001CF RID: 463
			internal OverlayManager.FFIMethods.IsEnabledMethod IsEnabled;

			// Token: 0x040001D0 RID: 464
			internal OverlayManager.FFIMethods.IsLockedMethod IsLocked;

			// Token: 0x040001D1 RID: 465
			internal OverlayManager.FFIMethods.SetLockedMethod SetLocked;

			// Token: 0x040001D2 RID: 466
			internal OverlayManager.FFIMethods.OpenActivityInviteMethod OpenActivityInvite;

			// Token: 0x040001D3 RID: 467
			internal OverlayManager.FFIMethods.OpenGuildInviteMethod OpenGuildInvite;

			// Token: 0x040001D4 RID: 468
			internal OverlayManager.FFIMethods.OpenVoiceSettingsMethod OpenVoiceSettings;

			// Token: 0x040001D5 RID: 469
			internal OverlayManager.FFIMethods.InitDrawingDxgiMethod InitDrawingDxgi;

			// Token: 0x040001D6 RID: 470
			internal OverlayManager.FFIMethods.OnPresentMethod OnPresent;

			// Token: 0x040001D7 RID: 471
			internal OverlayManager.FFIMethods.ForwardMessageMethod ForwardMessage;

			// Token: 0x040001D8 RID: 472
			internal OverlayManager.FFIMethods.KeyEventMethod KeyEvent;

			// Token: 0x040001D9 RID: 473
			internal OverlayManager.FFIMethods.CharEventMethod CharEvent;

			// Token: 0x040001DA RID: 474
			internal OverlayManager.FFIMethods.MouseButtonEventMethod MouseButtonEvent;

			// Token: 0x040001DB RID: 475
			internal OverlayManager.FFIMethods.MouseMotionEventMethod MouseMotionEvent;

			// Token: 0x040001DC RID: 476
			internal OverlayManager.FFIMethods.ImeCommitTextMethod ImeCommitText;

			// Token: 0x040001DD RID: 477
			internal OverlayManager.FFIMethods.ImeSetCompositionMethod ImeSetComposition;

			// Token: 0x040001DE RID: 478
			internal OverlayManager.FFIMethods.ImeCancelCompositionMethod ImeCancelComposition;

			// Token: 0x040001DF RID: 479
			internal OverlayManager.FFIMethods.SetImeCompositionRangeCallbackMethod SetImeCompositionRangeCallback;

			// Token: 0x040001E0 RID: 480
			internal OverlayManager.FFIMethods.SetImeSelectionBoundsCallbackMethod SetImeSelectionBoundsCallback;

			// Token: 0x040001E1 RID: 481
			internal OverlayManager.FFIMethods.IsPointInsideClickZoneMethod IsPointInsideClickZone;

			// Token: 0x02000122 RID: 290
			// (Invoke) Token: 0x06000445 RID: 1093
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void IsEnabledMethod(IntPtr methodsPtr, ref bool enabled);

			// Token: 0x02000123 RID: 291
			// (Invoke) Token: 0x06000449 RID: 1097
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void IsLockedMethod(IntPtr methodsPtr, ref bool locked);

			// Token: 0x02000124 RID: 292
			// (Invoke) Token: 0x0600044D RID: 1101
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetLockedCallback(IntPtr ptr, Result result);

			// Token: 0x02000125 RID: 293
			// (Invoke) Token: 0x06000451 RID: 1105
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetLockedMethod(IntPtr methodsPtr, bool locked, IntPtr callbackData, OverlayManager.FFIMethods.SetLockedCallback callback);

			// Token: 0x02000126 RID: 294
			// (Invoke) Token: 0x06000455 RID: 1109
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenActivityInviteCallback(IntPtr ptr, Result result);

			// Token: 0x02000127 RID: 295
			// (Invoke) Token: 0x06000459 RID: 1113
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenActivityInviteMethod(IntPtr methodsPtr, ActivityActionType type, IntPtr callbackData, OverlayManager.FFIMethods.OpenActivityInviteCallback callback);

			// Token: 0x02000128 RID: 296
			// (Invoke) Token: 0x0600045D RID: 1117
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenGuildInviteCallback(IntPtr ptr, Result result);

			// Token: 0x02000129 RID: 297
			// (Invoke) Token: 0x06000461 RID: 1121
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenGuildInviteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string code, IntPtr callbackData, OverlayManager.FFIMethods.OpenGuildInviteCallback callback);

			// Token: 0x0200012A RID: 298
			// (Invoke) Token: 0x06000465 RID: 1125
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenVoiceSettingsCallback(IntPtr ptr, Result result);

			// Token: 0x0200012B RID: 299
			// (Invoke) Token: 0x06000469 RID: 1129
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OpenVoiceSettingsMethod(IntPtr methodsPtr, IntPtr callbackData, OverlayManager.FFIMethods.OpenVoiceSettingsCallback callback);

			// Token: 0x0200012C RID: 300
			// (Invoke) Token: 0x0600046D RID: 1133
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result InitDrawingDxgiMethod(IntPtr methodsPtr, IntPtr swapchain, bool useMessageForwarding);

			// Token: 0x0200012D RID: 301
			// (Invoke) Token: 0x06000471 RID: 1137
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void OnPresentMethod(IntPtr methodsPtr);

			// Token: 0x0200012E RID: 302
			// (Invoke) Token: 0x06000475 RID: 1141
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ForwardMessageMethod(IntPtr methodsPtr, IntPtr message);

			// Token: 0x0200012F RID: 303
			// (Invoke) Token: 0x06000479 RID: 1145
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void KeyEventMethod(IntPtr methodsPtr, bool down, [MarshalAs(UnmanagedType.LPStr)] string keyCode, KeyVariant variant);

			// Token: 0x02000130 RID: 304
			// (Invoke) Token: 0x0600047D RID: 1149
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CharEventMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string character);

			// Token: 0x02000131 RID: 305
			// (Invoke) Token: 0x06000481 RID: 1153
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MouseButtonEventMethod(IntPtr methodsPtr, byte down, int clickCount, MouseButton which, int x, int y);

			// Token: 0x02000132 RID: 306
			// (Invoke) Token: 0x06000485 RID: 1157
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MouseMotionEventMethod(IntPtr methodsPtr, int x, int y);

			// Token: 0x02000133 RID: 307
			// (Invoke) Token: 0x06000489 RID: 1161
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ImeCommitTextMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text);

			// Token: 0x02000134 RID: 308
			// (Invoke) Token: 0x0600048D RID: 1165
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ImeSetCompositionMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text, ref ImeUnderline underlines, int from, int to);

			// Token: 0x02000135 RID: 309
			// (Invoke) Token: 0x06000491 RID: 1169
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ImeCancelCompositionMethod(IntPtr methodsPtr);

			// Token: 0x02000136 RID: 310
			// (Invoke) Token: 0x06000495 RID: 1173
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetImeCompositionRangeCallbackCallback(IntPtr ptr, int from, int to, ref Rect bounds);

			// Token: 0x02000137 RID: 311
			// (Invoke) Token: 0x06000499 RID: 1177
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetImeCompositionRangeCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, OverlayManager.FFIMethods.SetImeCompositionRangeCallbackCallback callback);

			// Token: 0x02000138 RID: 312
			// (Invoke) Token: 0x0600049D RID: 1181
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetImeSelectionBoundsCallbackCallback(IntPtr ptr, Rect anchor, Rect focus, bool isAnchorFirst);

			// Token: 0x02000139 RID: 313
			// (Invoke) Token: 0x060004A1 RID: 1185
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetImeSelectionBoundsCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, OverlayManager.FFIMethods.SetImeSelectionBoundsCallbackCallback callback);

			// Token: 0x0200013A RID: 314
			// (Invoke) Token: 0x060004A5 RID: 1189
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate bool IsPointInsideClickZoneMethod(IntPtr methodsPtr, int x, int y);
		}

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x060001E2 RID: 482
		public delegate void SetLockedHandler(Result result);

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x060001E6 RID: 486
		public delegate void OpenActivityInviteHandler(Result result);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x060001EA RID: 490
		public delegate void OpenGuildInviteHandler(Result result);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x060001EE RID: 494
		public delegate void OpenVoiceSettingsHandler(Result result);

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x060001F2 RID: 498
		public delegate void SetImeCompositionRangeCallbackHandler(int from, int to, ref Rect bounds);

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x060001F6 RID: 502
		public delegate void SetImeSelectionBoundsCallbackHandler(Rect anchor, Rect focus, bool isAnchorFirst);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x060001FA RID: 506
		public delegate void ToggleHandler(bool locked);
	}
}
