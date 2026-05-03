using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000033 RID: 51
	public class Discord : IDisposable
	{
		// Token: 0x06000030 RID: 48
		[DllImport("discord_game_sdk", ExactSpelling = true)]
		private static extern Result DiscordCreate(uint version, ref Discord.FFICreateParams createParams, out IntPtr manager);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private Discord.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(Discord.FFIMethods));
				}
				return (Discord.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C30 File Offset: 0x00000E30
		public Discord(long clientId, ulong flags)
		{
			Discord.FFICreateParams fficreateParams;
			fficreateParams.ClientId = clientId;
			fficreateParams.Flags = flags;
			this.Events = default(Discord.FFIEvents);
			this.EventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Discord.FFIEvents>(this.Events));
			fficreateParams.Events = this.EventsPtr;
			this.SelfHandle = GCHandle.Alloc(this);
			fficreateParams.EventData = GCHandle.ToIntPtr(this.SelfHandle);
			this.ApplicationEvents = default(ApplicationManager.FFIEvents);
			this.ApplicationEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ApplicationManager.FFIEvents>(this.ApplicationEvents));
			fficreateParams.ApplicationEvents = this.ApplicationEventsPtr;
			fficreateParams.ApplicationVersion = 1U;
			this.UserEvents = default(UserManager.FFIEvents);
			this.UserEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<UserManager.FFIEvents>(this.UserEvents));
			fficreateParams.UserEvents = this.UserEventsPtr;
			fficreateParams.UserVersion = 1U;
			this.ImageEvents = default(ImageManager.FFIEvents);
			this.ImageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ImageManager.FFIEvents>(this.ImageEvents));
			fficreateParams.ImageEvents = this.ImageEventsPtr;
			fficreateParams.ImageVersion = 1U;
			this.ActivityEvents = default(ActivityManager.FFIEvents);
			this.ActivityEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ActivityManager.FFIEvents>(this.ActivityEvents));
			fficreateParams.ActivityEvents = this.ActivityEventsPtr;
			fficreateParams.ActivityVersion = 1U;
			this.RelationshipEvents = default(RelationshipManager.FFIEvents);
			this.RelationshipEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<RelationshipManager.FFIEvents>(this.RelationshipEvents));
			fficreateParams.RelationshipEvents = this.RelationshipEventsPtr;
			fficreateParams.RelationshipVersion = 1U;
			this.LobbyEvents = default(LobbyManager.FFIEvents);
			this.LobbyEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LobbyManager.FFIEvents>(this.LobbyEvents));
			fficreateParams.LobbyEvents = this.LobbyEventsPtr;
			fficreateParams.LobbyVersion = 1U;
			this.NetworkEvents = default(NetworkManager.FFIEvents);
			this.NetworkEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<NetworkManager.FFIEvents>(this.NetworkEvents));
			fficreateParams.NetworkEvents = this.NetworkEventsPtr;
			fficreateParams.NetworkVersion = 1U;
			this.OverlayEvents = default(OverlayManager.FFIEvents);
			this.OverlayEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<OverlayManager.FFIEvents>(this.OverlayEvents));
			fficreateParams.OverlayEvents = this.OverlayEventsPtr;
			fficreateParams.OverlayVersion = 2U;
			this.StorageEvents = default(StorageManager.FFIEvents);
			this.StorageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<StorageManager.FFIEvents>(this.StorageEvents));
			fficreateParams.StorageEvents = this.StorageEventsPtr;
			fficreateParams.StorageVersion = 1U;
			this.StoreEvents = default(StoreManager.FFIEvents);
			this.StoreEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<StoreManager.FFIEvents>(this.StoreEvents));
			fficreateParams.StoreEvents = this.StoreEventsPtr;
			fficreateParams.StoreVersion = 1U;
			this.VoiceEvents = default(VoiceManager.FFIEvents);
			this.VoiceEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<VoiceManager.FFIEvents>(this.VoiceEvents));
			fficreateParams.VoiceEvents = this.VoiceEventsPtr;
			fficreateParams.VoiceVersion = 1U;
			this.AchievementEvents = default(AchievementManager.FFIEvents);
			this.AchievementEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf<AchievementManager.FFIEvents>(this.AchievementEvents));
			fficreateParams.AchievementEvents = this.AchievementEventsPtr;
			fficreateParams.AchievementVersion = 1U;
			this.InitEvents(this.EventsPtr, ref this.Events);
			Result result = Discord.DiscordCreate(3U, ref fficreateParams, out this.MethodsPtr);
			bool flag = result > Result.Ok;
			if (flag)
			{
				this.Dispose();
				throw new ResultException(result);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F6F File Offset: 0x0000116F
		private void InitEvents(IntPtr eventsPtr, ref Discord.FFIEvents events)
		{
			Marshal.StructureToPtr<Discord.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F80 File Offset: 0x00001180
		public void Dispose()
		{
			bool flag = this.MethodsPtr != IntPtr.Zero;
			if (flag)
			{
				this.Methods.Destroy(this.MethodsPtr);
			}
			this.SelfHandle.Free();
			Marshal.FreeHGlobal(this.EventsPtr);
			Marshal.FreeHGlobal(this.ApplicationEventsPtr);
			Marshal.FreeHGlobal(this.UserEventsPtr);
			Marshal.FreeHGlobal(this.ImageEventsPtr);
			Marshal.FreeHGlobal(this.ActivityEventsPtr);
			Marshal.FreeHGlobal(this.RelationshipEventsPtr);
			Marshal.FreeHGlobal(this.LobbyEventsPtr);
			Marshal.FreeHGlobal(this.NetworkEventsPtr);
			Marshal.FreeHGlobal(this.OverlayEventsPtr);
			Marshal.FreeHGlobal(this.StorageEventsPtr);
			Marshal.FreeHGlobal(this.StoreEventsPtr);
			Marshal.FreeHGlobal(this.VoiceEventsPtr);
			Marshal.FreeHGlobal(this.AchievementEventsPtr);
			bool flag2 = this.setLogHook != null;
			if (flag2)
			{
				this.setLogHook.Value.Free();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003088 File Offset: 0x00001288
		public void RunCallbacks()
		{
			Result result = this.Methods.RunCallbacks(this.MethodsPtr);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030C0 File Offset: 0x000012C0
		[MonoPInvokeCallback]
		private static void SetLogHookCallbackImpl(IntPtr ptr, LogLevel level, string message)
		{
			Discord.SetLogHookHandler setLogHookHandler = (Discord.SetLogHookHandler)GCHandle.FromIntPtr(ptr).Target;
			setLogHookHandler(level, message);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000030EC File Offset: 0x000012EC
		public void SetLogHook(LogLevel minLevel, Discord.SetLogHookHandler callback)
		{
			bool flag = this.setLogHook != null;
			if (flag)
			{
				this.setLogHook.Value.Free();
			}
			this.setLogHook = new GCHandle?(GCHandle.Alloc(callback));
			this.Methods.SetLogHook(this.MethodsPtr, minLevel, GCHandle.ToIntPtr(this.setLogHook.Value), new Discord.FFIMethods.SetLogHookCallback(Discord.SetLogHookCallbackImpl));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003164 File Offset: 0x00001364
		public ApplicationManager GetApplicationManager()
		{
			bool flag = this.ApplicationManagerInstance == null;
			if (flag)
			{
				this.ApplicationManagerInstance = new ApplicationManager(this.Methods.GetApplicationManager(this.MethodsPtr), this.ApplicationEventsPtr, ref this.ApplicationEvents);
			}
			return this.ApplicationManagerInstance;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000031B8 File Offset: 0x000013B8
		public UserManager GetUserManager()
		{
			bool flag = this.UserManagerInstance == null;
			if (flag)
			{
				this.UserManagerInstance = new UserManager(this.Methods.GetUserManager(this.MethodsPtr), this.UserEventsPtr, ref this.UserEvents);
			}
			return this.UserManagerInstance;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000320C File Offset: 0x0000140C
		public ImageManager GetImageManager()
		{
			bool flag = this.ImageManagerInstance == null;
			if (flag)
			{
				this.ImageManagerInstance = new ImageManager(this.Methods.GetImageManager(this.MethodsPtr), this.ImageEventsPtr, ref this.ImageEvents);
			}
			return this.ImageManagerInstance;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003260 File Offset: 0x00001460
		public ActivityManager GetActivityManager()
		{
			bool flag = this.ActivityManagerInstance == null;
			if (flag)
			{
				this.ActivityManagerInstance = new ActivityManager(this.Methods.GetActivityManager(this.MethodsPtr), this.ActivityEventsPtr, ref this.ActivityEvents);
			}
			return this.ActivityManagerInstance;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032B4 File Offset: 0x000014B4
		public RelationshipManager GetRelationshipManager()
		{
			bool flag = this.RelationshipManagerInstance == null;
			if (flag)
			{
				this.RelationshipManagerInstance = new RelationshipManager(this.Methods.GetRelationshipManager(this.MethodsPtr), this.RelationshipEventsPtr, ref this.RelationshipEvents);
			}
			return this.RelationshipManagerInstance;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003308 File Offset: 0x00001508
		public LobbyManager GetLobbyManager()
		{
			bool flag = this.LobbyManagerInstance == null;
			if (flag)
			{
				this.LobbyManagerInstance = new LobbyManager(this.Methods.GetLobbyManager(this.MethodsPtr), this.LobbyEventsPtr, ref this.LobbyEvents);
			}
			return this.LobbyManagerInstance;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000335C File Offset: 0x0000155C
		public NetworkManager GetNetworkManager()
		{
			bool flag = this.NetworkManagerInstance == null;
			if (flag)
			{
				this.NetworkManagerInstance = new NetworkManager(this.Methods.GetNetworkManager(this.MethodsPtr), this.NetworkEventsPtr, ref this.NetworkEvents);
			}
			return this.NetworkManagerInstance;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000033B0 File Offset: 0x000015B0
		public OverlayManager GetOverlayManager()
		{
			bool flag = this.OverlayManagerInstance == null;
			if (flag)
			{
				this.OverlayManagerInstance = new OverlayManager(this.Methods.GetOverlayManager(this.MethodsPtr), this.OverlayEventsPtr, ref this.OverlayEvents);
			}
			return this.OverlayManagerInstance;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003404 File Offset: 0x00001604
		public StorageManager GetStorageManager()
		{
			bool flag = this.StorageManagerInstance == null;
			if (flag)
			{
				this.StorageManagerInstance = new StorageManager(this.Methods.GetStorageManager(this.MethodsPtr), this.StorageEventsPtr, ref this.StorageEvents);
			}
			return this.StorageManagerInstance;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003458 File Offset: 0x00001658
		public StoreManager GetStoreManager()
		{
			bool flag = this.StoreManagerInstance == null;
			if (flag)
			{
				this.StoreManagerInstance = new StoreManager(this.Methods.GetStoreManager(this.MethodsPtr), this.StoreEventsPtr, ref this.StoreEvents);
			}
			return this.StoreManagerInstance;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000034AC File Offset: 0x000016AC
		public VoiceManager GetVoiceManager()
		{
			bool flag = this.VoiceManagerInstance == null;
			if (flag)
			{
				this.VoiceManagerInstance = new VoiceManager(this.Methods.GetVoiceManager(this.MethodsPtr), this.VoiceEventsPtr, ref this.VoiceEvents);
			}
			return this.VoiceManagerInstance;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003500 File Offset: 0x00001700
		public AchievementManager GetAchievementManager()
		{
			bool flag = this.AchievementManagerInstance == null;
			if (flag)
			{
				this.AchievementManagerInstance = new AchievementManager(this.Methods.GetAchievementManager(this.MethodsPtr), this.AchievementEventsPtr, ref this.AchievementEvents);
			}
			return this.AchievementManagerInstance;
		}

		// Token: 0x040000E6 RID: 230
		private GCHandle SelfHandle;

		// Token: 0x040000E7 RID: 231
		private IntPtr EventsPtr;

		// Token: 0x040000E8 RID: 232
		private Discord.FFIEvents Events;

		// Token: 0x040000E9 RID: 233
		private IntPtr ApplicationEventsPtr;

		// Token: 0x040000EA RID: 234
		private ApplicationManager.FFIEvents ApplicationEvents;

		// Token: 0x040000EB RID: 235
		internal ApplicationManager ApplicationManagerInstance;

		// Token: 0x040000EC RID: 236
		private IntPtr UserEventsPtr;

		// Token: 0x040000ED RID: 237
		private UserManager.FFIEvents UserEvents;

		// Token: 0x040000EE RID: 238
		internal UserManager UserManagerInstance;

		// Token: 0x040000EF RID: 239
		private IntPtr ImageEventsPtr;

		// Token: 0x040000F0 RID: 240
		private ImageManager.FFIEvents ImageEvents;

		// Token: 0x040000F1 RID: 241
		internal ImageManager ImageManagerInstance;

		// Token: 0x040000F2 RID: 242
		private IntPtr ActivityEventsPtr;

		// Token: 0x040000F3 RID: 243
		private ActivityManager.FFIEvents ActivityEvents;

		// Token: 0x040000F4 RID: 244
		internal ActivityManager ActivityManagerInstance;

		// Token: 0x040000F5 RID: 245
		private IntPtr RelationshipEventsPtr;

		// Token: 0x040000F6 RID: 246
		private RelationshipManager.FFIEvents RelationshipEvents;

		// Token: 0x040000F7 RID: 247
		internal RelationshipManager RelationshipManagerInstance;

		// Token: 0x040000F8 RID: 248
		private IntPtr LobbyEventsPtr;

		// Token: 0x040000F9 RID: 249
		private LobbyManager.FFIEvents LobbyEvents;

		// Token: 0x040000FA RID: 250
		internal LobbyManager LobbyManagerInstance;

		// Token: 0x040000FB RID: 251
		private IntPtr NetworkEventsPtr;

		// Token: 0x040000FC RID: 252
		private NetworkManager.FFIEvents NetworkEvents;

		// Token: 0x040000FD RID: 253
		internal NetworkManager NetworkManagerInstance;

		// Token: 0x040000FE RID: 254
		private IntPtr OverlayEventsPtr;

		// Token: 0x040000FF RID: 255
		private OverlayManager.FFIEvents OverlayEvents;

		// Token: 0x04000100 RID: 256
		internal OverlayManager OverlayManagerInstance;

		// Token: 0x04000101 RID: 257
		private IntPtr StorageEventsPtr;

		// Token: 0x04000102 RID: 258
		private StorageManager.FFIEvents StorageEvents;

		// Token: 0x04000103 RID: 259
		internal StorageManager StorageManagerInstance;

		// Token: 0x04000104 RID: 260
		private IntPtr StoreEventsPtr;

		// Token: 0x04000105 RID: 261
		private StoreManager.FFIEvents StoreEvents;

		// Token: 0x04000106 RID: 262
		internal StoreManager StoreManagerInstance;

		// Token: 0x04000107 RID: 263
		private IntPtr VoiceEventsPtr;

		// Token: 0x04000108 RID: 264
		private VoiceManager.FFIEvents VoiceEvents;

		// Token: 0x04000109 RID: 265
		internal VoiceManager VoiceManagerInstance;

		// Token: 0x0400010A RID: 266
		private IntPtr AchievementEventsPtr;

		// Token: 0x0400010B RID: 267
		private AchievementManager.FFIEvents AchievementEvents;

		// Token: 0x0400010C RID: 268
		internal AchievementManager AchievementManagerInstance;

		// Token: 0x0400010D RID: 269
		private IntPtr MethodsPtr;

		// Token: 0x0400010E RID: 270
		private object MethodsStructure;

		// Token: 0x0400010F RID: 271
		private GCHandle? setLogHook;

		// Token: 0x02000051 RID: 81
		internal struct FFIEvents
		{
		}

		// Token: 0x02000052 RID: 82
		internal struct FFIMethods
		{
			// Token: 0x0400015D RID: 349
			internal Discord.FFIMethods.DestroyHandler Destroy;

			// Token: 0x0400015E RID: 350
			internal Discord.FFIMethods.RunCallbacksMethod RunCallbacks;

			// Token: 0x0400015F RID: 351
			internal Discord.FFIMethods.SetLogHookMethod SetLogHook;

			// Token: 0x04000160 RID: 352
			internal Discord.FFIMethods.GetApplicationManagerMethod GetApplicationManager;

			// Token: 0x04000161 RID: 353
			internal Discord.FFIMethods.GetUserManagerMethod GetUserManager;

			// Token: 0x04000162 RID: 354
			internal Discord.FFIMethods.GetImageManagerMethod GetImageManager;

			// Token: 0x04000163 RID: 355
			internal Discord.FFIMethods.GetActivityManagerMethod GetActivityManager;

			// Token: 0x04000164 RID: 356
			internal Discord.FFIMethods.GetRelationshipManagerMethod GetRelationshipManager;

			// Token: 0x04000165 RID: 357
			internal Discord.FFIMethods.GetLobbyManagerMethod GetLobbyManager;

			// Token: 0x04000166 RID: 358
			internal Discord.FFIMethods.GetNetworkManagerMethod GetNetworkManager;

			// Token: 0x04000167 RID: 359
			internal Discord.FFIMethods.GetOverlayManagerMethod GetOverlayManager;

			// Token: 0x04000168 RID: 360
			internal Discord.FFIMethods.GetStorageManagerMethod GetStorageManager;

			// Token: 0x04000169 RID: 361
			internal Discord.FFIMethods.GetStoreManagerMethod GetStoreManager;

			// Token: 0x0400016A RID: 362
			internal Discord.FFIMethods.GetVoiceManagerMethod GetVoiceManager;

			// Token: 0x0400016B RID: 363
			internal Discord.FFIMethods.GetAchievementManagerMethod GetAchievementManager;

			// Token: 0x020000BA RID: 186
			// (Invoke) Token: 0x060002A5 RID: 677
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DestroyHandler(IntPtr MethodsPtr);

			// Token: 0x020000BB RID: 187
			// (Invoke) Token: 0x060002A9 RID: 681
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result RunCallbacksMethod(IntPtr methodsPtr);

			// Token: 0x020000BC RID: 188
			// (Invoke) Token: 0x060002AD RID: 685
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetLogHookCallback(IntPtr ptr, LogLevel level, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x020000BD RID: 189
			// (Invoke) Token: 0x060002B1 RID: 689
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SetLogHookMethod(IntPtr methodsPtr, LogLevel minLevel, IntPtr callbackData, Discord.FFIMethods.SetLogHookCallback callback);

			// Token: 0x020000BE RID: 190
			// (Invoke) Token: 0x060002B5 RID: 693
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetApplicationManagerMethod(IntPtr discordPtr);

			// Token: 0x020000BF RID: 191
			// (Invoke) Token: 0x060002B9 RID: 697
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetUserManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C0 RID: 192
			// (Invoke) Token: 0x060002BD RID: 701
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetImageManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C1 RID: 193
			// (Invoke) Token: 0x060002C1 RID: 705
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetActivityManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C2 RID: 194
			// (Invoke) Token: 0x060002C5 RID: 709
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetRelationshipManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C3 RID: 195
			// (Invoke) Token: 0x060002C9 RID: 713
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetLobbyManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C4 RID: 196
			// (Invoke) Token: 0x060002CD RID: 717
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetNetworkManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C5 RID: 197
			// (Invoke) Token: 0x060002D1 RID: 721
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetOverlayManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C6 RID: 198
			// (Invoke) Token: 0x060002D5 RID: 725
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetStorageManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C7 RID: 199
			// (Invoke) Token: 0x060002D9 RID: 729
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetStoreManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C8 RID: 200
			// (Invoke) Token: 0x060002DD RID: 733
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetVoiceManagerMethod(IntPtr discordPtr);

			// Token: 0x020000C9 RID: 201
			// (Invoke) Token: 0x060002E1 RID: 737
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate IntPtr GetAchievementManagerMethod(IntPtr discordPtr);
		}

		// Token: 0x02000053 RID: 83
		internal struct FFICreateParams
		{
			// Token: 0x0400016C RID: 364
			internal long ClientId;

			// Token: 0x0400016D RID: 365
			internal ulong Flags;

			// Token: 0x0400016E RID: 366
			internal IntPtr Events;

			// Token: 0x0400016F RID: 367
			internal IntPtr EventData;

			// Token: 0x04000170 RID: 368
			internal IntPtr ApplicationEvents;

			// Token: 0x04000171 RID: 369
			internal uint ApplicationVersion;

			// Token: 0x04000172 RID: 370
			internal IntPtr UserEvents;

			// Token: 0x04000173 RID: 371
			internal uint UserVersion;

			// Token: 0x04000174 RID: 372
			internal IntPtr ImageEvents;

			// Token: 0x04000175 RID: 373
			internal uint ImageVersion;

			// Token: 0x04000176 RID: 374
			internal IntPtr ActivityEvents;

			// Token: 0x04000177 RID: 375
			internal uint ActivityVersion;

			// Token: 0x04000178 RID: 376
			internal IntPtr RelationshipEvents;

			// Token: 0x04000179 RID: 377
			internal uint RelationshipVersion;

			// Token: 0x0400017A RID: 378
			internal IntPtr LobbyEvents;

			// Token: 0x0400017B RID: 379
			internal uint LobbyVersion;

			// Token: 0x0400017C RID: 380
			internal IntPtr NetworkEvents;

			// Token: 0x0400017D RID: 381
			internal uint NetworkVersion;

			// Token: 0x0400017E RID: 382
			internal IntPtr OverlayEvents;

			// Token: 0x0400017F RID: 383
			internal uint OverlayVersion;

			// Token: 0x04000180 RID: 384
			internal IntPtr StorageEvents;

			// Token: 0x04000181 RID: 385
			internal uint StorageVersion;

			// Token: 0x04000182 RID: 386
			internal IntPtr StoreEvents;

			// Token: 0x04000183 RID: 387
			internal uint StoreVersion;

			// Token: 0x04000184 RID: 388
			internal IntPtr VoiceEvents;

			// Token: 0x04000185 RID: 389
			internal uint VoiceVersion;

			// Token: 0x04000186 RID: 390
			internal IntPtr AchievementEvents;

			// Token: 0x04000187 RID: 391
			internal uint AchievementVersion;
		}

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000166 RID: 358
		public delegate void SetLogHookHandler(LogLevel level, string message);
	}
}
