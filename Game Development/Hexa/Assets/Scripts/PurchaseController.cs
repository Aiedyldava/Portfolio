using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseController : BaseController, IStoreListener
{
	[HideInInspector]
	public static string ProductRemoveAd = "hexa1010.product.remove.ads";

	[HideInInspector]
	public static string PackageCoin1 = "hexa1010.package.coin.1";

	[HideInInspector]
	public static string PackageCoin2 = "hexa1010.package.coin.2";

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	private bool m_PurchaseInProgress;

	private static Action<bool> __f__am_cache0;

	private void Start()
	{
		if (PurchaseController.m_StoreController == null)
		{
			this.InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (this.IsInitialized())
		{
			return;
		}
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		configurationBuilder.AddProduct(PurchaseController.PackageCoin1, ProductType.Consumable);
		configurationBuilder.AddProduct(PurchaseController.PackageCoin2, ProductType.Consumable);
		configurationBuilder.AddProduct(PurchaseController.ProductRemoveAd, ProductType.NonConsumable);
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	public void BuyProductID(string productId)
	{
		if (this.IsInitialized())
		{
			Product product = PurchaseController.m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				PurchaseController.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	//public void RestorePurchases()
	//{
	//	if (!this.IsInitialized())
	//	{
	//		UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
	//		return;
	//	}
	//	if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
	//	{
	//		UnityEngine.Debug.Log("RestorePurchases started ...");
	//		IAppleExtensions extension = PurchaseController.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
	//		extension.RestoreTransactions(delegate(bool result)
	//		{
	//			UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
	//		});
	//	}
	//	else
	//	{
	//		UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
	//	}
	//}

	private bool IsInitialized()
	{
		return PurchaseController.m_StoreController != null && PurchaseController.m_StoreExtensionProvider != null;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		GameController.DialogManager.PopupShop.GoLoad.SetActive(false);
		GameController.DialogManager.PopupShop.Hide();
		if (string.Equals(args.purchasedProduct.definition.id, PurchaseController.PackageCoin1, StringComparison.Ordinal))
		{
			GameController.DialogManager.PopupPurchaseResult.ShowSuccessCoin(1000);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, PurchaseController.PackageCoin2, StringComparison.Ordinal))
		{
			GameController.DialogManager.PopupPurchaseResult.ShowSuccessCoin(5000);
		}
		else if (string.Equals(args.purchasedProduct.definition.id, PurchaseController.ProductRemoveAd, StringComparison.Ordinal))
		{
			GameController.DialogManager.PopupPurchaseResult.ShowSuccessRemoveAds();
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		GameController.DialogManager.PopupShop.GoLoad.SetActive(false);
		GameController.DialogManager.PopupPurchaseResult.ShowFail();
		UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		UnityEngine.Debug.Log("OnInitialized: PASS");
		PurchaseController.m_StoreController = controller;
		PurchaseController.m_StoreExtensionProvider = extensions;
	}
}
