using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PathologicalGames;

public class StarSprite : MonoBehaviour
{
	public int id;
	private int level;
	private string starName;
	public int type;

	private Vector4 spriteColor;
	public Image starImage;

	public float lifeTime;
	public float xMin, xMax;
	private Vector3 spawnPos;

	public ParticleSystem starPointPrefab;
	private ParticleSystem starPointSpawner;

	// Use this for initialization
	private bool firstTime;

//	void Awake()
//	{
//		this.gameObject.SetActive(false);
//	}


	void OnEnable()
	{
		spawnPos = new Vector3 (Random.Range(xMin, xMax), -100f, 16f);
		transform.position = spawnPos;
		transform.localScale = new Vector3 (1f, 1f, 1f);
		StartCoroutine(DespawnTimer());
		StartCoroutine(StarPoints());
	}

	void OnDisable()
	{
		if(PoolManager.Pools.ContainsKey("Pool"))
			PoolManager.Pools["Pool"].Despawn(this.transform);
	}

	public void SetSprite (int _id, int _level, string _name, int _type, Sprite _sprite, bool _firstTime)
	{
		id = _id;
		starImage.overrideSprite = _sprite;
		level = _level;
		starName = _name;
		type = _type;
		firstTime = _firstTime;

		//rare
		if (type == 1)
		{
			spriteColor = new Vector4 (1f, 1f, 1f, 0.3f);
		}
		else if (type == 2)
		{
			spriteColor = new Vector4 (1f, 0.11f, 0.39f, 0.3f);
		}
		else
		{
			spriteColor = new Vector4 (1f, 0.5f, 0f, 0.3f);
		}

		starImage.color = spriteColor;

	}

	IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		this.gameObject.SetActive(false);
	}
	// Update is called once per frame

	IEnumerator StarPoints()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(0.8f, 3.5f));

			Vector3 starPointPos = transform.position;
			starPointPos.x += Random.Range(-0.8f, 0.8f);
			starPointPos.z += Random.Range(-0.8f, 0);

			starPointSpawner = PoolManager.Pools["EffectPool"].Spawn(starPointPrefab, starPointPos, Quaternion.identity);

		}
	}

	void Update()
	{


		if (Input.GetMouseButtonDown(0))
		{	
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast (ray,out hit)) 
			{			
				if (hit.transform.name == this.transform.name)
				{
					SoundManager.Instance.PlaySound(1);
					
					// increal alpha by click. plcy click sount here.
					spriteColor.w += 0.15f;
					starImage.color = spriteColor;
					
					transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
					
					// if star is showed perfectly
					if (spriteColor.w >= 1f)
					{
						
						//StarLoader script = starLoader.GetComponent<StarLoader>();
						
						string typeString;
						if (type == 1)
						{
							typeString ="[일반] ";
							StarLoader.Instance.UpdateAlbum(id,1);

						}
						else if (type == 2)
						{
							typeString ="[영웅] ";
							StarLoader.Instance.UpdateAlbum(id,2);
						}
						else
						{
							typeString ="[전설] ";
							StarLoader.Instance.UpdateAlbum(id,3);
						}

						string notice = string.Concat("[ ", level, " 레벨] ", typeString, starName, " 발견 하였습니다.");

						NoticeManager.Instance.SetNotice(notice, 5f);

						if (firstTime)
						{
							GameController.Instance.StarPhoto(id);
							firstTime = false;
						}


						SpawnManager.Instance.SpawnStarIcons(type, level, transform.position);

						gameObject.SetActive(false);
						
					}
				}
			}
		}
	}
}

