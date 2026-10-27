using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000037 RID: 55
	public class ImageManager
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003A9C File Offset: 0x00001C9C
		private ImageManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ImageManager.FFIMethods));
				}
				return (ImageManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003AE4 File Offset: 0x00001CE4
		internal ImageManager(IntPtr ptr, IntPtr eventsPtr, ref ImageManager.FFIEvents events)
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

		// Token: 0x0600005D RID: 93 RVA: 0x00003B3C File Offset: 0x00001D3C
		private void InitEvents(IntPtr eventsPtr, ref ImageManager.FFIEvents events)
		{
			Marshal.StructureToPtr<ImageManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003B50 File Offset: 0x00001D50
		[MonoPInvokeCallback]
		private static void FetchCallbackImpl(IntPtr ptr, Result result, ImageHandle handleResult)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			ImageManager.FetchHandler fetchHandler = (ImageManager.FetchHandler)gchandle.Target;
			gchandle.Free();
			fetchHandler(result, handleResult);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003B84 File Offset: 0x00001D84
		public void Fetch(ImageHandle handle, bool refresh, ImageManager.FetchHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.Fetch(this.MethodsPtr, handle, refresh, GCHandle.ToIntPtr(value), new ImageManager.FFIMethods.FetchCallback(ImageManager.FetchCallbackImpl));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public ImageDimensions GetDimensions(ImageHandle handle)
		{
			ImageDimensions result = default(ImageDimensions);
			Result result2 = this.Methods.GetDimensions(this.MethodsPtr, handle, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void GetData(ImageHandle handle, byte[] data)
		{
			Result result = this.Methods.GetData(this.MethodsPtr, handle, data, data.Length);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C46 File Offset: 0x00001E46
		public void Fetch(ImageHandle handle, ImageManager.FetchHandler callback)
		{
			this.Fetch(handle, false, callback);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C54 File Offset: 0x00001E54
		public byte[] GetData(ImageHandle handle)
		{
			ImageDimensions dimensions = this.GetDimensions(handle);
			byte[] array = new byte[dimensions.Width * dimensions.Height * 4U];
			this.GetData(handle, array);
			return array;
		}

		// Token: 0x04000115 RID: 277
		private IntPtr MethodsPtr;

		// Token: 0x04000116 RID: 278
		private object MethodsStructure;

		// Token: 0x0200005E RID: 94
		internal struct FFIEvents
		{
		}

		// Token: 0x0200005F RID: 95
		internal struct FFIMethods
		{
			// Token: 0x04000192 RID: 402
			internal ImageManager.FFIMethods.FetchMethod Fetch;

			// Token: 0x04000193 RID: 403
			internal ImageManager.FFIMethods.GetDimensionsMethod GetDimensions;

			// Token: 0x04000194 RID: 404
			internal ImageManager.FFIMethods.GetDataMethod GetData;

			// Token: 0x020000D8 RID: 216
			// (Invoke) Token: 0x0600031D RID: 797
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchCallback(IntPtr ptr, Result result, ImageHandle handleResult);

			// Token: 0x020000D9 RID: 217
			// (Invoke) Token: 0x06000321 RID: 801
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchMethod(IntPtr methodsPtr, ImageHandle handle, bool refresh, IntPtr callbackData, ImageManager.FFIMethods.FetchCallback callback);

			// Token: 0x020000DA RID: 218
			// (Invoke) Token: 0x06000325 RID: 805
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetDimensionsMethod(IntPtr methodsPtr, ImageHandle handle, ref ImageDimensions dimensions);

			// Token: 0x020000DB RID: 219
			// (Invoke) Token: 0x06000329 RID: 809
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetDataMethod(IntPtr methodsPtr, ImageHandle handle, byte[] data, int dataLen);
		}

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x0600017E RID: 382
		public delegate void FetchHandler(Result result, ImageHandle handleResult);
	}
}
