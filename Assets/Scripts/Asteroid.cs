using UnityEngine;
using System.Collections;
using PathologicalGames;

public class Asteroid : MonoBehaviour 
{
	public int maxHealth;
	public string dustname;
	private int health;
	public int coins;

	void OnEnable()
	{
		health = maxHealth;
	}

	void OnDisable()
	{
		if(PoolManager.Pools.ContainsKey("Pool"))
			PoolManager.Pools["Pool"].Despawn(this.transform);
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Bolt"))
		{
			other.gameObject.SetActive(false);
			
			if (health > 0)
			{
//				SpawnController.Instance.PopDust(other.transform.position);
				Vector3 slow = new Vector3 (0, 0, -0.12f);
				gameObject.transform.rigidbody.velocity -= slow;

				if (other.gameObject.GetComponent<BoltStat>())
				{
					health -= other.gameObject.GetComponent<BoltStat>().damage;
				}
				else
				{
					health -=1;
				}
			}

			if (health <=0)
			{
				SpawnManager.Instance.PopDustName (dustname, coins, transform.position);
				gameObject.SetActive(false);
			}
			else
			{
				SpawnManager.Instance.PopDust(other.transform.position);
			}
		
		}
		else if (other.CompareTag("Player"))
		{
			this.gameObject.SetActive(false);

			if (!PlayerController.isShield)
			{
				NoticeManager.Instance.SetNotice("충격으로 탐사선에 화재가 발생했습니다.\n 우주 샤워로 화재를 진압하세요 (소화기가 소모 됩니다.)",5);
				SpawnManager.Instance.PopDust(transform.position);
				GameController.Instance.SlowMotion();
			}
			else
			{
				SpawnManager.Instance.PopDustName (dustname, coins, transform.position);
			}
		}
		else if (other.CompareTag("EMP"))
		{
			this.gameObject.SetActive(false);
			SpawnManager.Instance.PopDustName (dustname, coins, transform.position);
		}


	}
}
