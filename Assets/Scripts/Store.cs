using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Prime31;


public class Store : MonoBehaviour 
{
#if UNITY_ANDROID
	// Use this for initialization
	private bool isConnect;

	public GameObject resultWindow;

	void OnEnable()
	{
		if (isConnect != true)
		{
			Open();
			isConnect = true;
		}
	}

	void OnDisable()
	{
		Close();
		isConnect = false;
	}

	void Open()
	{
		Init();
	//	AudioListener.pause = true;
	}

	void Init()
	{
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		
		var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiAuaxUVegwS7YpWKYta7BluTgyLuh9wMR/ThKaVh8saH7Xl/eK0of3ilILzc/cs9LhW07uQeijQmxcMTiZI8pO5TfHuQCNSUZoK9dXJ1W0AzAj4He+Ur/XNEI3w0XSEUKAxQiFTbZ5x1/IoKH7LnvFWNqTKOmdRVSh45+G11qguABmHiT4PS0YTuUEfij1N4pkzJtNnfvTpwkx9mnCuO1e38659moGihHcwviKlv0W2ddb8te28iv5UxWSR2h9SWqHpjYFfv2Y2V9s1+F0MLb1fwbreAFocqmE+bHhV/oHVLBRv3Qsuwqej9CwRWU6YAwefjxmt0tEeDwHn2DF7gWQIDAQAB";
		GoogleIAB.init( key );

		var skus = new string[] { "one.coin", "nineteen.coin", "three.coin", "five.coin", "nine.coin", "intotheblack.vip", "intotheblack.venus", "intotheblack.shield" };
		GoogleIAB.queryInventory( skus );
	}


	// 구입자의 계정에 소모되지 않은 sku가 있는지
	void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		Debug.Log(string.Format("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
		Prime31.Utils.logObject(purchases);
		Prime31.Utils.logObject(skus);
		
		foreach (GooglePurchase obj in purchases)
		{
			// 저 결재 정보 리스트 안의 놈들을 꺼내서 죄다 컨슘을 불러줌.
			if (obj.productId == "intotheblack.vip")
			{
				Debug.Log ("VIP");
				GameController.isVip = 1;
				PlayerPrefs.SetInt ("isVip",GameController.isVip);
			}
			else if (obj.productId == "intotheblack.venus")
			{
				Debug.Log ("Venus");
				
				GameController.wingMercury = 1;
				GameController.wingVenus = 1;
				PlayerPrefs.SetInt("mercury", GameController.wingMercury);
				PlayerPrefs.SetInt("venus", GameController.wingVenus);
				
				PlayerController.Instance.UpdateWing();
				//script.UpdateWing();
			}
			else if (obj.productId == "intotheblack.shield")
			{
				Debug.Log ("shield");
				
				PlayerController.infinityShield = 1;
				PlayerPrefs.SetInt("infinityShield", PlayerController.infinityShield);
			}
			else
			{
				GoogleIAB.consumeProduct(obj.productId);
			}
		}
		
		// 여기는 첨에 쿼리 인벤토리 날렸을 때 소모 못시킨게 있음 여기로 들어와요.
		// 여기서 소모 안된게 있으면 싹 소모를 시켜줘야해요.
		// 분명 코드를 소모 시키라고 짯는데 무슨 이유에서든 소모 안된게 남아 있는거.
		// 그래서 게임 초기화할 시점에 한번 싹 해주는게 좋아영.
		// 죠기 skus 리스트 안에 소모 안된 놈들이 들어 있을거에요. 죄다 소모 ㄱㄱ
		// 그럼 자동으로 소모 성공 이벤트를 또 탈거고 거기서 자동으로 쭈주죽 함수가 타겠죠!
	}
	
	// 인벤토리 목록 요청 실패
	void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
	}
	
	// 구입 성공 완료
	void purchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		// 구입이 성공하면 불리는 함수.
		// 구입한 항목이 관리되지 않는 제품(캐시보석 같은) 놈이면, 요기서 소모를 시켜줌. 결재 성공했으니까.

		if (purchase.productId == "intotheblack.vip")
		{
			GameController.isVip = 1;
			PlayerPrefs.SetInt ("isVip",GameController.isVip);

			resultWindow.GetComponent<StoreResult>().result = "축하 합니다. VIP 가 되셨습니다.\n 획득 골드가 2 배 증가합니다 \n 앞으로 광고가 보여지지 않습니다.";
			resultWindow.SetActive(true);
		}
		else if (purchase.productId == "intotheblack.venus")
		{

			GameController.wingMercury = 1;
			GameController.wingVenus = 1;
			PlayerPrefs.SetInt("mercury", GameController.wingMercury);
			PlayerPrefs.SetInt("venus", GameController.wingVenus);
			PlayerController.Instance.UpdateWing();

			resultWindow.GetComponent<StoreResult>().result = "수성과 금성 행성을 획득하였습니다. \n 앞으로 어떤 외계인이 나타나도 두렵지 않습니다. ";
			resultWindow.SetActive(true);

			//script.UpdateWing();

		}
		else if (purchase.productId == "intotheblack.shield")
		{
			Debug.Log ("shield");

			PlayerController.infinityShield = 1;
			PlayerPrefs.SetInt("infinityShield", PlayerController.infinityShield);
		}
		else
		{
			GoogleIAB.consumeProduct(purchase.productId);
		}
		
		// 아이템 넣는 처리는 죠 아래 소모 성공 함수가 실행되는 곳에서. void consumePurchaseSucceededEvent()
	}
	
	// 구입 실패
	void purchaseFailedEvent(string error, int response)
	{
		Debug.Log("purchaseFailedEvent: " + error + ", response: " + response);
		// 실패햇을 때 스트링 에러와 리스폰스가 오니 그거 확인해서 실패 처리를 여기다 코딩.
	}
	
	
	// 소모 성공 완료
	void consumePurchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		// 저 위에서 소비를 시켰고 그게 성공하면 불리는 함수구요.
		// 구입도 됐고 소비까지 성공했으니 요기에서 실제 게임내에 보석을 주는 등의 처리를 하면 되영.
		// 자세히 영수증 체크를 하고 싶으면 저 구글퍼체이스 들어가셔서 안에 항목들 보시면 될듯.
		// 근데 클라는 아무 의미 없어요. 클라서 뭘 체크 해봐야 소용이 없...
		
		
		if (purchase.productId == "one.coin")
		{
			GameController.dustPoints += 10000;
			resultWindow.GetComponent<StoreResult>().result = "우주 골드를 사는 당신은 물질 따위에 연연하지 않는 진정한 스페이스 카우보이!!";
			resultWindow.SetActive(true);
		}
		else if (purchase.productId == "three.coin")
		{
			resultWindow.GetComponent<StoreResult>().result = "노동자들을 착취하여 생산되는 일부 커피들과 다르게 이 골드는 100% 개발자의 노력으로 만들어 졌습니다.";
			resultWindow.SetActive(true);
			// 보석 80개 주기 처리
			GameController.dustPoints += 50000;
		}
		else if (purchase.productId == "five.coin")
		{
			resultWindow.GetComponent<StoreResult>().result = "이 기회에 담배도 끊고, 세금도 끊고? 응? 건강도 챙겨보세요.";
			resultWindow.SetActive(true);
			// 보석 180개 주기 처리
			GameController.dustPoints += 100000;
		}
		else if (purchase.productId == "nine.coin")
		{
			resultWindow.GetComponent<StoreResult>().result = "좋아하는 일 하면서 살 수 있게 도와주셔서 감사합니다. 다음에는 더 잘 만들께요 ^^";
			resultWindow.SetActive(true);
			// "에러 관련 메시지 처리 "
			GameController.dustPoints += 250000;
		}

		
	}
	
	// 소모 실패
	void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
	}
	
	// 구매 가능한 앱 버전일 경우 발생하는 이벤트.
	void billingSupportedEvent()
	{
		Debug.Log( "billingSupportedEvent" );
	}
	
	// 구매 불가능할 경우 발생하는 이벤트
	void billingNotSupportedEvent( string error )
	{
		Debug.Log( "billingNotSupportedEvent: " + error );
	}
	
	
	// 구입 성공 후 인증 대기중
	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
		// 요놈은 오토인증을 false로 해놓으면 구매성공했을 때 요기로 들어와여.
		// 요기로 들어오면 여기서 서버에 영수증을 보내고 서버에서 그 영수증을 검증하고 하기 위해서 그래요.
		// 근데 우리는 서버가 없기 때문에 false 설정도 안할거고... 그럼 여길 타지도 않을....
		// 게임 서버가 있다면 다른 이야기.
	}
	
	
	// 여기 아래 두개(온 언에이블, 온디스에이블)는 += 랑 -= 보시면 델리게이트 이벤트 등록하는 놈들이에요.
	// 이벤트를 등록해두면 어떤 이벤트가 발생했을 때 자동으로 그 등록된 함수를 실행시켜주거든요.
	// 그게 델리게이트 이벤트라는 건데 설명하긴 좀 복잡해서 C# 델리게이트로 검색해서 문법을 익히시면 될듯.
	// 요는 저게 등록이 되어 있어야 콜백식으로 함수가 호출되요. 성능이나 이런거에 전혀 영향 없으니 그냥 놔두시면 되요.
//	void OnEnable()
//	{
//		// Listen to all events for illustration purposes
//		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
//		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
//		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
//		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
//		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
//		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
//		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
//		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
//		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
//	}
//	
//	void OnDisable()
//	{
//		// Remove all event handlers
//		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
//		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
//		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
//		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
//		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
//		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
//		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
//		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
//		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
//	}


	public void PurchaseStatic()
	{
		GoogleIAB.purchaseProduct( "android.test.purchased" );

	}

	public void PurchaseReal(string productname)
	{
		SoundManager.Instance.PlaySound(8);
		GoogleIAB.purchaseProduct(productname);
	}

//	public void PurchaseConsume(string productname)
//	{
//		GoogleIAB.consumeProduct(productname);
//	}

	public void PurchaseManaged(string productname)
	{
		SoundManager.Instance.PlaySound(8);
		GoogleIAB.purchaseProduct(productname);
	}


	public void LoadDemo()
	{
		//Application.LoadLevel("IABTestScene");
	}



	void Close()
	{
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;

//		GoogleIAB.unbindService();
//		AudioListener.pause = false;

	}
#endif
}

