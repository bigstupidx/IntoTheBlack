using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class AdmobManager : MonoBehaviour 
{
	private static AdmobManager _instance;
	
	public static AdmobManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(AdmobManager)) as AdmobManager;
				if(!_instance)
				{
					//					GameObject container = new GameObject();
					//					container.name = "GameControllerContainer";
					//					_instance = container.AddComponent(typeof(GameController)) as GameController;
				}
			}
			
			return _instance;
		}
	}


#if UNITY_EDITOR
	string gameId = @"131624534";
#elif UNITY_ANDROID
	string gameId = @"131624534";
#elif UNITY_IOS
	string gameId = @"131624541";
#else
	string gameId = @"131624534";
#endif



//	bool adsShowing = false;
//	bool showedStartAd = false;

	private BannerView banner;
	private AdRequest request;

	void Start () 
	{

		// it is test device code. before release have to delete it
//		request = new AdRequest.Builder().AddTestDevice("D8753DFC817BC813CF3690B864D54E1D").Build ();



		if(GameController.isVip !=1)
		{
			StartCoroutine(BannerLoop());
			Advertisement.Initialize (gameId);
		}
	}



	IEnumerator BannerLoop()
	{
		yield return new WaitForSeconds(20f);
			banner = new BannerView("ca-app-pub-7143280300027830/7400835709", AdSize.SmartBanner, AdPosition.Top);
			AdRequest request = new AdRequest.Builder().Build();
			
			if (GameController.isVip !=1)
				banner.LoadAd(request);	

		yield return new WaitForSeconds(20f);

				banner.Hide ();

		while(GameController.isVip !=1)
		{
			yield return new WaitForSeconds(90f);
				if (GameController.isVip !=1)
					banner.Show();
			yield return new WaitForSeconds(20f);
				banner.Hide ();
		}
	}

	public void ShowBanner()
	{
		if(Advertisement.isReady() && GameController.isVip !=1)
		{
			Advertisement.Show();
		}
	}
}