using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathologicalGames;
using SmartLocalization;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}



public class PlayerController : MonoBehaviour 
{

	private static PlayerController _instance;
	
	public static PlayerController Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
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


	public Transform shotSpawn;

	public static float fireRate;
//	public static float[] fireRateValues;
	private float nextFire;

	public Boundary boundary;

	public float rotationSpeed;
	public static float rotationValue;

//	private float mAngle = 0.0f;

	public Button fireButton;
	public Button autoButton;

	public static bool autoShot = false;

	public GameObject mercury;
	public GameObject venus;
	public GameObject earth;
	public GameObject moon;
	public GameObject mars;
	public GameObject jupiter;
	public GameObject saturn;
	public GameObject uranus;
	public GameObject neptune;

//	public GameObject backCamera;

	public static bool isShield;
	public GameObject playerShield;
	public Animator shieldAni;
	private float shieldTime;
	public Text shieldTimeText;
	public static int infinityShield;

	private float afkTimer, afkTime;
	private bool isAfk;
	public Animator cameraAni;

	public Transform[] bolt;

	private Transform xForm;

	public static bool isFireButton;
	public GameObject[] skillButton;

	private Transform spawner;
	private Vector3 realShotPos;

	void Awake()
	{
		for (int i=0; i < skillButton.Length; i++)
		{
			skillButton[i].SetActive(false);
		}

		xForm = this.transform;
	}

	void Start()
	{
		fireRate = 1.2f;
		infinityShield = PlayerPrefs.GetInt("infinityShield");
		afkTime = 300f;

		RefreshRotationValue();
		UpdateWing();
	}

	void OnTriggerEnter(Collider other)
	{
		//if(isShield)
		if(!other.CompareTag("Bolt") && isShield)
		{
//			Debug.Log (other.name);
			 shieldAni.SetTrigger("shieldOn");
		}
	}

	public void TurnOnShield(float _time)
	{
		playerShield.SetActive(true);
		isShield = true;
		shieldTime += _time;
	}

	void CheckShieldTime()
	{
		if(infinityShield == 1)
		{
			if(playerShield.activeSelf == false)
			{
				playerShield.SetActive(true);
				isShield = true;
			}

			shieldTime = 100f;
			shieldTimeText.text = string.Concat("Shield : ON");
		}
		else
		{
			shieldTime -= Time.deltaTime;
			shieldTimeText.text = string.Concat("Shield : ", shieldTime.ToString("N0"), " secs");
		}

		if (shieldTime <0)
		{
			isShield = false;
			playerShield.SetActive(false);
			shieldTime = 0;
			shieldTimeText.text = "";
		}

	}


	void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("infinityShield", infinityShield);
	}

	public void UpdateWing()
	{
		if (GameController.wingMercury == 1)
			mercury.SetActive(true);
		if (GameController.wingVenus == 1)
			venus.SetActive(true);
		if (GameController.wingEarth == 1)
		{
			skillButton[0].SetActive(true);
			earth.SetActive(true);
		}
		if (GameController.wingMoon == 1)
		{
			skillButton[1].SetActive(true);
			moon.SetActive(true);
		}
		if (GameController.wingMars == 1)
		{
			skillButton[2].SetActive(true);
			mars.SetActive(true);
		}
		if (GameController.wingJupiter == 1)
		{
			skillButton[3].SetActive(true);
			jupiter.SetActive(true);
		}
		if (GameController.wingSaturn == 1)
			saturn.SetActive(true);
		if (GameController.wingUranus == 1)
			uranus.SetActive(true);
		if (GameController.wingNeptune == 1)
			neptune.SetActive(true);

	}


	void Update ()
	{
		AutoMove ();
		FireShot ();
		DragMove();


		if(Input.GetMouseButtonDown(0))
	   	{
			afkTimer = afkTime;
		}
		if (isShield)
			CheckShieldTime();

		if (!isAfk)
		{
			afkTimer -= Time.deltaTime;
		}

		if (afkTimer < 0)
		{
			cameraAni.SetBool("isZoom", true);
			isAfk = true;
		}

		if(afkTimer > 0 && isAfk)
		{
			cameraAni.SetBool("isZoom", false);
			isAfk = false;
		}

	}

	bool isTouching;
	bool isLastFrameMouseDown;

	Vector3 startPos = Vector3.zero;
	Vector3 endPos = Vector3.zero;
	Vector3 targetPos = Vector3.zero;

//	Vector3 targetCameraPos = Vector3.zero;

	private void DragMove()
	{

			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				startPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			}

			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				endPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
				
				targetPos = endPos - startPos; 
				startPos = endPos;
				
				targetPos = new Vector3(targetPos.x, 0, 0); 
				transform.position += targetPos;
				
				if (transform.position.x > 5)
					transform.position = new Vector3 (5f, 0, 0);
				
				if (transform.position.x < -5)
					transform.position = new Vector3 (-5f, 0, 0);
			}

//		if (Input.GetTouch == 0)
//		{
//			isTouching = false;
//		}
	}			
	
	private void AutoMove()
	{
		transform.Rotate (0,0, Time.deltaTime * rotationValue);
	}


//		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
//		{
//			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//			transform.Translate(-touchDeltaPosition.x * Time.deltaTime * 2f, 0, 0);
//		}

//		if(Input.GetMouseButtonDown(0))
//		{
//			isTouching = true;
//			startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		}		
//
//		if(isTouching && Input.GetMouseButton(0))
//		{
//			endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
//
//			targetPos = endPos - startPos; 
//			startPos = endPos;
//
//			targetPos = new Vector3(targetPos.x, 0, 0); 
//			transform.position += targetPos;
//			
//			if (transform.position.x > 5)
//				transform.position = new Vector3 (5f, 0, 0);
//
//			if (transform.position.x < -5)
//				transform.position = new Vector3 (-5f, 0, 0);
//
//		}
//
//
//		if (!Input.GetMouseButton(0))
//	    {
//			isTouching = false;
//		}
//	}			
//
//	private void AutoMove()
//	{
//		transform.Rotate (0,0, Time.deltaTime * rotationValue);
//	}


	public void FireShot()
	{
		nextFire += Time.deltaTime;

		if(nextFire > fireRate)
		{
//			SpawnManager.Instance.PopCoins(1,Vector3.one);
//			SpawnManager.Instance.PopDust(Vector3.zero);
			nextFire = 0f;

			realShotPos = shotSpawn.position;
			realShotPos.y = 0;

			switch (GameController.shotLevel)
			{
			case 1:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[0], realShotPos, Quaternion.identity);
				break;
			case 2:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[1], realShotPos, Quaternion.identity);
				break;
			case 3:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[2], realShotPos, Quaternion.identity);
				break;
			case 4:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[3], realShotPos, Quaternion.identity);
				break;
			case 5:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[4], realShotPos, Quaternion.identity);
				break;
			case 6:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[5], realShotPos, Quaternion.identity);
				break;
			case 7:
			case 8:
			case 9:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[6], realShotPos, Quaternion.identity);
				break;
			case 10:
			case 11:
			case 12:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[7], realShotPos, Quaternion.identity);
				break;
			case 13:
			case 14:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[8], realShotPos, Quaternion.identity);
				break;
			case 15:
			case 16:
			case 17:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[9], realShotPos, Quaternion.identity);
				break;
			case 18:
			case 19:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[10], realShotPos, Quaternion.identity);
				break;
			case 20:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[11], realShotPos, Quaternion.identity);
				break;
			case 21:
			case 22:
			case 23:
			case 24:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[12], realShotPos, Quaternion.identity);
				break;
			case 25:
			case 26:
			case 27:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[13], realShotPos, Quaternion.identity);
				break;
			case 28:
			case 29:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[14], realShotPos, Quaternion.identity);
				break;
			case 30:
				spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[15], realShotPos, Quaternion.identity);
				break;
			}

			
//
//			if (GameController.shotLevel > 25)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt05");
//				spawner.position = realShotPos;
//			}
//			else if (GameController.shotLevel > 20)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt04");
//				spawner.position = realShotPos;
//			}
//			else if (GameController.shotLevel > 15)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt03");
//				spawner.position = realShotPos;
//			}
//			else if (GameController.shotLevel > 10)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt02");
//				spawner.position = realShotPos;
//			}
//			else if (GameController.shotLevel > 5)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt01");
//				spawner.position = realShotPos;
//			}
//			else if (GameController.shotLevel > 0)
//			{
//				spawner = PoolManager.Pools["PlayerPool"].Spawn("bolt0");
//				spawner.position = realShotPos;
//			}
		}
	}

	public void RefreshRotationValue()
	{
		rotationValue = rotationSpeed  * (GameController.engineLevel + GameController.timeLevel);

		if (rotationValue < -1000f)
		{
			rotationValue = -1000f;
		}

	}

	public void AutoShotToggle()
	{
		if (GameController.shotLevel < 3)
		{
			LanguageManager thisLanguageManager = LanguageManager.Instance;
			NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("WeaponWarning"), 2f);
			return;
		}

		if (autoShot == false)
		{
			autoShot = true;
			autoButton.image.color = Color.red;
		}
		else
		{
			autoShot = false;
			autoButton.image.color = Color.green;
		}
	}

	public void RapidShot(float _time)
	{
		StartCoroutine(RapidShotCoroutine(_time));
	}

	IEnumerator RapidShotCoroutine(float _time)
	{

		for (int i =0; i < 6; i++)
		{
			realShotPos = shotSpawn.position;
			realShotPos.y = 0;

			SoundManager.Instance.PlaySound(2);
			spawner = PoolManager.Pools["PlayerPool"].Spawn(bolt[12], realShotPos, Quaternion.identity);
			
			yield return new WaitForSeconds(0.25f);
		}
	}


}
