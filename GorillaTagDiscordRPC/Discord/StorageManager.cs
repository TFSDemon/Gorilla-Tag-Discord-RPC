using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x0200003C RID: 60
	public class StorageManager
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005D10 File Offset: 0x00003F10
		private StorageManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(StorageManager.FFIMethods));
				}
				return (StorageManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005D58 File Offset: 0x00003F58
		internal StorageManager(IntPtr ptr, IntPtr eventsPtr, ref StorageManager.FFIEvents events)
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

		// Token: 0x060000ED RID: 237 RVA: 0x00005DB0 File Offset: 0x00003FB0
		private void InitEvents(IntPtr eventsPtr, ref StorageManager.FFIEvents events)
		{
			Marshal.StructureToPtr<StorageManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public uint Read(string name, byte[] data)
		{
			uint result = 0U;
			Result result2 = this.Methods.Read(this.MethodsPtr, name, data, data.Length, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005E08 File Offset: 0x00004008
		[MonoPInvokeCallback]
		private static void ReadAsyncCallbackImpl(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StorageManager.ReadAsyncHandler readAsyncHandler = (StorageManager.ReadAsyncHandler)gchandle.Target;
			gchandle.Free();
			byte[] array = new byte[dataLen];
			Marshal.Copy(dataPtr, array, 0, dataLen);
			readAsyncHandler(result, array);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005E4C File Offset: 0x0000404C
		public void ReadAsync(string name, StorageManager.ReadAsyncHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ReadAsync(this.MethodsPtr, name, GCHandle.ToIntPtr(value), new StorageManager.FFIMethods.ReadAsyncCallback(StorageManager.ReadAsyncCallbackImpl));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005E8C File Offset: 0x0000408C
		[MonoPInvokeCallback]
		private static void ReadAsyncPartialCallbackImpl(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StorageManager.ReadAsyncPartialHandler readAsyncPartialHandler = (StorageManager.ReadAsyncPartialHandler)gchandle.Target;
			gchandle.Free();
			byte[] array = new byte[dataLen];
			Marshal.Copy(dataPtr, array, 0, dataLen);
			readAsyncPartialHandler(result, array);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005ED0 File Offset: 0x000040D0
		public void ReadAsyncPartial(string name, ulong offset, ulong length, StorageManager.ReadAsyncPartialHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.ReadAsyncPartial(this.MethodsPtr, name, offset, length, GCHandle.ToIntPtr(value), new StorageManager.FFIMethods.ReadAsyncPartialCallback(StorageManager.ReadAsyncPartialCallbackImpl));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005F14 File Offset: 0x00004114
		public void Write(string name, byte[] data)
		{
			Result result = this.Methods.Write(this.MethodsPtr, name, data, data.Length);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005F50 File Offset: 0x00004150
		[MonoPInvokeCallback]
		private static void WriteAsyncCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StorageManager.WriteAsyncHandler writeAsyncHandler = (StorageManager.WriteAsyncHandler)gchandle.Target;
			gchandle.Free();
			writeAsyncHandler(result);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005F84 File Offset: 0x00004184
		public void WriteAsync(string name, byte[] data, StorageManager.WriteAsyncHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.WriteAsync(this.MethodsPtr, name, data, data.Length, GCHandle.ToIntPtr(value), new StorageManager.FFIMethods.WriteAsyncCallback(StorageManager.WriteAsyncCallbackImpl));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005FC8 File Offset: 0x000041C8
		public void Delete(string name)
		{
			Result result = this.Methods.Delete(this.MethodsPtr, name);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006000 File Offset: 0x00004200
		public bool Exists(string name)
		{
			bool result = false;
			Result result2 = this.Methods.Exists(this.MethodsPtr, name, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006040 File Offset: 0x00004240
		public int Count()
		{
			int result = 0;
			this.Methods.Count(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006070 File Offset: 0x00004270
		public FileStat Stat(string name)
		{
			FileStat result = default(FileStat);
			Result result2 = this.Methods.Stat(this.MethodsPtr, name, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000060B8 File Offset: 0x000042B8
		public FileStat StatAt(int index)
		{
			FileStat result = default(FileStat);
			Result result2 = this.Methods.StatAt(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006100 File Offset: 0x00004300
		public string GetPath()
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetPath(this.MethodsPtr, stringBuilder);
			bool flag = result > Result.Ok;
			if (flag)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000614C File Offset: 0x0000434C
		public IEnumerable<FileStat> Files()
		{
			int num = this.Count();
			List<FileStat> list = new List<FileStat>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.StatAt(i));
			}
			return list;
		}

		// Token: 0x0400012C RID: 300
		private IntPtr MethodsPtr;

		// Token: 0x0400012D RID: 301
		private object MethodsStructure;

		// Token: 0x02000088 RID: 136
		internal struct FFIEvents
		{
		}

		// Token: 0x02000089 RID: 137
		internal struct FFIMethods
		{
			// Token: 0x040001E2 RID: 482
			internal StorageManager.FFIMethods.ReadMethod Read;

			// Token: 0x040001E3 RID: 483
			internal StorageManager.FFIMethods.ReadAsyncMethod ReadAsync;

			// Token: 0x040001E4 RID: 484
			internal StorageManager.FFIMethods.ReadAsyncPartialMethod ReadAsyncPartial;

			// Token: 0x040001E5 RID: 485
			internal StorageManager.FFIMethods.WriteMethod Write;

			// Token: 0x040001E6 RID: 486
			internal StorageManager.FFIMethods.WriteAsyncMethod WriteAsync;

			// Token: 0x040001E7 RID: 487
			internal StorageManager.FFIMethods.DeleteMethod Delete;

			// Token: 0x040001E8 RID: 488
			internal StorageManager.FFIMethods.ExistsMethod Exists;

			// Token: 0x040001E9 RID: 489
			internal StorageManager.FFIMethods.CountMethod Count;

			// Token: 0x040001EA RID: 490
			internal StorageManager.FFIMethods.StatMethod Stat;

			// Token: 0x040001EB RID: 491
			internal StorageManager.FFIMethods.StatAtMethod StatAt;

			// Token: 0x040001EC RID: 492
			internal StorageManager.FFIMethods.GetPathMethod GetPath;

			// Token: 0x0200013B RID: 315
			// (Invoke) Token: 0x060004A9 RID: 1193
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result ReadMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen, ref uint read);

			// Token: 0x0200013C RID: 316
			// (Invoke) Token: 0x060004AD RID: 1197
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ReadAsyncCallback(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen);

			// Token: 0x0200013D RID: 317
			// (Invoke) Token: 0x060004B1 RID: 1201
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ReadAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, IntPtr callbackData, StorageManager.FFIMethods.ReadAsyncCallback callback);

			// Token: 0x0200013E RID: 318
			// (Invoke) Token: 0x060004B5 RID: 1205
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ReadAsyncPartialCallback(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen);

			// Token: 0x0200013F RID: 319
			// (Invoke) Token: 0x060004B9 RID: 1209
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void ReadAsyncPartialMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ulong offset, ulong length, IntPtr callbackData, StorageManager.FFIMethods.ReadAsyncPartialCallback callback);

			// Token: 0x02000140 RID: 320
			// (Invoke) Token: 0x060004BD RID: 1213
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result WriteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen);

			// Token: 0x02000141 RID: 321
			// (Invoke) Token: 0x060004C1 RID: 1217
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void WriteAsyncCallback(IntPtr ptr, Result result);

			// Token: 0x02000142 RID: 322
			// (Invoke) Token: 0x060004C5 RID: 1221
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void WriteAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen, IntPtr callbackData, StorageManager.FFIMethods.WriteAsyncCallback callback);

			// Token: 0x02000143 RID: 323
			// (Invoke) Token: 0x060004C9 RID: 1225
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result DeleteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name);

			// Token: 0x02000144 RID: 324
			// (Invoke) Token: 0x060004CD RID: 1229
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result ExistsMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref bool exists);

			// Token: 0x02000145 RID: 325
			// (Invoke) Token: 0x060004D1 RID: 1233
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x02000146 RID: 326
			// (Invoke) Token: 0x060004D5 RID: 1237
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result StatMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref FileStat stat);

			// Token: 0x02000147 RID: 327
			// (Invoke) Token: 0x060004D9 RID: 1241
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result StatAtMethod(IntPtr methodsPtr, int index, ref FileStat stat);

			// Token: 0x02000148 RID: 328
			// (Invoke) Token: 0x060004DD RID: 1245
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetPathMethod(IntPtr methodsPtr, StringBuilder path);
		}

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x060001FE RID: 510
		public delegate void ReadAsyncHandler(Result result, byte[] data);

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x06000202 RID: 514
		public delegate void ReadAsyncPartialHandler(Result result, byte[] data);

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x06000206 RID: 518
		public delegate void WriteAsyncHandler(Result result);
	}
}
