using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using SmartLocalization;

public class GameController : MonoBehaviour 
{

	private static GameController _instance;
	
	public static GameController Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
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

//	public static bool googleLogin;
//	public static bool isGoogleLogin;
	public Sprite testSprite;

	public static float distanceFromEarth;
	public static string nextGoalName;
	public static float nextGoalDistance;

	public static float journeyTime;
	public static float distanceToStar;
	public static int dustPoints;
	public static int starPoints;
	public static int legendStars;
	public static int heroStars;

	public static int timeAccelation;
//	public static int[] timeLevelValues;
//
//	public static float[] engineLevelValues;
	public static float engineSpeed;
	public static float boosterSpeed = 0;

	public static bool isSwingByCharged;
	public static bool ingSwing;
	public static int swingByLevel;
	public static float swingByTime;
	public static float swingBySpeed;

	public static int engineLevel;
	public static int timeLevel;
	public static int shotLevel;

	public static float[] swingByValues;
	private float swingTimeCheck;

	public GameObject swingButton;

	public static int isVip;

	public static int wingMercury;
	public static int wingVenus;
	public static int wingEarth;
	public static int wingMoon;
	public static int wingMars;
	public static int wingJupiter;
	public static int wingSaturn;
	public static int wingUranus;
	public static int wingNeptune;

	public static float distanceToNext;

	public AudioClip[] bgmClip;
	private float audioCheck;
	private int nextBgm;

	public static int isBgm;
	public static int isEffectSound;
	public static float bgmVolume;
	public static float effectSoundVolume;

	public GameObject starShotPanel;

	public static int fireLevel;
	public GameObject fireShip1;
	public GameObject fireShip2;
	public GameObject fireShip3;

	public GameObject waterFall;

	public static int maxShotLevel;
	public static int maxEngineLevel;
	public static int maxTimeLevel;

	public static int extinguisherNumber;

	public static float scrollModifier;
	public static bool isGoogle;
	public static int uploadFacebook;

	public static int colonyProgress;

	private float fireModifier = 1;

	public Image autoButtonImage;
	public static bool autoFight = false;

	DateTime currentDate;
	DateTime oldDate;



	//public AudioClip

	void Awake()
	{
		Application.targetFrameRate = 30; // have to vSync Off, and try to save battery and prevent device heating

		waterFall.SetActive(false);

		isSwingByCharged = false;
		ingSwing = false;
		// never go in sleepmode
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if UNITY_EDITOR
		Application.runInBackground = true; // it is not work in android 
#endif

		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		
		Social.localUser.Authenticate((bool success) =>	
		{
			if(success)
			{
				isGoogle = true;
			}
			else
			{
				isGoogle = false;
			}
		});

		if (!FB.IsInitialized)
		{
			CallFBInit();
//			status = "FB.Init() called with " + FB.AppId;
		}
		
#if UNITY_EDITOR
//		PlayerPrefs.DeleteAll();
#endif

//		distanceFromEarth = 0.0f; //6378 earth radius
		nextGoalDistance = 0.0f;

		isVip = PlayerPrefs.GetInt("isVip");
		if (isVip < 0) {isVip = 0;}

		dustPoints = PlayerPrefs.GetInt("dustPoints");
		starPoints = PlayerPrefs.GetInt("starPoints");
		legendStars = PlayerPrefs.GetInt("legendStars");
		heroStars = PlayerPrefs.GetInt("heroStars");
		uploadFacebook = PlayerPrefs.GetInt ("uploadFacebook");

		colonyProgress = PlayerPrefs.GetInt("colonyProgress");

		journeyTime = PlayerPrefs.GetFloat("journeyTime");
		if (journeyTime < 0 ) {journeyTime = 0; }

		distanceFromEarth = PlayerPrefs.GetFloat("distanceFromEarth");
		if (distanceFromEarth < 1) { distanceFromEarth = -500f; }

		distanceToNext = PlayerPrefs.GetFloat ("distanceToNext");
		if (distanceToNext < 1) { distanceToNext = 500f; }

		shotLevel = PlayerPrefs.GetInt("shotLevel");
		if (shotLevel < 1) { shotLevel = 1; }

		engineLevel = PlayerPrefs.GetInt("engineLevel");
		if (engineLevel < 1) { engineLevel = 1; }

		timeLevel = PlayerPrefs.GetInt("timeLevel");
		if (timeLevel < 1)	{ timeLevel = 1; }


		isBgm = PlayerPrefs.GetInt ("isBgm");

		isEffectSound = PlayerPrefs.GetInt ("isEffectSound");

		ingSwing = false;

		swingByValues = new float[3] {30.0f, 60.0f, 100.0f};

		timeAccelation = timeLevel;
		engineSpeed = 35640 + (engineLevel * 10000);

		wingMercury = PlayerPrefs.GetInt("mercury");
		wingVenus = PlayerPrefs.GetInt("venus");
		wingEarth = PlayerPrefs.GetInt("earth");
		wingMoon = PlayerPrefs.GetInt("moon");
		wingMars = PlayerPrefs.GetInt("mars");
		wingJupiter = PlayerPrefs.GetInt("jupiter");
		wingSaturn = PlayerPrefs.GetInt("saturn");
		wingUranus = PlayerPrefs.GetInt("uranus");
		wingNeptune = PlayerPrefs.GetInt("neptune");

		maxShotLevel = 30;
		maxEngineLevel = 100;
		maxTimeLevel = 300;

		extinguisherNumber = 5;

		if (bgmClip != null)
		{
			audioCheck = bgmClip[0].length; 
			nextBgm = 1;

		}

		scrollModifier = 1f;

		currentDate = System.DateTime.Now;

		if (PlayerPrefs.GetString("lastPlayTime") !="")
		{
			long temp = Convert.ToInt64(PlayerPrefs.GetString ("lastPlayTime"));
			oldDate = DateTime.FromBinary(temp);
		}
		else
		{
			oldDate = System.DateTime.Now;
		}

		TimeSpan difference = currentDate.Subtract(oldDate);

		journeyTime += difference.Seconds;

		distanceFromEarth += (float)((35640 + (engineLevel * 10000)) * 0.000277777 * difference.Seconds);


#if UNITY_EDITOR
		legendStars = 2000;
		heroStars = 3000;
		starPoints = 5000;
		dustPoints = 10000000;
#endif

	}

	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}

	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);

	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}


	public void SwingByCharge()
	{
		swingByTime += 45f;
		if (!ingSwing)
		{
			isSwingByCharged = true;
			Button btn = swingButton.GetComponent<Button>();
			btn.interactable = true;
		}
	}

	public void SwingBy()
	{

		boosterSpeed = 1000000f;
		RefreshShip();

		Button btn = swingButton.GetComponent<Button>();
		btn.interactable = false;

		PlayerController.rotationValue += 50f;

		isSwingByCharged = false;
		// to background scroll effect and update button Text
		ingSwing = true;

		SoundManager.Instance.PlaySound(14);

		StartCoroutine(BoosterScroll(true));

	}
	
	public void StopSwingBy()
	{

		boosterSpeed = 0;

		RefreshShip();
		GameController.swingByLevel = 1;
		PlayerController.Instance.RefreshRotationValue();

		swingBySpeed = 0;
		swingByLevel = 0;
		swingByTime = 0;
		ingSwing = false;

		StartCoroutine(BoosterScroll(false));

	}

 	IEnumerator BoosterScroll(bool start)
	{

		if (start)
		{
			for (int i=0; i < 15; i++)
			{
				scrollModifier += i;
				yield return new WaitForSeconds(0.05f);
			}
		}
		else
		{
			for (int i=15; i > 0; i--)
			{
				scrollModifier -= i;
				yield return new WaitForSeconds(0.05f);
				if (scrollModifier < 0)
					scrollModifier = 0;
			}
			scrollModifier = 0;
		}
	}


	public void ToggleAuto()
	{
		if (autoFight)
		{
			autoButtonImage.color = Color.white;
			autoFight = false;
		}
		else
		{
			autoButtonImage.color = Color.red;
			autoFight = true;
		}

	}

	void Update () 
	{

		journeyTime += timeLevel * Time.deltaTime;
		distanceFromEarth += engineSpeed * 0.000277777F * timeLevel * Time.deltaTime; // km/h -> km/sec 

//		if ( Application.platform == RuntimePlatform.Android )
//		{
//			if( Input.GetKeyDown( KeyCode.Escape ))
//			{ 
//				Application.Quit ();
//			}
//		}


		if (ingSwing)
		{
			swingByTime -= Time.deltaTime;

			if(swingByTime <= 0 )
				StopSwingBy();
		}



		if(autoFight && fireLevel > 0 && extinguisherNumber > 0)
		{   
			fireLevel = 0;
			Extinguisher();
		}


		#if UNITY_EDITOR
		if(!isSwingByCharged && !ingSwing)
			GameController.Instance.SwingByCharge();

		#endif

		BgmChanger();

	}

	void BgmChanger()
	{
		if (isBgm == 0)
		{
			audioCheck -= Time.deltaTime;

			if (audioCheck < 0)
			{
				audio.clip = bgmClip[nextBgm];
				audio.Play ();
				audioCheck = bgmClip[nextBgm].length;

				if (nextBgm < bgmClip.Length-1)
				{
					nextBgm += 1;
				}
				else
				{
					nextBgm = 0;
				}
			}
		}
		else
		{
			audio.Stop ();
			audioCheck = 0;
		}
	}

	public void StarPhoto(int _id)
	{
		starShotPanel.GetComponent<StarShotPanel>().SetPhoto(_id);
		starShotPanel.SetActive(true);
	}



	void OnApplicationQuit()
	{
		// save data and send highscore here
		SavePlayerData();

	}




	public void SavePlayerData()
	{
		PlayerPrefs.SetInt("dustPoints", dustPoints);
		PlayerPrefs.SetInt("starPoints", starPoints);
		PlayerPrefs.SetInt("legendStars", legendStars);
		PlayerPrefs.SetInt("heroStars", heroStars);
		PlayerPrefs.SetFloat("distanceFromEarth", distanceFromEarth);
		PlayerPrefs.SetFloat("journeyTime", journeyTime);
		PlayerPrefs.SetInt("shotLevel", shotLevel);
		PlayerPrefs.SetInt("engineLevel", engineLevel);
		PlayerPrefs.SetInt("timeLevel", timeLevel);
		PlayerPrefs.SetInt("mercury", wingMercury);
		PlayerPrefs.SetInt("venus", wingVenus);
		PlayerPrefs.SetInt("earth", wingEarth);
		PlayerPrefs.SetInt("moon", wingMoon);
		PlayerPrefs.SetInt("mars", wingMars);
		PlayerPrefs.SetInt("jupiter", wingJupiter);
		PlayerPrefs.SetInt("saturn", wingSaturn);
		PlayerPrefs.SetInt("uranus", wingUranus);
		PlayerPrefs.SetInt("neptune", wingNeptune);
		PlayerPrefs.SetFloat("distanceToNext", distanceToNext);
		PlayerPrefs.SetInt("unlockShot",UpgradeManager.unlockShot);
		PlayerPrefs.SetInt("unlockEngine",UpgradeManager.unlockEngine);
		PlayerPrefs.SetInt("unlockTime",UpgradeManager.unlockTime);
		PlayerPrefs.SetInt("uploadFacebook",uploadFacebook);
		PlayerPrefs.SetInt("colonyProgress",colonyProgress);

		PlayerPrefs.SetString("lastPlayTime", System.DateTime.Now.ToBinary().ToString());

		if(isGoogle)
		{
			Social.localUser.Authenticate((bool logined) =>
	        {
				if(logined)
				{
					long leaderboard = Convert.ToInt64(distanceFromEarth);
					Social.ReportScore(leaderboard, "CgkIyvPM5r0LEAIQBg", (bool success) =>
	           		{ 
						// leaderboard uploaded
					});
				}
				else
				{
					isGoogle = false;
				}
			});
		}
	}

	public void SlowMotion()
	{
		if(fireLevel == 0)
		{	
			fireShip1.SetActive(true);
			fireLevel = 1;
			fireModifier = 0.9f;
			RefreshShip();
			PlayerController.rotationValue = -10f;
		}
		else if(fireLevel == 1)
		{
			fireShip2.SetActive(true);
			fireLevel = 2;
			fireModifier = 0.8f;
			RefreshShip();
			PlayerController.rotationValue = -5f;
		}
		else if(fireLevel == 2)
		{
			fireShip3.SetActive(true);
			fireLevel = 3;
			fireModifier = 0.7f;
			RefreshShip();
			PlayerController.rotationValue = -3f;
		}
	}

	public void RefreshShip() // recaculate from engine, time level data
	{
		GameController.timeAccelation = GameController.timeLevel;
		GameController.engineSpeed = (35640 + (GameController.engineLevel * 10000) + boosterSpeed) * fireModifier; 
	}

	public void Extinguisher()
	{
		if(extinguisherNumber > 0)
		{
			SoundManager.Instance.PlaySound(13);
			waterFall.SetActive(true);
			extinguisherNumber -=1;
			Invoke("RecoveryShip", 5);
		}
	}

	public void RecoveryShip()
	{
		fireShip1.SetActive(false);
		fireShip2.SetActive(false);
		fireShip3.SetActive(false);

		waterFall.SetActive(false);
		fireLevel = 0;
		fireModifier =1;
		PlayerController.Instance.RefreshRotationValue();
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("fireOff"), 5);

		RefreshShip();

	}
}

	

