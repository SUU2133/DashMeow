using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour , IStoreListener
{
	[SerializeField] Text stateText;
	[SerializeField] GameObject nonConsumableButton;
	private IStoreController storeController;

	private string cannedfood400 = "cannedfood400";
	private string cannedfood800 = "cannedfood800";
	private string cannedfood2000 = "cannedfood2000";
	private string cannedfood4000 = "cannedfood4000";
	private string cannedfood8000 = "cannedfood8000";
	private string cannedfood20000 = "cannedfood20000";

	private string basickpackage = "basickpackage";
	private string nobilitypackage = "nobilitypackage";
	private string dailypackage = "dailypackage";
	private string nerocatpackage = "nerocatpackage";

	private string cannedfood400_400 = "cannedfood400_400";
	private string cannedfood800_800 = "cannedfood800_800";
	private string cannedfood2000_2000 = "cannedfood2000_2000";
	private string cannedfood4000_4000 = "cannedfood4000_4000";
	private string cannedfood8000_8000 = "cannedfood8000_8000";
	private string cannedfood20000_20000 = "cannedfood20000_20000";

	void Start()
    {
		InitIAP();
		nonConsumableButton = this.gameObject;
	}

   private void InitIAP()
	{
		//버튼 활성 / 비활성 시키는 설정

		//앱 내부에서 판매할 아이템들을 정리 및 구성 가능
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(cannedfood400, ProductType.Consumable);
		builder.AddProduct(cannedfood800, ProductType.Consumable);
		builder.AddProduct(cannedfood2000, ProductType.Consumable);
		builder.AddProduct(cannedfood4000, ProductType.Consumable);
		builder.AddProduct(cannedfood8000, ProductType.Consumable);
		builder.AddProduct(cannedfood20000, ProductType.Consumable);

		builder.AddProduct(basickpackage, ProductType.NonConsumable);
		builder.AddProduct(nobilitypackage, ProductType.NonConsumable);
		builder.AddProduct(dailypackage, ProductType.NonConsumable);
		builder.AddProduct(nerocatpackage, ProductType.NonConsumable);



		builder.AddProduct(cannedfood400_400, ProductType.NonConsumable);
		builder.AddProduct(cannedfood800_800, ProductType.NonConsumable);
		builder.AddProduct(cannedfood2000_2000, ProductType.NonConsumable);
		builder.AddProduct(cannedfood4000_4000, ProductType.NonConsumable);
		builder.AddProduct(cannedfood8000_8000, ProductType.NonConsumable);
		builder.AddProduct(cannedfood20000_20000, ProductType.NonConsumable);

		UnityPurchasing.Initialize(this, builder);


	}
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		storeController = controller;
		CheckNonConsumable(cannedfood400_400);
		CheckNonConsumable(cannedfood800_800);
		CheckNonConsumable(cannedfood2000_2000);
		CheckNonConsumable(cannedfood4000_4000);
		CheckNonConsumable(cannedfood8000_8000);
		CheckNonConsumable(cannedfood20000_20000);

		CheckNonConsumable(basickpackage);
		CheckNonConsumable(nobilitypackage);
		CheckNonConsumable(dailypackage);
		CheckNonConsumable(nerocatpackage);
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("IAP 초기화 실패" + error);
	}

	public void OnInitializeFailed(InitializationFailureReason error, string message)
	{
		Debug.Log("IAP 초기화 실패" + error + message);
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log("구매 실패");
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
	{
		var product = purchaseEvent.purchasedProduct;//결제 정보 저장

		Debug.Log("구매 성공 : " + product.definition.id);

		if(product.definition.id== cannedfood400)
		{
			DataManager.Instance.cannedFood += 400;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == cannedfood800)
		{
			DataManager.Instance.cannedFood += 800;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == cannedfood2000)
		{
			DataManager.Instance.cannedFood += 2000;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == cannedfood4000)
		{
			DataManager.Instance.cannedFood += 4000;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == cannedfood8000)
		{
			DataManager.Instance.cannedFood += 8000;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == cannedfood20000)
		{
			DataManager.Instance.cannedFood += 20000;
			DataManager.Instance.catGage++;
			DataManager.Instance.SaveDataToJson();
		}


		else if(product.definition.id == basickpackage)
		{
			DataManager.Instance.silverKey += 10;
			DataManager.Instance.goldKey+=5;
			DataManager.Instance.sushi += 10000;
			DataManager.Instance.brokenBlue += 10;
			DataManager.Instance.brokenGreen += 10;
			DataManager.Instance.brokenRed += 10;
			DataManager.Instance.catGage++;
			nonConsumableButton.SetActive(true);
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == nobilitypackage)
		{
			DataManager.Instance.silverKey += 10;
			DataManager.Instance.goldKey += 10;
			DataManager.Instance.sushi += 30000;
			DataManager.Instance.brokenBlue += 20;
			DataManager.Instance.brokenGreen += 20;
			DataManager.Instance.brokenRed += 20;
			DataManager.Instance.catGage++;
			nonConsumableButton.SetActive(true);
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == dailypackage)
		{
			DataManager.Instance.silverKey += 10;
			DataManager.Instance.sushi += 20000;
			DataManager.Instance.brokenBlue += 3;
			DataManager.Instance.brokenGreen += 3;
			DataManager.Instance.brokenRed += 3;
			DataManager.Instance.catGage++;
			nonConsumableButton.SetActive(true);
			DataManager.Instance.SaveDataToJson();
		}
		else if (product.definition.id == nerocatpackage)
		{
			DataManager.Instance.neroCatSkin++;
			DataManager.Instance.neroCatWallpaper++;
			DataManager.Instance.catGage++;
			nonConsumableButton.SetActive(true);
			DataManager.Instance.SaveDataToJson();
		}


		else if (product.definition.id == cannedfood400_400)
		{
			DataManager.Instance.cannedFood += 800;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(false);
		}
		else if (product.definition.id == cannedfood800_800)
		{
			DataManager.Instance.cannedFood += 1600;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(true);
		}
		else if (product.definition.id == cannedfood2000_2000)
		{
			DataManager.Instance.cannedFood += 4000;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(true);
		}
		else if (product.definition.id == cannedfood4000_4000)
		{
			DataManager.Instance.cannedFood += 8000;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(true);
		}
		else if (product.definition.id == cannedfood8000_8000)
		{
			DataManager.Instance.cannedFood += 16000;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(true);
		}
		else if (product.definition.id == cannedfood20000_20000)
		{
			DataManager.Instance.cannedFood += 40000;
			DataManager.Instance.SaveDataToJson();
			nonConsumableButton.SetActive(true);
		}

		return PurchaseProcessingResult.Complete;

	}

	public void Purchase(string productID)
	{
		//버튼 이벤트 연결
		storeController.InitiatePurchase(productID);
	}

	//구매 영수증 확인
	private void CheckNonConsumable(string id)
	{
		var product = storeController.products.WithID(id);

		if (product != null)
		{
			//해당 제품의 영수증을 가지고 있는지 확인. 있으면 ture
			bool isCheck = product.hasReceipt;

			nonConsumableButton.SetActive(isCheck);
		}
	}

}
