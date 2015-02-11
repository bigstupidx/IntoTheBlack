using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class AdmobManager : MonoBehaviour 
{

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
		}
	}



	IEnumerator BannerLoop()
	{
		yield return new WaitForSeconds(30f);
			banner = new BannerView("ca-app-pub-7143280300027830/7400835709", AdSize.SmartBanner, AdPosition.Top);
			AdRequest request = new AdRequest.Builder().Build();
			
			if (GameController.isVip !=1)
				banner.LoadAd(request);	

		yield return new WaitForSeconds(20f);

				banner.Hide ();
			if (GameController.isVip !=1)
				Advertisement.Initialize (gameId);

		while(GameController.isVip !=1)
		{
			yield return new WaitForSeconds(300f);
				if(Advertisement.isReady() && GameController.isVip !=1)
				{
					Advertisement.Show();
				}
			yield return new WaitForSeconds(90f);
				if (GameController.isVip !=1)
					banner.Show();
			yield return new WaitForSeconds(20f);

				banner.Hide ();
		}
	}
}