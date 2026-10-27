using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x02000039 RID: 57
	public class LobbyManager
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004020 File Offset: 0x00002220
		private LobbyManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyManager.FFIMethods));
				}
				return (LobbyManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000073 RID: 115 RVA: 0x00004068 File Offset: 0x00002268
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x000040A0 File Offset: 0x000022A0
		public event LobbyManager.LobbyUpdateHandler OnLobbyUpdate;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000075 RID: 117 RVA: 0x000040D8 File Offset: 0x000022D8
		// (remove) Token: 0x06000076 RID: 118 RVA: 0x00004110 File Offset: 0x00002310
		public event LobbyManager.LobbyDeleteHandler OnLobbyDelete;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000077 RID: 119 RVA: 0x00004148 File Offset: 0x00002348
		// (remove) Token: 0x06000078 RID: 120 RVA: 0x00004180 File Offset: 0x00002380
		public event LobbyManager.MemberConnectHandler OnMemberConnect;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000079 RID: 121 RVA: 0x000041B8 File Offset: 0x000023B8
		// (remove) Token: 0x0600007A RID: 122 RVA: 0x000041F0 File Offset: 0x000023F0
		public event LobbyManager.MemberUpdateHandler OnMemberUpdate;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600007B RID: 123 RVA: 0x00004228 File Offset: 0x00002428
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x00004260 File Offset: 0x00002460
		public event LobbyManager.MemberDisconnectHandler OnMemberDisconnect;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600007D RID: 125 RVA: 0x00004298 File Offset: 0x00002498
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x000042D0 File Offset: 0x000024D0
		public event LobbyManager.LobbyMessageHandler OnLobbyMessage;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600007F RID: 127 RVA: 0x00004308 File Offset: 0x00002508
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x00004340 File Offset: 0x00002540
		public event LobbyManager.SpeakingHandler OnSpeaking;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000081 RID: 129 RVA: 0x00004378 File Offset: 0x00002578
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x000043B0 File Offset: 0x000025B0
		public event LobbyManager.NetworkMessageHandler OnNetworkMessage;

		// Token: 0x06000083 RID: 131 RVA: 0x000043E8 File Offset: 0x000025E8
		internal LobbyManager(IntPtr ptr, IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
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

		// Token: 0x06000084 RID: 132 RVA: 0x00004440 File Offset: 0x00002640
		private void InitEvents(IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
		{
			events.OnLobbyUpdate = new LobbyManager.FFIEvents.LobbyUpdateHandler(LobbyManager.OnLobbyUpdateImpl);
			events.OnLobbyDelete = new LobbyManager.FFIEvents.LobbyDeleteHandler(LobbyManager.OnLobbyDeleteImpl);
			events.OnMemberConnect = new LobbyManager.FFIEvents.MemberConnectHandler(LobbyManager.OnMemberConnectImpl);
			events.OnMemberUpdate = new LobbyManager.FFIEvents.MemberUpdateHandler(LobbyManager.OnMemberUpdateImpl);
			events.OnMemberDisconnect = new LobbyManager.FFIEvents.MemberDisconnectHandler(LobbyManager.OnMemberDisconnectImpl);
			events.OnLobbyMessage = new LobbyManager.FFIEvents.LobbyMessageHandler(LobbyManager.OnLobbyMessageImpl);
			events.OnSpeaking = new LobbyManager.FFIEvents.SpeakingHandler(LobbyManager.OnSpeakingImpl);
			events.OnNetworkMessage = new LobbyManager.FFIEvents.NetworkMessageHandler(LobbyManager.OnNetworkMessageImpl);
			Marshal.StructureToPtr<LobbyManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000044EC File Offset: 0x000026EC
		public LobbyTransaction GetLobbyCreateTransaction()
		{
			LobbyTransaction result = default(LobbyTransaction);
			Result result2 = this.Methods.GetLobbyCreateTransaction(this.MethodsPtr, ref result.MethodsPtr);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004538 File Offset: 0x00002738
		public LobbyTransaction GetLobbyUpdateTransaction(long lobbyId)
		{
			LobbyTransaction result = default(LobbyTransaction);
			Result result2 = this.Methods.GetLobbyUpdateTransaction(this.MethodsPtr, lobbyId, ref result.MethodsPtr);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004584 File Offset: 0x00002784
		public LobbyMemberTransaction GetMemberUpdateTransaction(long lobbyId, long userId)
		{
			LobbyMemberTransaction result = default(LobbyMemberTransaction);
			Result result2 = this.Methods.GetMemberUpdateTransaction(this.MethodsPtr, lobbyId, userId, ref result.MethodsPtr);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000045D0 File Offset: 0x000027D0
		[MonoPInvokeCallback]
		private static void CreateLobbyCallbackImpl(IntPtr ptr, Result result, ref Lobby lobby)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.CreateLobbyHandler createLobbyHandler = (LobbyManager.CreateLobbyHandler)gchandle.Target;
			gchandle.Free();
			createLobbyHandler(result, ref lobby);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004604 File Offset: 0x00002804
		public void CreateLobby(LobbyTransaction transaction, LobbyManager.CreateLobbyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.CreateLobby(this.MethodsPtr, transaction.MethodsPtr, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.CreateLobbyCallback(LobbyManager.CreateLobbyCallbackImpl));
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004654 File Offset: 0x00002854
		[MonoPInvokeCallback]
		private static void UpdateLobbyCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.UpdateLobbyHandler updateLobbyHandler = (LobbyManager.UpdateLobbyHandler)gchandle.Target;
			gchandle.Free();
			updateLobbyHandler(result);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004688 File Offset: 0x00002888
		public void UpdateLobby(long lobbyId, LobbyTransaction transaction, LobbyManager.UpdateLobbyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.UpdateLobby(this.MethodsPtr, lobbyId, transaction.MethodsPtr, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.UpdateLobbyCallback(LobbyManager.UpdateLobbyCallbackImpl));
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000046DC File Offset: 0x000028DC
		[MonoPInvokeCallback]
		private static void DeleteLobbyCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.DeleteLobbyHandler deleteLobbyHandler = (LobbyManager.DeleteLobbyHandler)gchandle.Target;
			gchandle.Free();
			deleteLobbyHandler(result);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004710 File Offset: 0x00002910
		public void DeleteLobby(long lobbyId, LobbyManager.DeleteLobbyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.DeleteLobby(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.DeleteLobbyCallback(LobbyManager.DeleteLobbyCallbackImpl));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004750 File Offset: 0x00002950
		[MonoPInvokeCallback]
		private static void ConnectLobbyCallbackImpl(IntPtr ptr, Result result, ref Lobby lobby)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.ConnectLobbyHandler connectLobbyHandler = (LobbyManager.ConnectLobbyHandler)gchandle.Target;
			gchandle.Free();
			connectLobbyHandler(result, ref lobby);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004784 File Offset: 0x00002984
		public void ConnectLobby(long lobbyId, string secret, LobbyManager.ConnectLobbyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ConnectLobby(this.MethodsPtr, lobbyId, secret, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.ConnectLobbyCallback(LobbyManager.ConnectLobbyCallbackImpl));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000047C4 File Offset: 0x000029C4
		[MonoPInvokeCallback]
		private static void ConnectLobbyWithActivitySecretCallbackImpl(IntPtr ptr, Result result, ref Lobby lobby)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.ConnectLobbyWithActivitySecretHandler connectLobbyWithActivitySecretHandler = (LobbyManager.ConnectLobbyWithActivitySecretHandler)gchandle.Target;
			gchandle.Free();
			connectLobbyWithActivitySecretHandler(result, ref lobby);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000047F8 File Offset: 0x000029F8
		public void ConnectLobbyWithActivitySecret(string activitySecret, LobbyManager.ConnectLobbyWithActivitySecretHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ConnectLobbyWithActivitySecret(this.MethodsPtr, activitySecret, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback(LobbyManager.ConnectLobbyWithActivitySecretCallbackImpl));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004838 File Offset: 0x00002A38
		[MonoPInvokeCallback]
		private static void DisconnectLobbyCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.DisconnectLobbyHandler disconnectLobbyHandler = (LobbyManager.DisconnectLobbyHandler)gchandle.Target;
			gchandle.Free();
			disconnectLobbyHandler(result);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000486C File Offset: 0x00002A6C
		public void DisconnectLobby(long lobbyId, LobbyManager.DisconnectLobbyHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.DisconnectLobby(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.DisconnectLobbyCallback(LobbyManager.DisconnectLobbyCallbackImpl));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000048AC File Offset: 0x00002AAC
		public Lobby GetLobby(long lobbyId)
		{
			Lobby result = default(Lobby);
			Result result2 = this.Methods.GetLobby(this.MethodsPtr, lobbyId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000048F4 File Offset: 0x00002AF4
		public string GetLobbyActivitySecret(long lobbyId)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			Result result = this.Methods.GetLobbyActivitySecret(this.MethodsPtr, lobbyId, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004940 File Offset: 0x00002B40
		public string GetLobbyMetadataValue(long lobbyId, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetLobbyMetadataValue(this.MethodsPtr, lobbyId, key, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004990 File Offset: 0x00002B90
		public string GetLobbyMetadataKey(long lobbyId, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			Result result = this.Methods.GetLobbyMetadataKey(this.MethodsPtr, lobbyId, index, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000049E0 File Offset: 0x00002BE0
		public int LobbyMetadataCount(long lobbyId)
		{
			int result = 0;
			Result result2 = this.Methods.LobbyMetadataCount(this.MethodsPtr, lobbyId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004A20 File Offset: 0x00002C20
		public int MemberCount(long lobbyId)
		{
			int result = 0;
			Result result2 = this.Methods.MemberCount(this.MethodsPtr, lobbyId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004A60 File Offset: 0x00002C60
		public long GetMemberUserId(long lobbyId, int index)
		{
			long result = 0L;
			Result result2 = this.Methods.GetMemberUserId(this.MethodsPtr, lobbyId, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public User GetMemberUser(long lobbyId, long userId)
		{
			User result = default(User);
			Result result2 = this.Methods.GetMemberUser(this.MethodsPtr, lobbyId, userId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004AEC File Offset: 0x00002CEC
		public string GetMemberMetadataValue(long lobbyId, long userId, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetMemberMetadataValue(this.MethodsPtr, lobbyId, userId, key, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004B3C File Offset: 0x00002D3C
		public string GetMemberMetadataKey(long lobbyId, long userId, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			Result result = this.Methods.GetMemberMetadataKey(this.MethodsPtr, lobbyId, userId, index, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004B8C File Offset: 0x00002D8C
		public int MemberMetadataCount(long lobbyId, long userId)
		{
			int result = 0;
			Result result2 = this.Methods.MemberMetadataCount(this.MethodsPtr, lobbyId, userId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600009F RID: 159
		private static void UpdateMemberCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.UpdateMemberHandler updateMemberHandler = (LobbyManager.UpdateMemberHandler)gchandle.Target;
			gchandle.Free();
			updateMemberHandler(result);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004C00 File Offset: 0x00002E00
		public void UpdateMember(long lobbyId, long userId, LobbyMemberTransaction transaction, LobbyManager.UpdateMemberHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.UpdateMember(this.MethodsPtr, lobbyId, userId, transaction.MethodsPtr, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.UpdateMemberCallback(LobbyManager.UpdateMemberCallbackImpl));
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004C54 File Offset: 0x00002E54
		private static void SendLobbyMessageCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.SendLobbyMessageHandler sendLobbyMessageHandler = (LobbyManager.SendLobbyMessageHandler)gchandle.Target;
			gchandle.Free();
			sendLobbyMessageHandler(result);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004C88 File Offset: 0x00002E88
		public void SendLobbyMessage(long lobbyId, byte[] data, LobbyManager.SendLobbyMessageHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.SendLobbyMessage(this.MethodsPtr, lobbyId, data, data.Length, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.SendLobbyMessageCallback(LobbyManager.SendLobbyMessageCallbackImpl));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004CCC File Offset: 0x00002ECC
		public LobbySearchQuery GetSearchQuery()
		{
			LobbySearchQuery result = default(LobbySearchQuery);
			Result result2 = this.Methods.GetSearchQuery(this.MethodsPtr, ref result.MethodsPtr);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164
		private static void SearchCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.SearchHandler searchHandler = (LobbyManager.SearchHandler)gchandle.Target;
			gchandle.Free();
			searchHandler(result);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004D4C File Offset: 0x00002F4C
		public void Search(LobbySearchQuery query, LobbyManager.SearchHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.Search(this.MethodsPtr, query.MethodsPtr, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.SearchCallback(LobbyManager.SearchCallbackImpl));
			query.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004D9C File Offset: 0x00002F9C
		public int LobbyCount()
		{
			int result = 0;
			this.Methods.LobbyCount(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004DCC File Offset: 0x00002FCC
		public long GetLobbyId(int index)
		{
			long result = 0L;
			Result result2 = this.Methods.GetLobbyId(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168
		private static void ConnectVoiceCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.ConnectVoiceHandler connectVoiceHandler = (LobbyManager.ConnectVoiceHandler)gchandle.Target;
			gchandle.Free();
			connectVoiceHandler(result);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004E40 File Offset: 0x00003040
		public void ConnectVoice(long lobbyId, LobbyManager.ConnectVoiceHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ConnectVoice(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.ConnectVoiceCallback(LobbyManager.ConnectVoiceCallbackImpl));
		}

		// Token: 0x060000AA RID: 170
		private static void DisconnectVoiceCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			LobbyManager.DisconnectVoiceHandler disconnectVoiceHandler = (LobbyManager.DisconnectVoiceHandler)gchandle.Target;
			gchandle.Free();
			disconnectVoiceHandler(result);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void DisconnectVoice(long lobbyId, LobbyManager.DisconnectVoiceHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.DisconnectVoice(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(value), new LobbyManager.FFIMethods.DisconnectVoiceCallback(LobbyManager.DisconnectVoiceCallbackImpl));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void ConnectNetwork(long lobbyId)
		{
			Result result = this.Methods.ConnectNetwork(this.MethodsPtr, lobbyId);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004F2C File Offset: 0x0000312C
		public void DisconnectNetwork(long lobbyId)
		{
			Result result = this.Methods.DisconnectNetwork(this.MethodsPtr, lobbyId);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004F64 File Offset: 0x00003164
		public void FlushNetwork()
		{
			Result result = this.Methods.FlushNetwork(this.MethodsPtr);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004F9C File Offset: 0x0000319C
		public void OpenNetworkChannel(long lobbyId, byte channelId, bool reliable)
		{
			Result result = this.Methods.OpenNetworkChannel(this.MethodsPtr, lobbyId, channelId, reliable);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004FD4 File Offset: 0x000031D4
		public void SendNetworkMessage(long lobbyId, long userId, byte channelId, byte[] data)
		{
			Result result = this.Methods.SendNetworkMessage(this.MethodsPtr, lobbyId, userId, channelId, data, data.Length);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005014 File Offset: 0x00003214
		[MonoPInvokeCallback]
		private static void OnLobbyUpdateImpl(IntPtr ptr, long lobbyId)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnLobbyUpdate != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnLobbyUpdate(lobbyId);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000505C File Offset: 0x0000325C
		[MonoPInvokeCallback]
		private static void OnLobbyDeleteImpl(IntPtr ptr, long lobbyId, uint reason)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnLobbyDelete != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnLobbyDelete(lobbyId, reason);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000050A8 File Offset: 0x000032A8
		[MonoPInvokeCallback]
		private static void OnMemberConnectImpl(IntPtr ptr, long lobbyId, long userId)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnMemberConnect != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnMemberConnect(lobbyId, userId);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000050F4 File Offset: 0x000032F4
		[MonoPInvokeCallback]
		private static void OnMemberUpdateImpl(IntPtr ptr, long lobbyId, long userId)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnMemberUpdate != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnMemberUpdate(lobbyId, userId);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005140 File Offset: 0x00003340
		[MonoPInvokeCallback]
		private static void OnMemberDisconnectImpl(IntPtr ptr, long lobbyId, long userId)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnMemberDisconnect != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnMemberDisconnect(lobbyId, userId);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000518C File Offset: 0x0000338C
		[MonoPInvokeCallback]
		private static void OnLobbyMessageImpl(IntPtr ptr, long lobbyId, long userId, IntPtr dataPtr, int dataLen)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnLobbyMessage != null;
			if (flag)
			{
				byte[] array = new byte[dataLen];
				Marshal.Copy(dataPtr, array, 0, dataLen);
				discord.LobbyManagerInstance.OnLobbyMessage(lobbyId, userId, array);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000051EC File Offset: 0x000033EC
		[MonoPInvokeCallback]
		private static void OnSpeakingImpl(IntPtr ptr, long lobbyId, long userId, bool speaking)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnSpeaking != null;
			if (flag)
			{
				discord.LobbyManagerInstance.OnSpeaking(lobbyId, userId, speaking);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005238 File Offset: 0x00003438
		[MonoPInvokeCallback]
		private static void OnNetworkMessageImpl(IntPtr ptr, long lobbyId, long userId, byte channelId, IntPtr dataPtr, int dataLen)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.LobbyManagerInstance.OnNetworkMessage != null;
			if (flag)
			{
				byte[] array = new byte[dataLen];
				Marshal.Copy(dataPtr, array, 0, dataLen);
				discord.LobbyManagerInstance.OnNetworkMessage(lobbyId, userId, channelId, array);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005298 File Offset: 0x00003498
		public IEnumerable<User> GetMemberUsers(long lobbyID)
		{
			int num = this.MemberCount(lobbyID);
			List<User> list = new List<User>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetMemberUser(lobbyID, this.GetMemberUserId(lobbyID, i)));
			}
			return list;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000052E3 File Offset: 0x000034E3
		public void SendLobbyMessage(long lobbyID, string data, LobbyManager.SendLobbyMessageHandler handler)
		{
			this.SendLobbyMessage(lobbyID, Encoding.UTF8.GetBytes(data), handler);
		}

		// Token: 0x0400011B RID: 283
		private IntPtr MethodsPtr;

		// Token: 0x0400011C RID: 284
		private object MethodsStructure;

		// Token: 0x02000066 RID: 102
		internal struct FFIEvents
		{
			// Token: 0x0400019B RID: 411
			internal LobbyManager.FFIEvents.LobbyUpdateHandler OnLobbyUpdate;

			// Token: 0x0400019C RID: 412
			internal LobbyManager.FFIEvents.LobbyDeleteHandler OnLobbyDelete;

			// Token: 0x0400019D RID: 413
			internal LobbyManager.FFIEvents.MemberConnectHandler OnMemberConnect;

			// Token: 0x0400019E RID: 414
			internal LobbyManager.FFIEvents.MemberUpdateHandler OnMemberUpdate;

			// Token: 0x0400019F RID: 415
			internal LobbyManager.FFIEvents.MemberDisconnectHandler OnMemberDisconnect;

			// Token: 0x040001A0 RID: 416
			internal LobbyManager.FFIEvents.LobbyMessageHandler OnLobbyMessage;

			// Token: 0x040001A1 RID: 417
			internal LobbyManager.FFIEvents.SpeakingHandler OnSpeaking;

			// Token: 0x040001A2 RID: 418
			internal LobbyManager.FFIEvents.NetworkMessageHandler OnNetworkMessage;

			// Token: 0x020000E3 RID: 227
			// (Invoke) Token: 0x06000349 RID: 841
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void LobbyUpdateHandler(IntPtr ptr, long lobbyId);

			// Token: 0x020000E4 RID: 228
			// (Invoke) Token: 0x0600034D RID: 845
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void LobbyDeleteHandler(IntPtr ptr, long lobbyId, uint reason);

			// Token: 0x020000E5 RID: 229
			// (Invoke) Token: 0x06000351 RID: 849
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MemberConnectHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x020000E6 RID: 230
			// (Invoke) Token: 0x06000355 RID: 853
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MemberUpdateHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x020000E7 RID: 231
			// (Invoke) Token: 0x06000359 RID: 857
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MemberDisconnectHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x020000E8 RID: 232
			// (Invoke) Token: 0x0600035D RID: 861
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void LobbyMessageHandler(IntPtr ptr, long lobbyId, long userId, IntPtr dataPtr, int dataLen);

			// Token: 0x020000E9 RID: 233
			// (Invoke) Token: 0x06000361 RID: 865
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SpeakingHandler(IntPtr ptr, long lobbyId, long userId, bool speaking);

			// Token: 0x020000EA RID: 234
			// (Invoke) Token: 0x06000365 RID: 869
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void NetworkMessageHandler(IntPtr ptr, long lobbyId, long userId, byte channelId, IntPtr dataPtr, int dataLen);
		}

		// Token: 0x02000067 RID: 103
		internal struct FFIMethods
		{
			// Token: 0x040001A3 RID: 419
			internal LobbyManager.FFIMethods.GetLobbyCreateTransactionMethod GetLobbyCreateTransaction;

			// Token: 0x040001A4 RID: 420
			internal LobbyManager.FFIMethods.GetLobbyUpdateTransactionMethod GetLobbyUpdateTransaction;

			// Token: 0x040001A5 RID: 421
			internal LobbyManager.FFIMethods.GetMemberUpdateTransactionMethod GetMemberUpdateTransaction;

			// Token: 0x040001A6 RID: 422
			internal LobbyManager.FFIMethods.CreateLobbyMethod CreateLobby;

			// Token: 0x040001A7 RID: 423
			internal LobbyManager.FFIMethods.UpdateLobbyMethod UpdateLobby;

			// Token: 0x040001A8 RID: 424
			internal LobbyManager.FFIMethods.DeleteLobbyMethod DeleteLobby;

			// Token: 0x040001A9 RID: 425
			internal LobbyManager.FFIMethods.ConnectLobbyMethod ConnectLobby;

			// Token: 0x040001AA RID: 426
			internal LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretMethod ConnectLobbyWithActivitySecret;

			// Token: 0x040001AB RID: 427
			internal LobbyManager.FFIMethods.DisconnectLobbyMethod DisconnectLobby;

			// Token: 0x040001AC RID: 428
			internal LobbyManager.FFIMethods.GetLobbyMethod GetLobby;

			// Token: 0x040001AD RID: 429
			internal LobbyManager.FFIMethods.GetLobbyActivitySecretMethod GetLobbyActivitySecret;

			// Token: 0x040001AE RID: 430
			internal LobbyManager.FFIMethods.GetLobbyMetadataValueMethod GetLobbyMetadataValue;

			// Token: 0x040001AF RID: 431
			internal LobbyManager.FFIMethods.GetLobbyMetadataKeyMethod GetLobbyMetadataKey;

			// Token: 0x040001B0 RID: 432
			internal LobbyManager.FFIMethods.LobbyMetadataCountMethod LobbyMetadataCount;

			// Token: 0x040001B1 RID: 433
			internal LobbyManager.FFIMethods.MemberCountMethod MemberCount;

			// Token: 0x040001B2 RID: 434
			internal LobbyManager.FFIMethods.GetMemberUserIdMethod GetMemberUserId;

			// Token: 0x040001B3 RID: 435
			internal LobbyManager.FFIMethods.GetMemberUserMethod GetMemberUser;

			// Token: 0x040001B4 RID: 436
			internal LobbyManager.FFIMethods.GetMemberMetadataValueMethod GetMemberMetadataValue;

			// Token: 0x040001B5 RID: 437
			internal LobbyManager.FFIMethods.GetMemberMetadataKeyMethod GetMemberMetadataKey;

			// Token: 0x040001B6 RID: 438
			internal LobbyManager.FFIMethods.MemberMetadataCountMethod MemberMetadataCount;

			// Token: 0x040001B7 RID: 439
			internal LobbyManager.FFIMethods.UpdateMemberMethod UpdateMember;

			// Token: 0x040001B8 RID: 440
			internal LobbyManager.FFIMethods.SendLobbyMessageMethod SendLobbyMessage;

			// Token: 0x040001B9 RID: 441
			internal LobbyManager.FFIMethods.GetSearchQueryMethod GetSearchQuery;

			// Token: 0x040001BA RID: 442
			internal LobbyManager.FFIMethods.SearchMethod Search;

			// Token: 0x040001BB RID: 443
			internal LobbyManager.FFIMethods.LobbyCountMethod LobbyCount;

			// Token: 0x040001BC RID: 444
			internal LobbyManager.FFIMethods.GetLobbyIdMethod GetLobbyId;

			// Token: 0x040001BD RID: 445
			internal LobbyManager.FFIMethods.ConnectVoiceMethod ConnectVoice;

			// Token: 0x040001BE RID: 446
			internal LobbyManager.FFIMethods.DisconnectVoiceMethod DisconnectVoice;

			// Token: 0x040001BF RID: 447
			internal LobbyManager.FFIMethods.ConnectNetworkMethod ConnectNetwork;

			// Token: 0x040001C0 RID: 448
			internal LobbyManager.FFIMethods.DisconnectNetworkMethod DisconnectNetwork;

			// Token: 0x040001C1 RID: 449
			internal LobbyManager.FFIMethods.FlushNetworkMethod FlushNetwork;

			// Token: 0x040001C2 RID: 450
			internal LobbyManager.FFIMethods.OpenNetworkChannelMethod OpenNetworkChannel;

			// Token: 0x040001C3 RID: 451
			internal LobbyManager.FFIMethods.SendNetworkMessageMethod SendNetworkMessage;

			// Token: 0x020000EB RID: 235
			// (Invoke) Token: 0x06000369 RID: 873
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyCreateTransactionMethod(IntPtr methodsPtr, ref IntPtr transaction);

			// Token: 0x020000EC RID: 236
			// (Invoke) Token: 0x0600036D RID: 877
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyUpdateTransactionMethod(IntPtr methodsPtr, long lobbyId, ref IntPtr transaction);

			// Token: 0x020000ED RID: 237
			// (Invoke) Token: 0x06000371 RID: 881
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMemberUpdateTransactionMethod(IntPtr methodsPtr, long lobbyId, long userId, ref IntPtr transaction);

			// Token: 0x020000EE RID: 238
			// (Invoke) Token: 0x06000375 RID: 885
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CreateLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x020000EF RID: 239
			// (Invoke) Token: 0x06000379 RID: 889
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CreateLobbyMethod(IntPtr methodsPtr, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.CreateLobbyCallback callback);

			// Token: 0x020000F0 RID: 240
			// (Invoke) Token: 0x0600037D RID: 893
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x020000F1 RID: 241
			// (Invoke) Token: 0x06000381 RID: 897
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.UpdateLobbyCallback callback);

			// Token: 0x020000F2 RID: 242
			// (Invoke) Token: 0x06000385 RID: 901
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DeleteLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x020000F3 RID: 243
			// (Invoke) Token: 0x06000389 RID: 905
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DeleteLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DeleteLobbyCallback callback);

			// Token: 0x020000F4 RID: 244
			// (Invoke) Token: 0x0600038D RID: 909
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x020000F5 RID: 245
			// (Invoke) Token: 0x06000391 RID: 913
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectLobbyMethod(IntPtr methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string secret, IntPtr callbackData, LobbyManager.FFIMethods.ConnectLobbyCallback callback);

			// Token: 0x020000F6 RID: 246
			// (Invoke) Token: 0x06000395 RID: 917
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectLobbyWithActivitySecretCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x020000F7 RID: 247
			// (Invoke) Token: 0x06000399 RID: 921
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectLobbyWithActivitySecretMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string activitySecret, IntPtr callbackData, LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback callback);

			// Token: 0x020000F8 RID: 248
			// (Invoke) Token: 0x0600039D RID: 925
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DisconnectLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x020000F9 RID: 249
			// (Invoke) Token: 0x060003A1 RID: 929
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DisconnectLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DisconnectLobbyCallback callback);

			// Token: 0x020000FA RID: 250
			// (Invoke) Token: 0x060003A5 RID: 933
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyMethod(IntPtr methodsPtr, long lobbyId, ref Lobby lobby);

			// Token: 0x020000FB RID: 251
			// (Invoke) Token: 0x060003A9 RID: 937
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyActivitySecretMethod(IntPtr methodsPtr, long lobbyId, StringBuilder secret);

			// Token: 0x020000FC RID: 252
			// (Invoke) Token: 0x060003AD RID: 941
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyMetadataValueMethod(IntPtr methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value);

			// Token: 0x020000FD RID: 253
			// (Invoke) Token: 0x060003B1 RID: 945
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyMetadataKeyMethod(IntPtr methodsPtr, long lobbyId, int index, StringBuilder key);

			// Token: 0x020000FE RID: 254
			// (Invoke) Token: 0x060003B5 RID: 949
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result LobbyMetadataCountMethod(IntPtr methodsPtr, long lobbyId, ref int count);

			// Token: 0x020000FF RID: 255
			// (Invoke) Token: 0x060003B9 RID: 953
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result MemberCountMethod(IntPtr methodsPtr, long lobbyId, ref int count);

			// Token: 0x02000100 RID: 256
			// (Invoke) Token: 0x060003BD RID: 957
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMemberUserIdMethod(IntPtr methodsPtr, long lobbyId, int index, ref long userId);

			// Token: 0x02000101 RID: 257
			// (Invoke) Token: 0x060003C1 RID: 961
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMemberUserMethod(IntPtr methodsPtr, long lobbyId, long userId, ref User user);

			// Token: 0x02000102 RID: 258
			// (Invoke) Token: 0x060003C5 RID: 965
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMemberMetadataValueMethod(IntPtr methodsPtr, long lobbyId, long userId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value);

			// Token: 0x02000103 RID: 259
			// (Invoke) Token: 0x060003C9 RID: 969
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetMemberMetadataKeyMethod(IntPtr methodsPtr, long lobbyId, long userId, int index, StringBuilder key);

			// Token: 0x02000104 RID: 260
			// (Invoke) Token: 0x060003CD RID: 973
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result MemberMetadataCountMethod(IntPtr methodsPtr, long lobbyId, long userId, ref int count);

			// Token: 0x02000105 RID: 261
			// (Invoke) Token: 0x060003D1 RID: 977
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateMemberCallback(IntPtr ptr, Result result);

			// Token: 0x02000106 RID: 262
			// (Invoke) Token: 0x060003D5 RID: 981
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void UpdateMemberMethod(IntPtr methodsPtr, long lobbyId, long userId, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.UpdateMemberCallback callback);

			// Token: 0x02000107 RID: 263
			// (Invoke) Token: 0x060003D9 RID: 985
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendLobbyMessageCallback(IntPtr ptr, Result result);

			// Token: 0x02000108 RID: 264
			// (Invoke) Token: 0x060003DD RID: 989
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SendLobbyMessageMethod(IntPtr methodsPtr, long lobbyId, byte[] data, int dataLen, IntPtr callbackData, LobbyManager.FFIMethods.SendLobbyMessageCallback callback);

			// Token: 0x02000109 RID: 265
			// (Invoke) Token: 0x060003E1 RID: 993
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetSearchQueryMethod(IntPtr methodsPtr, ref IntPtr query);

			// Token: 0x0200010A RID: 266
			// (Invoke) Token: 0x060003E5 RID: 997
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SearchCallback(IntPtr ptr, Result result);

			// Token: 0x0200010B RID: 267
			// (Invoke) Token: 0x060003E9 RID: 1001
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void SearchMethod(IntPtr methodsPtr, IntPtr query, IntPtr callbackData, LobbyManager.FFIMethods.SearchCallback callback);

			// Token: 0x0200010C RID: 268
			// (Invoke) Token: 0x060003ED RID: 1005
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void LobbyCountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x0200010D RID: 269
			// (Invoke) Token: 0x060003F1 RID: 1009
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetLobbyIdMethod(IntPtr methodsPtr, int index, ref long lobbyId);

			// Token: 0x0200010E RID: 270
			// (Invoke) Token: 0x060003F5 RID: 1013
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectVoiceCallback(IntPtr ptr, Result result);

			// Token: 0x0200010F RID: 271
			// (Invoke) Token: 0x060003F9 RID: 1017
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ConnectVoiceMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.ConnectVoiceCallback callback);

			// Token: 0x02000110 RID: 272
			// (Invoke) Token: 0x060003FD RID: 1021
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DisconnectVoiceCallback(IntPtr ptr, Result result);

			// Token: 0x02000111 RID: 273
			// (Invoke) Token: 0x06000401 RID: 1025
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void DisconnectVoiceMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DisconnectVoiceCallback callback);

			// Token: 0x02000112 RID: 274
			// (Invoke) Token: 0x06000405 RID: 1029
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result ConnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

			// Token: 0x02000113 RID: 275
			// (Invoke) Token: 0x06000409 RID: 1033
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result DisconnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

			// Token: 0x02000114 RID: 276
			// (Invoke) Token: 0x0600040D RID: 1037
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result FlushNetworkMethod(IntPtr methodsPtr);

			// Token: 0x02000115 RID: 277
			// (Invoke) Token: 0x06000411 RID: 1041
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result OpenNetworkChannelMethod(IntPtr methodsPtr, long lobbyId, byte channelId, bool reliable);

			// Token: 0x02000116 RID: 278
			// (Invoke) Token: 0x06000415 RID: 1045
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SendNetworkMessageMethod(IntPtr methodsPtr, long lobbyId, long userId, byte channelId, byte[] data, int dataLen);
		}

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x0600018E RID: 398
		public delegate void CreateLobbyHandler(Result result, ref Lobby lobby);

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x06000192 RID: 402
		public delegate void UpdateLobbyHandler(Result result);

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x06000196 RID: 406
		public delegate void DeleteLobbyHandler(Result result);

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x0600019A RID: 410
		public delegate void ConnectLobbyHandler(Result result, ref Lobby lobby);

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x0600019E RID: 414
		public delegate void ConnectLobbyWithActivitySecretHandler(Result result, ref Lobby lobby);

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x060001A2 RID: 418
		public delegate void DisconnectLobbyHandler(Result result);

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x060001A6 RID: 422
		public delegate void UpdateMemberHandler(Result result);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x060001AA RID: 426
		public delegate void SendLobbyMessageHandler(Result result);

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x060001AE RID: 430
		public delegate void SearchHandler(Result result);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060001B2 RID: 434
		public delegate void ConnectVoiceHandler(Result result);

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x060001B6 RID: 438
		public delegate void DisconnectVoiceHandler(Result result);

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x060001BA RID: 442
		public delegate void LobbyUpdateHandler(long lobbyId);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x060001BE RID: 446
		public delegate void LobbyDeleteHandler(long lobbyId, uint reason);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060001C2 RID: 450
		public delegate void MemberConnectHandler(long lobbyId, long userId);

		// Token: 0x02000076 RID: 118
		// (Invoke) Token: 0x060001C6 RID: 454
		public delegate void MemberUpdateHandler(long lobbyId, long userId);

		// Token: 0x02000077 RID: 119
		// (Invoke) Token: 0x060001CA RID: 458
		public delegate void MemberDisconnectHandler(long lobbyId, long userId);

		// Token: 0x02000078 RID: 120
		// (Invoke) Token: 0x060001CE RID: 462
		public delegate void LobbyMessageHandler(long lobbyId, long userId, byte[] data);

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x060001D2 RID: 466
		public delegate void SpeakingHandler(long lobbyId, long userId, bool speaking);

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x060001D6 RID: 470
		public delegate void NetworkMessageHandler(long lobbyId, long userId, byte channelId, byte[] data);
	}
}
