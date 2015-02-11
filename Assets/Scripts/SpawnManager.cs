using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class SpawnManager : MonoBehaviour 
{
	private static SpawnManager _instance;
	public static SpawnManager Instance
	{
		get
		{
			if(_instance == null)
			{
				// _instance = GameObject.FindObjectOfType(typeof(SpawnController)) as SpawnController;
				
				_instance = GameObject.FindObjectOfType<SpawnManager>();
				
				if(!_instance)
				{
	//					GameObject container = new GameObject();
	//					container.name = "SpawnControllerContainer";
	//					_instance = container.AddComponent(typeof(SpawnController)) as SpawnController;
				}
			}
			
			return _instance;
		}
	}
	
	public GameObject spaceStorm;
	public GameObject fireWork;

	public Vector3 leftPos;
	public Vector3 rightPos;

	public float xLeft, xRight;
	public float zUp, zDown;
	
	public float hazardSpeedMin;
	public float hazardSpeedMax;
	
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int hazardCount;

	private Transform spawner;

	int[] spawnTable;
	int[] spawnTableEarth;
	int[] spawnTableMoon;
	int[] spawnTableMars;

	float[] spawnProb;
	float[] spawnProbTable;

	private int type;
	private float xPos;
	
	private float hazardSpeed;
	private Vector3 spawnSpeed;
	private Vector3 spawnPos;

	public ParticleSystem dustPrefab;
	public static bool isWave;


	void Start ()
	{

		spawnProb = new float[4] {0, 0, 0, 0.1f}; // can, bigbarrel, barrel, bigAsteroid
		spawnProbTable = new float[4];

		CaculateSpawnProb();

//		PopCoins(100000,Vector3.zero);
//		SpawnStarIcons(1,1000,Vector3.zero);
//		SpawnStarIcons(2,1000,Vector3.zero);
//		SpawnStarIcons(3,1000,Vector3.zero);

		StartCoroutine (SpawnWaves());

	}

	public void CaculateSpawnProb()
	{
		if(EventManager.previousGoal > 5)
		{
			spawnProb[0] = 0.03f;
			spawnProb[1] = 0.02f;
			spawnProb[2] = 0.05f;

			hazardCount = 50;
			waveWait = 5f;
			spawnWait = 0.2f;

			hazardSpeedMin = 3.5f;
			hazardSpeedMax = 4.5f;


		}
		else if(EventManager.previousGoal > 2)
		{
			spawnProb[0] = 0.03f;
			spawnProb[1] = 0.025f;
			spawnProb[2] = 0.06f;

			hazardCount = 30;
			waveWait = 10f;
			spawnWait = 0.3f;

			hazardSpeedMin = 2.5f;
			hazardSpeedMax = 3.5f;


		}
		else if(EventManager.previousGoal > 1)
		{

			spawnProb[0] = 0.025f;
			spawnProb[1] = 0f;
			spawnProb[2] = 0.06f;

			hazardCount = 20;
			waveWait = 10f;
			spawnWait = 0.5f;

			hazardSpeedMin = 2f;
			hazardSpeedMax = 2.5f;

		}
		

		float temp = 1;
		for (int i = 0; i < spawnProbTable.Length; i++)
		{
			temp = temp - spawnProb[i];
			spawnProbTable[i] = temp;
		}

	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		
		while (true)
		{
			isWave = true;
			for (int i = 0; i < hazardCount ; i++)
			{
				SpawnHazard();
				
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			GameController.Instance.SavePlayerData();
		}
	}


	int GetAsteroidType()
	{

		float dice = Random.Range(0, 1f);

		for (int i = 0; i < spawnProbTable.Length; i++)
		{
			if (dice > spawnProbTable[i])
			{
				return i;
			}
		}

		return -1;
	}
	
	
	public void SpawnHazard()
	{
		type = GetAsteroidType();
		xPos = Random.Range(xLeft, xRight);
		
		hazardSpeed = Random.Range(hazardSpeedMin, hazardSpeedMax);
		spawnSpeed = new Vector3 (0, 0, -hazardSpeed);
		spawnPos = new Vector3 (xPos, 0, zUp);

		switch (type)
		{
		case 0:
			spawner = PoolManager.Pools["Pool"].Spawn("AsteroidCan");

			if(Random.Range(0,2)%2 == 0)
			{
				spawner.position = leftPos;
				spawner.rigidbody.velocity = new Vector3(1.2f,0,0);
			}
			else
			{
				spawner.position = rightPos;
				spawner.rigidbody.velocity = new Vector3(-0.8f,0,0);
			}

			//spawner.rigidbody.rotation = Quaternion.identity;
			break;
		case 1:
			spawner = PoolManager.Pools["Pool"].Spawn("AsteroidBigBarrel");

			spawner.position = spawnPos; 
			spawner.rigidbody.velocity = spawnSpeed;

			break;
		case 2:
			spawner = PoolManager.Pools["Pool"].Spawn("AsteroidBarrel");

			spawner.position = spawnPos; 
			spawner.rigidbody.velocity = spawnSpeed;

			break;
		case 3:
			spawner = PoolManager.Pools["Pool"].Spawn("AsteroidBig");
			spawner.position = spawnPos; 
			spawner.rigidbody.velocity = spawnSpeed;
			//spawner.rigidbody.rotation = Quaternion.identity;

			break;
		default:
			spawner = PoolManager.Pools["Pool"].Spawn("AsteroidSmall");
			spawner.position = spawnPos;
			spawner.rigidbody.velocity = spawnSpeed;
			//spawner.rigidbody.rotation = Quaternion.identity;
			break;
		}

	}

	
	public void PopStorm()
	{
		spaceStorm.SetActive(true);
	}
	
	public void PopFireWork()
	{
		fireWork.SetActive(true);
	}

	public void PopCoins(int num, Vector3 spawnPosition)
	{
		StartCoroutine(PopCoin(num, spawnPosition));
	}	      

	public IEnumerator PopCoin(int num, Vector3 _spawnPosition)
	{
		int coin = 0;
		while (coin < num)
		{
			spawner = PoolManager.Pools["Pool"].Spawn("Coin", _spawnPosition,Quaternion.identity);

			coin +=1;
			yield return new WaitForSeconds(0.15f);
		}
	}

	public void PopDustName(string _dustname, int _coins, Vector3 spawnPosition)
	{
		int coins;
		switch (_dustname)
		{
		case "Can" :

			if (GameController.extinguisherNumber < 1)
			{
				spawner = PoolManager.Pools["Pool"].Spawn("Extinguisher", spawnPosition,Quaternion.identity);
			}
			else
			{
				int dice = Random.Range (0,3);
				if (dice > 0 )
				{
					spawner = PoolManager.Pools["Pool"].Spawn("GasBottle", spawnPosition,Quaternion.identity);
				}
	//			else if (dice == 1)
	//			{
	//				spawner = PoolManager.Pools["Pool"].Spawn("PlayerShield", spawnPosition,Quaternion.identity);
	//			}
				else
				{
					spawner = PoolManager.Pools["Pool"].Spawn("Extinguisher", spawnPosition,Quaternion.identity);
				}
			}
			break;
			
		case "Barrel" :

//			if(Random.Range(0,10) > 2)
//			{
				coins = Random.Range(1,6);
				spawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText", spawnPosition,Quaternion.identity);

				if(GameController.isVip == 1)
				{
					spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",coins,"0 X 2"));
				}
				else
				{
					spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",coins,"0"));
				}

				spawner.GetComponent<ScoreText>().scoreText.color = Color.cyan;

				PopCoins(coins, spawnPosition);
//			}
//			else
//			{
//				spawner = PoolManager.Pools["Pool"].Spawn("Rocket", spawnPosition,Quaternion.identity);
//				spawner.position = spawnPosition;
//			}
			break;
			
		case "Dust" :

			spawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText", spawnPosition, Quaternion.identity);

			if(GameController.isVip == 1)
			{
				spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",_coins,"0 X 2"));
			}
			else
			{
				spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",_coins,"0"));
			}
				
			spawner.GetComponent<ScoreText>().scoreText.color = Color.cyan;
			spawner.position = spawnPosition;

			PopCoins(_coins, spawnPosition);
			break;

		case "BigBarrel" :

			coins = Random.Range(5,11);
			spawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText", spawnPosition,Quaternion.identity);

			if(GameController.isVip == 1)
			{
				spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",coins,"0 X 2"));
			}
			else
			{
				spawner.GetComponent<ScoreText>().SetScore(string.Concat("+",coins,"0"));
			}

			spawner.GetComponent<ScoreText>().scoreText.color = Color.cyan;
			PopCoins(coins, spawnPosition);
			break;
		}
		
		PopDust(spawnPosition);
		
	}

	public void PopDust(Vector3 _spawnPosition)
	{
		SoundManager.Instance.PlaySound(7);
		PoolManager.Pools["EffectPool"].Spawn(dustPrefab, _spawnPosition, Quaternion.identity);
	}

	public void SpawnStarIcons(int _type, int _number, Vector3 _position)
	{
		StartCoroutine(SpawnStarIcon(_type, _number, _position));
	}
		
	IEnumerator SpawnStarIcon(int _type, int _number, Vector3 _position)
	{

		for (int i=0; i < _number; i++)
		{
			if (_type ==1)
			{
				spawner = PoolManager.Pools["Pool"].Spawn("aStar");
				spawner.position = _position;
			}
			else if (_type == 2)
			{
				spawner = PoolManager.Pools["Pool"].Spawn("aStarHero");
				spawner.position = _position;
			}
			else if (_type == 3)
			{
				spawner = PoolManager.Pools["Pool"].Spawn("aStarLegend");
				spawner.position = _position;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}




	public void SpawnBlood(Vector3 _spawnPosition)
	{
		spawner = PoolManager.Pools["Pool"].Spawn("Blood");
		spawner.position = _spawnPosition;
	}
	



	void OnApplicationQuit()
	{
		//PoolManager.Pools.DestroyAll();
	}


}
