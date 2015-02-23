using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using SmartLocalization;

// manage to game goals and event like character's conversation.
// study how to use singletone method, google docs, (doscpin), sturucture (data array) and action and task;
// using sorting by C# reference
//[System.Serializable]

//public struct GoalData
//{
//	public string name;
//	public float distance;
//	public int reward;
//	public string rewardText;
//
//	public GoalData(string _name, float _distance, int _reward, string _rewardText)
//	{
//		name = _name;
//		distance = _distance;
//		reward = _reward;
//		rewardText = _rewardText;
//	}
//}


public class EventManager : MonoBehaviour 
{
	private static EventManager _instance;
//	public GoalData[] m_Goals;

	public GameObject goalWindow;
	public GameObject eventDialog;
//	public GameObject cutSceneManager;

	public GameObject[] planets;

//	private GoalWindow goalWindowScript;
	private EventDialog eventDialogScript;
//	private CutSceneManager cutScript;

	public static int previousGoal;

	public float eventWaitTime;
	// event hmm
	public int eventCount = 0;

	public GameObject autoFightButton;

	private string rewardString;

	public static EventManager Instance()
	{
		if (_instance = null)
		{
			_instance = new EventManager(); // if it needs to work in multithreads, need to call lock();
		}

		return _instance;
    }


	// Use this for initialization
	void Awake () 
	{

		previousGoal = PlayerPrefs.GetInt("previousGoal");
		if (previousGoal == 0)
			previousGoal = -1;

		GameController.nextGoalName = DialogManager.m_Goals[0].name;
		GameController.nextGoalDistance = DialogManager.m_Goals[0].distance;

		eventCount = PlayerPrefs.GetInt("eventCount");


		Array.Reverse(DialogManager.m_Goals);

		goalWindow.SetActive(false);

		if (previousGoal < 3)
			autoFightButton.SetActive(false);

		LanguageManager thisLanguageManager = LanguageManager.Instance;
		rewardString = thisLanguageManager.GetTextValue("UI.Reward");

		StartCoroutine (EventLoop());
		 		
	}


	IEnumerator EventLoop()
	{
		yield return new WaitForSeconds(120);

		while(true)
		{
			if(DialogManager.isDialogPop == false)
			{
				CheckEvent();
			}

			yield return new WaitForSeconds(eventWaitTime);

		}
		
	}

	public void CheckEvent()
	{

		for (int i = 0; i < DialogManager.m_Dialogs.Length; i++)
		{
			if(eventCount > DialogManager.m_Dialogs.Length)
			{
				eventCount = 0;
			}

			if (DialogManager.m_Dialogs[i].id == eventCount)
			{

				DialogManager.Instance.SetDialog(DialogManager.m_Dialogs[i].pilot, DialogManager.m_Dialogs[i].talk);
				eventCount += 1;

				PlayerPrefs.SetInt("eventCount",eventCount);
				PlayerPrefs.SetInt("previousGoal",previousGoal);

				break;
			}
		}
	}


		
	void Update()
	{
		
		CheckGoal();
	}


	public void CheckGoal()
	{
		// currentDistance > nextGoal && goal distance from structure
		for (int i = 0; i < DialogManager.m_Goals.Length;  i++)
		{

			if (GameController.distanceFromEarth > DialogManager.m_Goals[i].distance && GameController.distanceFromEarth > GameController.nextGoalDistance)
			{
					// check saved last Goal and skip last goals
				if (previousGoal != DialogManager.m_Goals[i].id)
				{

					StartCoroutine(SetWindow(DialogManager.m_Goals[i].id, DialogManager.m_Goals[i].name, DialogManager.m_Goals[i].distance, DialogManager.m_Goals[i].reward, DialogManager.m_Goals[i].rewardText, DialogManager.m_Goals[i].reward));

					PlayerPrefs.SetInt("previousGoal", DialogManager.m_Goals[i].id);
					previousGoal = DialogManager.m_Goals[i].id;

					IngameReward(DialogManager.m_Goals[i].id);

				}

				if (i != 0)
				{
					GameController.nextGoalName = DialogManager.m_Goals[i-1].name;
					GameController.nextGoalDistance = DialogManager.m_Goals[i-1].distance;
					GameController.distanceToNext = DialogManager.m_Goals[i-1].distance - DialogManager.m_Goals[i].distance;
					PlayerPrefs.SetFloat("distanceToNext", GameController.distanceToNext);

				}
				else 
				{
					GameController.nextGoalDistance = float.MaxValue; // player max distance (current distance > max distance in structure;
				}
				
				break;
			}
		}
	}

	IEnumerator SetWindow(int _id, string _title, float _distance, int _rewardCoin, string _rewardDesc, int _reward)
	{
		GoalWindow window = goalWindow.GetComponent<GoalWindow>();



		window.goalTitle.text = _title;
		window.goalDistance.text = string.Concat(_distance.ToString("N0"), " Km");
		window.reward.text = string.Format("{0}: {1}", rewardString, _rewardCoin.ToString("N0"));
		window.rewardText.text = _rewardDesc;

	
		// resource load as Async
		ResourceRequest request = Resources.LoadAsync("Reward/" + _id, typeof(Sprite));
		yield return request;

		window.rewardImage.sprite = request.asset as Sprite;

		int coinNum = (int) (_reward*0.1);
		Vector3 coinPos = new Vector3 (0.0f, 0.0f, 12.0f);
		SpawnManager.Instance.PopCoins(coinNum,coinPos);

		goalWindow.SetActive(true);
	}

	public void CloseGoalWindow()
	{
		goalWindow.SetActive(false);
	}

	public void IngameReward(int _id)
	{

		switch(_id)
		{
			// Earth
		case 0: 

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQBA", 100.0f, (bool success) => 
				                      {
					// handle success or failure
				});
			}

			GameController.wingEarth = 1;
			PlayerPrefs.SetInt("earth", 1);

			SoundManager.Instance.PlaySound(9);
			NoticeManager.Instance.SetNotice("You have got Earth. New skill is unlocked.\n Touch Earth skill then you can get golds near space ship",5f);

			Instantiate(planets[0]);

			PlayerController.Instance.UpdateWing();
			SpawnManager.Instance.CaculateSpawnProb();

			break;

			// ISS
		case 1:
			Instantiate(planets[1]);

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQAQ", 100.0f, (bool success) => 
				                      {
					// handle success or failure
				});
			}

			SoundManager.Instance.PlaySound(9);

//			UpgradeManager.unlockEngine = 20;
//			PlayerPrefs.SetInt("unlockEngine",UpgradeManager.unlockEngine);	
//			
			NoticeManager.Instance.SetNotice("You discovered ISS, Keep touching it to get golds",5f);
			break;

			// Huble
		case 2:

			Instantiate(planets[2]);

			if(GameController.isGoogle)
			{
					Social.ReportProgress("CgkIyvPM5r0LEAIQAg", 100.0f, (bool success) => 
					                      {
						// handle success or failure
					});
			}

			SoundManager.Instance.PlaySound(9);

			UpgradeManager.unlockTime = 5;
			PlayerPrefs.SetInt("unlockTime",UpgradeManager.unlockTime);	

			GameController.timeLevel = 5;
			NoticeManager.Instance.SetNotice("you discoverd hubble telescope, start to accel 5 times ",5f);
			GameController.Instance.SavePlayerData();
			SpawnManager.Instance.CaculateSpawnProb();
			break;

			// Moon
		case 3:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQAw", 100.0f, (bool success) => 
					                      {
						// handle success or failure
				});
			}


			SoundManager.Instance.PlaySound(9);
			GameController.wingMoon = 1;
			PlayerPrefs.SetInt("moon", 1);
			PlayerController.Instance.UpdateWing();

			GameController.timeLevel +=5;
			NoticeManager.Instance.SetNotice("You have got the Moon. 10 times faster than normal time speed \n You can upgrade time accelation from now. ",5f);
			UpgradeManager.unlockTime = 301;
			PlayerPrefs.SetInt("unlockTime",UpgradeManager.unlockTime);

			autoFightButton.SetActive(true);



			Instantiate(planets[3]);

			SpawnManager.Instance.CaculateSpawnProb();
			break;
		case 4:
			Instantiate(planets[4]);
			break;
		case 5:
			Instantiate(planets[5]);
			break;
			// Mars
		case 6:

			if(GameController.isGoogle)
			{
					Social.ReportProgress("CgkIyvPM5r0LEAIQBQ", 100.0f, (bool success) => 
					                      {
						// handle success or failure
					});
			}

			SoundManager.Instance.PlaySound(9);
			GameController.wingMars = 1;
			PlayerPrefs.SetInt("Mars", 1);
			PlayerController.Instance.UpdateWing();

			NoticeManager.Instance.SetNotice("You have got Mars. New skill is unlocked",5f);
//			UpgradeManager.unlockEngine = 101;
//			PlayerPrefs.SetInt("unlockEngine",UpgradeManager.unlockEngine);
			Instantiate(planets[6]);

			SpawnManager.Instance.CaculateSpawnProb();

			break;
		case 7:
			Instantiate(planets[7]);
			break;
		case 8:
			Instantiate(planets[8]);
			break;
		case 9:
			Instantiate(planets[9]);
			break;
		case 10:
			Instantiate(planets[10]);
			break;

			// Jupiter
		case 11:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQCA", 100.0f, (bool success) => 
				                      {
					// handle success or failure
				});
			}


			SoundManager.Instance.PlaySound(9);
			GameController.wingJupiter = 1;
			PlayerPrefs.SetInt("jupiter", 1);
			NoticeManager.Instance.SetNotice("You have got Jupiter",5f);
			PlayerController.Instance.UpdateWing();
			Instantiate(planets[11]);

			break;

		case 12:
			Instantiate(planets[12]);
			break;
		case 13:
			Instantiate(planets[13]);
			break;
		case 14:
			Instantiate(planets[14]);
			break;
		case 15:
			Instantiate(planets[15]);
			break;
		case 16:
			Instantiate(planets[16]);
			break;
		case 17:
			Instantiate(planets[17]);
			break;
		case 18:
			Instantiate(planets[18]);
			break;
			// Saturn
		case 19:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQCQ", 100.0f, (bool success) => 
					                      {
						// handle success or failure
				});
			}

			SoundManager.Instance.PlaySound(9);
			GameController.wingSaturn = 1;
			PlayerPrefs.SetInt("saturn", 1);
			PlayerController.Instance.UpdateWing();

			NoticeManager.Instance.SetNotice("You have got Saturn",5f);
			Instantiate(planets[19]);

			break;

		case 20:
			Instantiate(planets[20]);
			break;
		case 21:
			Instantiate(planets[21]);
			break;
		case 22:
			Instantiate(planets[22]);
			break;
		case 23:
			Instantiate(planets[23]);
			break;
		case 24:
			Instantiate(planets[24]);
			break;
		case 25:
			Instantiate(planets[25]);
			break;

			// Urunus
		case 26:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQCg", 100.0f, (bool success) => 
					                      {
						// handle success or failure
					});
			}

			SoundManager.Instance.PlaySound(9);
			GameController.wingUranus = 1;
			PlayerPrefs.SetInt("uranus", 1);
			PlayerController.Instance.UpdateWing();
			
			NoticeManager.Instance.SetNotice("You have got Uranus",5f);
			Instantiate(planets[26]);

			break;

		case 27:
			Instantiate(planets[27]);
			break;
		case 28:
			Instantiate(planets[28]);
			break;
			// Neptune
		case 29:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQCw", 100.0f, (bool success) => 
					                      {
						// handle success or failure
				});
			}

			SoundManager.Instance.PlaySound(9);
			GameController.wingNeptune = 1;
			PlayerPrefs.SetInt("neptune", 1);
			PlayerController.Instance.UpdateWing();
			NoticeManager.Instance.SetNotice("You have got Neptune",5f);
			Instantiate(planets[29]);

			break;

		case 30:
			SoundManager.Instance.PlaySound(9);
			Instantiate(planets[30]);

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQDA", 100.0f, (bool success) => 
					                      {
						// handle success or failure
				});
			}

			break;
			// Helioseath
		case 31:

			if(GameController.isGoogle)
			{
				Social.ReportProgress("CgkIyvPM5r0LEAIQDQ", 100.0f, (bool success) => 
					                      {
						// handle success or failure
				});
			}

			SoundManager.Instance.PlaySound(9);
			Application.LoadLevel("Ending");
			break;

		default:
		//	Debug.Log ("Passed : " + _id);
			break;
		}
	}
}
