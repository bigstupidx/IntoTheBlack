using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

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
		NoticeManager.Instance.SetNotice("별자리가 나타났습니다. 마구 클릭하여 별을 모아 보세요. \n 별을 모아 무기를 업그레이드 할 수 있습니다.",5f);

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
				NoticeManager.Instance.SetNotice("우주 고래가 나타났습니다. 터치해서 자원을 모으세요.\n", 5);
				SpawnWhale();
				spawnWaitTime = 15f;
			}
			else
			{
				NoticeManager.Instance.SetNotice("외계인이 나타났습니다. 우주 폭풍을 부르기전에 물리치세요.\n", 5);
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
//			SpawnWhale();
//			SpawnAlien();
			SpawnStar();
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