using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using SmartLocalization;

public class Encounter : MonoBehaviour 
{

	private static Encounter _instance;
	
	public static Encounter Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(Encounter)) as Encounter;
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

	public GameObject whaleObject;
	public GameObject alien;
//	public Whale whaleScript;
	public Vector3 spawnPos;
	public float startWaitTime;
	public float spawnWaitTime;

	void Start()
	{
		spawnPos = new Vector3 (0.0f, -50.0f, 7.5f);

		StartCoroutine(EncounterLoop());
	}



	IEnumerator EncounterLoop()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;

		if (EventManager.previousGoal > 1)
		{
			startWaitTime = 23f;
		}
		else
		{
			startWaitTime = 120f;
		}

		yield return new WaitForSeconds(startWaitTime);

		SpawnStar();
		NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("Star.Pop"),5f);

		yield return new WaitForSeconds(23f);
		
		while(true)
		{
			if (Random.Range (0,5) > 0) 
		    {
				SpawnStar();
				spawnWaitTime = 20f;
			}
			else if (Random.Range (0,3) > 1 ) 
			{
				NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("Whale.Pop"), 5);
				SpawnWhale();
				spawnWaitTime = 15f;
			}
			else
			{
				NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("Alien.Pop"), 5);
				SpawnAlien();
				spawnWaitTime = 30f;
			}
			yield return new WaitForSeconds(spawnWaitTime);//	yield return new WaitForSeconds(173);

			GameController.Instance.SavePlayerData();
		}
	}

	void SpawnAlien()
	{
		alien.SetActive(true);
	}

	float spawnTimer;
	void Update()
	{

#if UNITY_EDITOR
		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0)
		{
			SpawnWhale();
			SpawnAlien();
//			SpawnStar();
			spawnTimer = 10f;
		}
#endif

	}

	public void SpawnWhale()
	{

		whaleObject.SetActive(true);
//		whaleObject.transform.position = whalePos;


	}

	public void SpawnStar()
	{		

		int dice;

		if (Random.Range(0,10) > 7) // 20% to random all album
		{
			dice = Random.Range(0, StarLoader.unlockLevel * 10);
		}
		else
		{
			dice = (StarLoader.unlockLevel-1) * 10 + Random.Range(0,10);
		}

		// exception for over array 
		if (dice >= StarLoader.starAlbum.Length)
		{
			dice = Random.Range (0, StarLoader.starAlbum.Length);
		}

		// exception for bad luck user;
		if (StarLoader.sessionAlbumCount > 10)
		{
			for (int i = (StarLoader.unlockLevel-1) * 10; i < (StarLoader.unlockLevel-1) * 10 + 10; i++)
			{
				if (i < StarLoader.starAlbum.Length)
				{
					if (StarLoader.starAlbum[i].isCommon == false)
					{
						dice = i;
						StarLoader.sessionAlbumCount = 0;
						break;
					}
				}
			}
		}


		int id = StarLoader.stars[dice].id;
		int level = StarLoader.stars[dice].level;
		string name = StarLoader.stars[dice].name;

		int type;

		if (StarLoader.starAlbum[dice].isEpic)
		{
			type = Random.Range(1,4);
		}
		else if (StarLoader.starAlbum[dice].isCommon)
		{
			type = Random.Range (1,3);
		}
		else
		{
			type = 1;
		}

		// ignore old level replace to unlocklevel;
		Transform star = PoolManager.Pools["Pool"].Spawn("StarSprite");

		if(StarLoader.starAlbum[dice].isCommon == false)
		{
			star.GetComponent<StarSprite>().SetSprite(id, level, name, type, StarLoader.stars[dice].sprite, true);
		}
		else
		{
			star.GetComponent<StarSprite>().SetSprite(id, level, name, type, StarLoader.stars[dice].sprite, false);
		}

	}

}