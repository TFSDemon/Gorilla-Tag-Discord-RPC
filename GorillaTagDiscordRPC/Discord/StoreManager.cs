using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200003D RID: 61
	public class StoreManager
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006190 File Offset: 0x00004390
		private StoreManager.FFIMethods Methods
		{
			get
			{
				bool flag = this.MethodsStructure == null;
				if (flag)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(StoreManager.FFIMethods));
				}
				return (StoreManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000FE RID: 254 RVA: 0x000061D8 File Offset: 0x000043D8
		// (remove) Token: 0x060000FF RID: 255 RVA: 0x00006210 File Offset: 0x00004410

		public event StoreManager.EntitlementCreateHandler OnEntitlementCreate;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000100 RID: 256 RVA: 0x00006248 File Offset: 0x00004448
		// (remove) Token: 0x06000101 RID: 257 RVA: 0x00006280 File Offset: 0x00004480

		public event StoreManager.EntitlementDeleteHandler OnEntitlementDelete;

		// Token: 0x06000102 RID: 258 RVA: 0x000062B8 File Offset: 0x000044B8
		internal StoreManager(IntPtr ptr, IntPtr eventsPtr, ref StoreManager.FFIEvents events)
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

		// Token: 0x06000103 RID: 259 RVA: 0x00006310 File Offset: 0x00004510
		private void InitEvents(IntPtr eventsPtr, ref StoreManager.FFIEvents events)
		{
			events.OnEntitlementCreate = new StoreManager.FFIEvents.EntitlementCreateHandler(StoreManager.OnEntitlementCreateImpl);
			events.OnEntitlementDelete = new StoreManager.FFIEvents.EntitlementDeleteHandler(StoreManager.OnEntitlementDeleteImpl);
			Marshal.StructureToPtr<StoreManager.FFIEvents>(events, eventsPtr, false);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006348 File Offset: 0x00004548
		[MonoPInvokeCallback]
		private static void FetchSkusCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StoreManager.FetchSkusHandler fetchSkusHandler = (StoreManager.FetchSkusHandler)gchandle.Target;
			gchandle.Free();
			fetchSkusHandler(result);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000637C File Offset: 0x0000457C
		public void FetchSkus(StoreManager.FetchSkusHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.FetchSkus(this.MethodsPtr, GCHandle.ToIntPtr(value), new StoreManager.FFIMethods.FetchSkusCallback(StoreManager.FetchSkusCallbackImpl));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000063BC File Offset: 0x000045BC
		public int CountSkus()
		{
			int result = 0;
			this.Methods.CountSkus(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000063EC File Offset: 0x000045EC
		public Sku GetSku(long skuId)
		{
			Sku result = default(Sku);
			Result result2 = this.Methods.GetSku(this.MethodsPtr, skuId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006434 File Offset: 0x00004634
		public Sku GetSkuAt(int index)
		{
			Sku result = default(Sku);
			Result result2 = this.Methods.GetSkuAt(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000647C File Offset: 0x0000467C
		[MonoPInvokeCallback]
		private static void FetchEntitlementsCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StoreManager.FetchEntitlementsHandler fetchEntitlementsHandler = (StoreManager.FetchEntitlementsHandler)gchandle.Target;
			gchandle.Free();
			fetchEntitlementsHandler(result);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000064B0 File Offset: 0x000046B0
		public void FetchEntitlements(StoreManager.FetchEntitlementsHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.FetchEntitlements(this.MethodsPtr, GCHandle.ToIntPtr(value), new StoreManager.FFIMethods.FetchEntitlementsCallback(StoreManager.FetchEntitlementsCallbackImpl));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000064F0 File Offset: 0x000046F0
		public int CountEntitlements()
		{
			int result = 0;
			this.Methods.CountEntitlements(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006520 File Offset: 0x00004720
		public Entitlement GetEntitlement(long entitlementId)
		{
			Entitlement result = default(Entitlement);
			Result result2 = this.Methods.GetEntitlement(this.MethodsPtr, entitlementId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006568 File Offset: 0x00004768
		public Entitlement GetEntitlementAt(int index)
		{
			Entitlement result = default(Entitlement);
			Result result2 = this.Methods.GetEntitlementAt(this.MethodsPtr, index, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000065B0 File Offset: 0x000047B0
		public bool HasSkuEntitlement(long skuId)
		{
			bool result = false;
			Result result2 = this.Methods.HasSkuEntitlement(this.MethodsPtr, skuId, ref result);
			bool flag = result2 > Result.Ok;
			if (flag)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000065F0 File Offset: 0x000047F0
		[MonoPInvokeCallback]
		private static void StartPurchaseCallbackImpl(IntPtr ptr, Result result)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptr);
			StoreManager.StartPurchaseHandler startPurchaseHandler = (StoreManager.StartPurchaseHandler)gchandle.Target;
			gchandle.Free();
			startPurchaseHandler(result);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006624 File Offset: 0x00004824
		public void StartPurchase(long skuId, StoreManager.StartPurchaseHandler callback)
		{
			GCHandle value = GCHandle.Alloc(callback);
			this.Methods.StartPurchase(this.MethodsPtr, skuId, GCHandle.ToIntPtr(value), new StoreManager.FFIMethods.StartPurchaseCallback(StoreManager.StartPurchaseCallbackImpl));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006664 File Offset: 0x00004864
		[MonoPInvokeCallback]
		private static void OnEntitlementCreateImpl(IntPtr ptr, ref Entitlement entitlement)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.StoreManagerInstance.OnEntitlementCreate != null;
			if (flag)
			{
				discord.StoreManagerInstance.OnEntitlementCreate(ref entitlement);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000066AC File Offset: 0x000048AC
		[MonoPInvokeCallback]
		private static void OnEntitlementDeleteImpl(IntPtr ptr, ref Entitlement entitlement)
		{
			Discord discord = (Discord)GCHandle.FromIntPtr(ptr).Target;
			bool flag = discord.StoreManagerInstance.OnEntitlementDelete != null;
			if (flag)
			{
				discord.StoreManagerInstance.OnEntitlementDelete(ref entitlement);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000066F4 File Offset: 0x000048F4
		public IEnumerable<Entitlement> GetEntitlements()
		{
			int num = this.CountEntitlements();
			List<Entitlement> list = new List<Entitlement>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetEntitlementAt(i));
			}
			return list;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006738 File Offset: 0x00004938
		public IEnumerable<Sku> GetSkus()
		{
			int num = this.CountSkus();
			List<Sku> list = new List<Sku>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetSkuAt(i));
			}
			return list;
		}

		// Token: 0x0400012E RID: 302
		private IntPtr MethodsPtr;

		// Token: 0x0400012F RID: 303
		private object MethodsStructure;

		// Token: 0x0200008D RID: 141
		internal struct FFIEvents
		{
			// Token: 0x040001ED RID: 493
			internal StoreManager.FFIEvents.EntitlementCreateHandler OnEntitlementCreate;

			// Token: 0x040001EE RID: 494
			internal StoreManager.FFIEvents.EntitlementDeleteHandler OnEntitlementDelete;

			// Token: 0x02000149 RID: 329
			// (Invoke) Token: 0x060004E1 RID: 1249
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void EntitlementCreateHandler(IntPtr ptr, ref Entitlement entitlement);

			// Token: 0x0200014A RID: 330
			// (Invoke) Token: 0x060004E5 RID: 1253
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void EntitlementDeleteHandler(IntPtr ptr, ref Entitlement entitlement);
		}

		// Token: 0x0200008E RID: 142
		internal struct FFIMethods
		{
			// Token: 0x040001EF RID: 495
			internal StoreManager.FFIMethods.FetchSkusMethod FetchSkus;

			// Token: 0x040001F0 RID: 496
			internal StoreManager.FFIMethods.CountSkusMethod CountSkus;

			// Token: 0x040001F1 RID: 497
			internal StoreManager.FFIMethods.GetSkuMethod GetSku;

			// Token: 0x040001F2 RID: 498
			internal StoreManager.FFIMethods.GetSkuAtMethod GetSkuAt;

			// Token: 0x040001F3 RID: 499
			internal StoreManager.FFIMethods.FetchEntitlementsMethod FetchEntitlements;

			// Token: 0x040001F4 RID: 500
			internal StoreManager.FFIMethods.CountEntitlementsMethod CountEntitlements;

			// Token: 0x040001F5 RID: 501
			internal StoreManager.FFIMethods.GetEntitlementMethod GetEntitlement;

			// Token: 0x040001F6 RID: 502
			internal StoreManager.FFIMethods.GetEntitlementAtMethod GetEntitlementAt;

			// Token: 0x040001F7 RID: 503
			internal StoreManager.FFIMethods.HasSkuEntitlementMethod HasSkuEntitlement;

			// Token: 0x040001F8 RID: 504
			internal StoreManager.FFIMethods.StartPurchaseMethod StartPurchase;

			// Token: 0x0200014B RID: 331
			// (Invoke) Token: 0x060004E9 RID: 1257
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchSkusCallback(IntPtr ptr, Result result);

			// Token: 0x0200014C RID: 332
			// (Invoke) Token: 0x060004ED RID: 1261
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchSkusMethod(IntPtr methodsPtr, IntPtr callbackData, StoreManager.FFIMethods.FetchSkusCallback callback);

			// Token: 0x0200014D RID: 333
			// (Invoke) Token: 0x060004F1 RID: 1265
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CountSkusMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x0200014E RID: 334
			// (Invoke) Token: 0x060004F5 RID: 1269
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetSkuMethod(IntPtr methodsPtr, long skuId, ref Sku sku);

			// Token: 0x0200014F RID: 335
			// (Invoke) Token: 0x060004F9 RID: 1273
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetSkuAtMethod(IntPtr methodsPtr, int index, ref Sku sku);

			// Token: 0x02000150 RID: 336
			// (Invoke) Token: 0x060004FD RID: 1277
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchEntitlementsCallback(IntPtr ptr, Result result);

			// Token: 0x02000151 RID: 337
			// (Invoke) Token: 0x06000501 RID: 1281
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void FetchEntitlementsMethod(IntPtr methodsPtr, IntPtr callbackData, StoreManager.FFIMethods.FetchEntitlementsCallback callback);

			// Token: 0x02000152 RID: 338
			// (Invoke) Token: 0x06000505 RID: 1285
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void CountEntitlementsMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x02000153 RID: 339
			// (Invoke) Token: 0x06000509 RID: 1289
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetEntitlementMethod(IntPtr methodsPtr, long entitlementId, ref Entitlement entitlement);

			// Token: 0x02000154 RID: 340
			// (Invoke) Token: 0x0600050D RID: 1293
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result GetEntitlementAtMethod(IntPtr methodsPtr, int index, ref Entitlement entitlement);

			// Token: 0x02000155 RID: 341
			// (Invoke) Token: 0x06000511 RID: 1297
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate Result HasSkuEntitlementMethod(IntPtr methodsPtr, long skuId, ref bool hasEntitlement);

			// Token: 0x02000156 RID: 342
			// (Invoke) Token: 0x06000515 RID: 1301
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void StartPurchaseCallback(IntPtr ptr, Result result);

			// Token: 0x02000157 RID: 343
			// (Invoke) Token: 0x06000519 RID: 1305
			[UnmanagedFunctionPointer(CallingConvention.Winapi)]
			internal delegate void StartPurchaseMethod(IntPtr methodsPtr, long skuId, IntPtr callbackData, StoreManager.FFIMethods.StartPurchaseCallback callback);
		}

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x0600020A RID: 522
		public delegate void FetchSkusHandler(Result result);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x0600020E RID: 526
		public delegate void FetchEntitlementsHandler(Result result);

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x06000212 RID: 530
		public delegate void StartPurchaseHandler(Result result);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x06000216 RID: 534
		public delegate void EntitlementCreateHandler(ref Entitlement entitlement);

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x0600021A RID: 538
		public delegate void EntitlementDeleteHandler(ref Entitlement entitlement);
	}
}
