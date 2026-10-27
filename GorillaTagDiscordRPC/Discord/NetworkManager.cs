using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200003A RID: 58
	public class NetworkManager
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000052FC File Offset: 0x000034FC
		private NetworkManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(NetworkManager.FFIMethods));
				}
				return (NetworkManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000BC RID: 188 RVA: 0x00005344 File Offset: 0x00003544
		// (remove) Token: 0x060000BD RID: 189 RVA: 0x0000537C File Offset: 0x0000357C

		public event NetworkManager.MessageHandler OnMessage;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000BE RID: 190 RVA: 0x000053B4 File Offset: 0x000035B4
		// (remove) Token: 0x060000BF RID: 191 RVA: 0x000053EC File Offset: 0x000035EC

		public event NetworkManager.RouteUpdateHandler OnRouteUpdate;

		// Token: 0x060000C0 RID: 192 RVA: 0x00005424 File Offset: 0x00003624
		internal NetworkManager(IntPtr ptr, IntPtr eventsPtr, ref NetworkManager.FFIEvents events)
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

		// Token: 0x060000C1 RID: 193 RVA: 0x0000547C File Offset: 0x0000367C
		private void InitEvents(IntPtr eventsPtr, ref NetworkManager.FFIEvents events)
		{
			events.OnMessage = new NetworkManager.FFIEvents.MessageHandler(NetworkManager.OnMessageImpl);
			events.OnRouteUpdate = new NetworkManager.FFIEvents.RouteUpdateHandler(NetworkManager.OnRouteUpdateImpl);
			Marshal.StructureToPtr<NetworkManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000054B4 File Offset: 0x000036B4
		public ulong GetPeerId()
		{
			ulong result = 0UL;
			this.Methods.GetPeerId(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000054E4 File Offset: 0x000036E4
		public void Flush()
		{
			Result result = this.Methods.Flush(this.MethodsPtr);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000551C File Offset: 0x0000371C
		public void OpenPeer(ulong peerId, string routeData)
		{
			Result result = this.Methods.OpenPeer(this.MethodsPtr, peerId, routeData);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005554 File Offset: 0x00003754
		public void UpdatePeer(ulong peerId, string routeData)
		{
			Result result = this.Methods.UpdatePeer(this.MethodsPtr, peerId, routeData);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000558C File Offset: 0x0000378C
		public void ClosePeer(ulong peerId)
		{
			Result result = this.Methods.ClosePeer(this.MethodsPtr, peerId);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000055C4 File Offset: 0x000037C4
		public void OpenChannel(ulong peerId, byte channelId, bool reliable)
		{
			Result result = this.Methods.OpenChannel(this.MethodsPtr, peerId, channelId, reliable);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000055FC File Offset: 0x000037FC
		public void CloseChannel(ulong peerId, byte channelId)
		{
			Result result = this.Methods.CloseChannel(this.MethodsPtr, peerId, channelId);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005634 File Offset: 0x00003834
		public void SendMessage(ulong peerId, byte channelId, byte[] data)
		{
			Result result = this.Methods.SendMessage(this.MethodsPtr, peerId, channelId, data, data.Length);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005670 File Offset: 0x00003870
		[MonoPInvokeCallback]
		private static void OnMessageImpl(IntPtr ptr, ulong peerId, byte channelId, IntPtr dataPtr, int dataLen)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.NetworkManagerInstance.OnMessage != null;
			if (flag)
			{
				byte[] array = new byte[dataLen];
				Marshal.Copy(dataPtr, array, 0, dataLen);
				discord.NetworkManagerInstance.OnMessage(peerId, channelId, array);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000056D0 File Offset: 0x000038D0
		[MonoPInvokeCallback]
		private static void OnRouteUpdateImpl(IntPtr ptr, string routeData)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.NetworkManagerInstance.OnRouteUpdate != null;
			if (flag)
			{
				discord.NetworkManagerInstance.OnRouteUpdate(routeData);
			}
		}

		// Token: 0x04000125 RID: 293
		private IntPtr MethodsPtr;

		// Token: 0x04000126 RID: 294
		private object MethodsStructure;

		// Token: 0x0200007B RID: 123
		internal struct FFIEvents
		{
			// Token: 0x040001C4 RID: 452
			internal NetworkManager.FFIEvents.MessageHandler OnMessage;

			// Token: 0x040001C5 RID: 453
			internal NetworkManager.FFIEvents.RouteUpdateHandler OnRouteUpdate;

			// Token: 0x02000117 RID: 279
			// (Invoke) Token: 0x06000419 RID: 1049
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void MessageHandler(IntPtr ptr, ulong peerId, byte channelId, IntPtr dataPtr, int dataLen);

			// Token: 0x02000118 RID: 280
			// (Invoke) Token: 0x0600041D RID: 1053
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void RouteUpdateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string routeData);
		}

		// Token: 0x0200007C RID: 124
		internal struct FFIMethods
		{
			// Token: 0x040001C6 RID: 454
			internal NetworkManager.FFIMethods.GetPeerIdMethod GetPeerId;

			// Token: 0x040001C7 RID: 455
			internal NetworkManager.FFIMethods.FlushMethod Flush;

			// Token: 0x040001C8 RID: 456
			internal NetworkManager.FFIMethods.OpenPeerMethod OpenPeer;

			// Token: 0x040001C9 RID: 457
			internal NetworkManager.FFIMethods.UpdatePeerMethod UpdatePeer;

			// Token: 0x040001CA RID: 458
			internal NetworkManager.FFIMethods.ClosePeerMethod ClosePeer;

			// Token: 0x040001CB RID: 459
			internal NetworkManager.FFIMethods.OpenChannelMethod OpenChannel;

			// Token: 0x040001CC RID: 460
			internal NetworkManager.FFIMethods.CloseChannelMethod CloseChannel;

			// Token: 0x040001CD RID: 461
			internal NetworkManager.FFIMethods.SendMessageMethod SendMessage;

			// Token: 0x02000119 RID: 281
			// (Invoke) Token: 0x06000421 RID: 1057
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void GetPeerIdMethod(IntPtr methodsPtr, ref ulong peerId);

			// Token: 0x0200011A RID: 282
			// (Invoke) Token: 0x06000425 RID: 1061
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result FlushMethod(IntPtr methodsPtr);

			// Token: 0x0200011B RID: 283
			// (Invoke) Token: 0x06000429 RID: 1065
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result OpenPeerMethod(IntPtr methodsPtr, ulong peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

			// Token: 0x0200011C RID: 284
			// (Invoke) Token: 0x0600042D RID: 1069
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result UpdatePeerMethod(IntPtr methodsPtr, ulong peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

			// Token: 0x0200011D RID: 285
			// (Invoke) Token: 0x06000431 RID: 1073
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result ClosePeerMethod(IntPtr methodsPtr, ulong peerId);

			// Token: 0x0200011E RID: 286
			// (Invoke) Token: 0x06000435 RID: 1077
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result OpenChannelMethod(IntPtr methodsPtr, ulong peerId, byte channelId, bool reliable);

			// Token: 0x0200011F RID: 287
			// (Invoke) Token: 0x06000439 RID: 1081
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result CloseChannelMethod(IntPtr methodsPtr, ulong peerId, byte channelId);

			// Token: 0x02000120 RID: 288
			// (Invoke) Token: 0x0600043D RID: 1085
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result SendMessageMethod(IntPtr methodsPtr, ulong peerId, byte channelId, byte[] data, int dataLen);
		}

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x060001DA RID: 474
		public delegate void MessageHandler(ulong peerId, byte channelId, byte[] data);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x060001DE RID: 478
		public delegate void RouteUpdateHandler(string routeData);
	}
}
