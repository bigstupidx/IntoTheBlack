using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathologicalGames;
using SmartLocalization;

public class AlienPop : MonoBehaviour
{
//	public int maxShieldHealth;

	public GameObject shield;

//	public float lifeTime;
	public GameObject healthBarObject;
	public Slider healthBar;
	public Text healthText;
	public Text timerText;

	private int level;
	private bool isShield;
	private int maxHealth;
	private int health;
	private int shieldHealth;
//	private int immune;
	private int reward;
	private float timer;

	public GameObject blackHole;

	void Awake()
	{
		gameObject.SetActive(false);
		blackHole.SetActive(false);
	}


	void OnEnable()
	{
		isShield = true;
		shield.SetActive(true);

//		int dice;

		if (GameController.shotLevel < 5)
		{
			level = Random.Range(1,3);
		}
		else
		{
			level = Random.Range(GameController.shotLevel -3, GameController.shotLevel+2);
		}

		shieldHealth = ((level + 1) * 7 + 5 ) * 13;
		maxHealth = ((level + 1) * 7 + 5 ) * 20;
		health = maxHealth;
//		immune = (level-1)*7 + 5;

		reward = level * 11 + 3;

		healthBar.maxValue = maxHealth;
		healthBar.value = health;
		healthBarObject.SetActive(false);
		timer = 28f;
	}

	void Update()
	{
		timer -= Time.deltaTime;

		timerText.text = string.Concat(level, " 레벨 ", timer.ToString("N1"));

		if (timer < 0)
		{
			GameController.Instance.SlowMotion();
			SpawnManager.Instance.PopStorm();
			timer = 28f;
			this.gameObject.SetActive(false);
		}

	}
	

	void OnTriggerEnter(Collider other)
	{
		Transform textSpawner;
		string damageText;
		if (other.CompareTag("Bolt"))
		{
			other.gameObject.SetActive(false);

			if (isShield)
			{
				SpawnManager.Instance.PopDust(other.transform.position);

				if (other.gameObject.GetComponent<BoltStat>())
				{
//					if (other.gameObject.GetComponent<BoltStat>().damage >= immune)
//					{
						shieldHealth -= other.gameObject.GetComponent<BoltStat>().damage;

						textSpawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText");
						damageText = string.Concat("-", other.gameObject.GetComponent<BoltStat>().damage.ToString("N0"));
						textSpawner.GetComponent<ScoreText>().SetDamage(damageText);
						textSpawner.position = other.transform.position;

//					}
//					else
//					{
//						textSpawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText");
//						textSpawner.GetComponent<ScoreText>().SetDamage("면역");
//						textSpawner.position = other.transform.position;
//						// immune;
//					}
				}

				if (shieldHealth <= 0)
				{
					isShield = false;
					shield.SetActive(false);
				}

			}
			else
			{
				if (health > 0)
				{
					if (other.gameObject.GetComponent<BoltStat>())
					{
						health -= other.gameObject.GetComponent<BoltStat>().damage;
						healthBar.value = health;
						healthText.text = string.Concat(health, " / ", maxHealth);
						healthBarObject.SetActive(true);

						damageText = string.Concat("-", other.gameObject.GetComponent<BoltStat>().damage.ToString("N0"));

						textSpawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText");
						textSpawner.GetComponent<ScoreText>().SetDamage(damageText);
						textSpawner.position = other.transform.position;

					}
				}
				
				if (health <= 0)
				{
					// explosion and give reward

					SpawnManager.Instance.PopCoins(reward,transform.position);

					LanguageManager thisLanguageManager = LanguageManager.Instance;
//					if (level >= UpgradeManager.unlockShot)
//					{
//						SpawnManager.Instance.PopTech(transform.position);
//					}
					SpawnManager.Instance.PopFireWork();

					textSpawner = PoolManager.Pools["EffectPool"].Spawn("ScoreText");
					textSpawner.GetComponent<ScoreText>().SetScore(string.Concat("+",reward,"0"));

					NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("BlackHole"),5);

					healthBarObject.SetActive(false);
					gameObject.SetActive(false);

					GameController.colonyProgress += 1;
					if (GameController.colonyProgress >= 100)
					{
						Vector3 coinPos = new Vector3 (0.0f, 0.0f, 12.0f);
						SpawnManager.Instance.PopCoins(1000,coinPos);
						GameController.colonyProgress = 0;
						NoticeManager.Instance.SetNotice(thisLanguageManager.GetTextValue("UI.ColonyComplete"),5);
					}
					blackHole.SetActive(true);
				}
				else
				{
					SpawnManager.Instance.PopDust(other.transform.position);
				}
			}
		}
	}
}
