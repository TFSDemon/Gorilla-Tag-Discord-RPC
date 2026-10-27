using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200003E RID: 62
	public class VoiceManager
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000677C File Offset: 0x0000497C
		private VoiceManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(VoiceManager.FFIMethods));
				}
				return (VoiceManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000116 RID: 278 RVA: 0x000067C4 File Offset: 0x000049C4
		// (remove) Token: 0x06000117 RID: 279 RVA: 0x000067FC File Offset: 0x000049FC

		public event VoiceManager.SettingsUpdateHandler OnSettingsUpdate;

		// Token: 0x06000118 RID: 280 RVA: 0x00006834 File Offset: 0x00004A34
		internal VoiceManager(IntPtr ptr, IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
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

		// Token: 0x06000119 RID: 281 RVA: 0x0000688C File Offset: 0x00004A8C
		private void InitEvents(IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
		{
			events.OnSettingsUpdate = new VoiceManager.FFIEvents.SettingsUpdateHandler(VoiceManager.OnSettingsUpdateImpl);
			Marshal.StructureToPtr<VoiceManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000068B0 File Offset: 0x00004AB0
		public InputMode GetInputMode()
		{
			InputMode result = default(InputMode);
			Result result2 = this.Methods.GetInputMode(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000068F4 File Offset: 0x00004AF4
		[MonoPInvokeCallback]
		private static void SetInputModeCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			VoiceManager.SetInputModeHandler setInputModeHandler = (VoiceManager.SetInputModeHandler)gchandle.Target;
			gchandle.Free();
			setInputModeHandler(result);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006928 File Offset: 0x00004B28
		public void SetInputMode(InputMode inputMode, VoiceManager.SetInputModeHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SetInputMode(this.MethodsPtr, inputMode, GCHandle.ToIntPtr(value), new VoiceManager.FFIMethods.SetInputModeCallback(VoiceManager.SetInputModeCallbackImpl));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006968 File Offset: 0x00004B68
		public bool IsSelfMute()
		{
			bool result = false;
			Result result2 = this.Methods.IsSelfMute(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000069A8 File Offset: 0x00004BA8
		public void SetSelfMute(bool mute)
		{
			Result result = this.Methods.SetSelfMute(this.MethodsPtr, mute);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000069E0 File Offset: 0x00004BE0
		public bool IsSelfDeaf()
		{
			bool result = false;
			Result result2 = this.Methods.IsSelfDeaf(this.MethodsPtr, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006A20 File Offset: 0x00004C20
		public void SetSelfDeaf(bool deaf)
		{
			Result result = this.Methods.SetSelfDeaf(this.MethodsPtr, deaf);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006A58 File Offset: 0x00004C58
		public bool IsLocalMute(long userId)
		{
			bool result = false;
			Result result2 = this.Methods.IsLocalMute(this.MethodsPtr, userId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006A98 File Offset: 0x00004C98
		public void SetLocalMute(long userId, bool mute)
		{
			Result result = this.Methods.SetLocalMute(this.MethodsPtr, userId, mute);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public byte GetLocalVolume(long userId)
		{
			byte result = 0;
			Result result2 = this.Methods.GetLocalVolume(this.MethodsPtr, userId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006B10 File Offset: 0x00004D10
		public void SetLocalVolume(long userId, byte volume)
		{
			Result result = this.Methods.SetLocalVolume(this.MethodsPtr, userId, volume);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006B48 File Offset: 0x00004D48
		[MonoPInvokeCallback]
		private static void OnSettingsUpdateImpl(IntPtr ptr)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.VoiceManagerInstance.OnSettingsUpdate != null;
			if (flag)
			{
				discord.VoiceManagerInstance.OnSettingsUpdate();
			}
		}

		// Token: 0x04000132 RID: 306
		private IntPtr MethodsPtr;

		// Token: 0x04000133 RID: 307
		private object MethodsStructure;

		// Token: 0x02000094 RID: 148
		internal struct FFIEvents
		{
			// Token: 0x040001F9 RID: 505
			internal VoiceManager.FFIEvents.SettingsUpdateHandler OnSettingsUpdate;

			// Token: 0x02000158 RID: 344
			// (Invoke) Token: 0x0600051D RID: 1309
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SettingsUpdateHandler(IntPtr ptr);
		}

		// Token: 0x02000095 RID: 149
		internal struct FFIMethods
		{
			// Token: 0x040001FA RID: 506
			internal VoiceManager.FFIMethods.GetInputModeMethod GetInputMode;

			// Token: 0x040001FB RID: 507
			internal VoiceManager.FFIMethods.SetInputModeMethod SetInputMode;

			// Token: 0x040001FC RID: 508
			internal VoiceManager.FFIMethods.IsSelfMuteMethod IsSelfMute;

			// Token: 0x040001FD RID: 509
			internal VoiceManager.FFIMethods.SetSelfMuteMethod SetSelfMute;

			// Token: 0x040001FE RID: 510
			internal VoiceManager.FFIMethods.IsSelfDeafMethod IsSelfDeaf;

			// Token: 0x040001FF RID: 511
			internal VoiceManager.FFIMethods.SetSelfDeafMethod SetSelfDeaf;

			// Token: 0x04000200 RID: 512
			internal VoiceManager.FFIMethods.IsLocalMuteMethod IsLocalMute;

			// Token: 0x04000201 RID: 513
			internal VoiceManager.FFIMethods.SetLocalMuteMethod SetLocalMute;

			// Token: 0x04000202 RID: 514
			internal VoiceManager.FFIMethods.GetLocalVolumeMethod GetLocalVolume;

			// Token: 0x04000203 RID: 515
			internal VoiceManager.FFIMethods.SetLocalVolumeMethod SetLocalVolume;

			// Token: 0x02000159 RID: 345
			// (Invoke) Token: 0x06000521 RID: 1313
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetInputModeMethod(IntPtr methodsPtr, ref InputMode inputMode);

			// Token: 0x0200015A RID: 346
			// (Invoke) Token: 0x06000525 RID: 1317
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetInputModeCallback(IntPtr ptr, Result result);

			// Token: 0x0200015B RID: 347
			// (Invoke) Token: 0x06000529 RID: 1321
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetInputModeMethod(IntPtr methodsPtr, InputMode inputMode, IntPtr callbackData, VoiceManager.FFIMethods.SetInputModeCallback callback);

			// Token: 0x0200015C RID: 348
			// (Invoke) Token: 0x0600052D RID: 1325
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result IsSelfMuteMethod(IntPtr methodsPtr, ref bool mute);

			// Token: 0x0200015D RID: 349
			// (Invoke) Token: 0x06000531 RID: 1329
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetSelfMuteMethod(IntPtr methodsPtr, bool mute);

			// Token: 0x0200015E RID: 350
			// (Invoke) Token: 0x06000535 RID: 1333
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result IsSelfDeafMethod(IntPtr methodsPtr, ref bool deaf);

			// Token: 0x0200015F RID: 351
			// (Invoke) Token: 0x06000539 RID: 1337
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetSelfDeafMethod(IntPtr methodsPtr, bool deaf);

			// Token: 0x02000160 RID: 352
			// (Invoke) Token: 0x0600053D RID: 1341
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result IsLocalMuteMethod(IntPtr methodsPtr, long userId, ref bool mute);

			// Token: 0x02000161 RID: 353
			// (Invoke) Token: 0x06000541 RID: 1345
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetLocalMuteMethod(IntPtr methodsPtr, long userId, bool mute);

			// Token: 0x02000162 RID: 354
			// (Invoke) Token: 0x06000545 RID: 1349
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLocalVolumeMethod(IntPtr methodsPtr, long userId, ref byte volume);

			// Token: 0x02000163 RID: 355
			// (Invoke) Token: 0x06000549 RID: 1353
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SetLocalVolumeMethod(IntPtr methodsPtr, long userId, byte volume);
		}

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x0600021E RID: 542
		public delegate void SetInputModeHandler(Result result);

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x06000222 RID: 546
		public delegate void SettingsUpdateHandler();
	}
}
