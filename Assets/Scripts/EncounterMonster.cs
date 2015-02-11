using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EncounterMonster : MonoBehaviour
{

	public int initialHealth;
	public Vector3 spawnPosition;
	public int reward;

	public GameObject sliderObject;
	public Slider hpBar;
	public Text hpText;

	private int maxHealth;
	private int health;
	private Vector3 position;

	public int maxShieldHealth;
	private int shieldHealth;

	public GameObject shield;
	public Vector3 shieldScale;
	private Vector3 originScale;
	public Animator animator;
	private bool isAlive;

	private Transform xform;
	// Use this for initialization


	void aWake()
	{
		xform = this.transform;
		health = initialHealth;
	}
	void Update ()
	{
		if (gameObject.transform.position.x < -10)
		{
			gameObject.SetActive(false);
		}
	}

	public void CallDie()
	{
		gameObject.SetActive(false);
	}

	void OnEnable()
	{
		xform.position = spawnPosition;
		maxHealth = initialHealth;
		health = initialHealth;

		shieldHealth = maxShieldHealth;


		hpBar.minValue = 0;
		hpBar.maxValue = maxHealth;
		sliderObject.SetActive(true);

		//originScale = shield.transform.localScale;
		shield.transform.localScale = shieldScale;
		shield.SetActive(true);

		hpBar.value = health;
		hpText.text = string.Concat (health, " / " ,maxHealth);

		isAlive = true;
		NoticeManager.Instance.SetNotice("외계 생명체를 공격하면 추가 보상을 얻습니다. \n 행성의 공격은 외계인에게 더 효과적입니다.",5);
	}

	void OnDisable()
	{
		if (health > 0)
		{
			SpawnManager.Instance.PopStorm();
		}
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Bolt") && isAlive)
		{
			other.gameObject.SetActive(false);

			if (shieldHealth > 0)
			{
				SpawnManager.Instance.PopDust(other.transform.position);
				shieldHealth -= other.gameObject.GetComponent<BoltStat>().damage;

				if (shieldHealth !=0)
				{
					float temp = 0.05f / maxShieldHealth;
					int y = maxShieldHealth - shieldHealth;
					Vector3 scaleMulty = new Vector3 (y * temp, y * temp, y * temp);
					scaleMulty += shield.transform.localScale;
					
					shield.transform.localScale = scaleMulty;

				}

				if (shieldHealth <=0)
				{
					shield.SetActive(false);
				}
					
			}
			else
			{

				health -= other.gameObject.GetComponent<BoltStat>().damage;
				SpawnManager.Instance.SpawnBlood(other.transform.position);
				animator.SetTrigger("Hit");

				hpBar.value = health;
				hpText.text = string.Concat (health, " / " ,maxHealth);

				if (health <= 0)
				{
					animator.SetTrigger("Die");
					sliderObject.SetActive(false);
					SpawnManager.Instance.PopFireWork();
					SpawnManager.Instance.PopCoins(reward, xform.position);

					isAlive = false;
				}
			}
		}
	}

}
