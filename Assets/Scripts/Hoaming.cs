using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PathologicalGames;

public class Hoaming : MonoBehaviour 
{
	private Transform target;
	// Use this for initialization
	public float magnetDist;
	public string itemName;
	public float magnetSpeed;
	// Update is called once per frame

	private Transform xform;

	void OnEnable()
	{
		target = GameObject.Find("Player").transform;
		xform = this.transform;
	}

	void Update () {
			
//		Vector3 targetDelta = target.position - transform.position;
		float dist = Vector3.Distance (xform.position, target.position);

		if (dist < magnetDist)
		{
			xform.position = Vector3.MoveTowards(xform.position, target.position, Time.deltaTime * magnetSpeed * (magnetDist / dist));
		}

	}

	void OnDisable()
	{
		if(PoolManager.Pools.ContainsKey("Pool"))
			PoolManager.Pools["Pool"].Despawn(xform);
	}
	
	void OnTriggerEnter(Collider other)
	{
	
		if (other.CompareTag("Player"))
		{
			if(itemName == "Extinguisher")
			{
//				Debug.Log ("Fire");
				GameController.extinguisherNumber +=1;
				if(GameController.extinguisherNumber > 10 )
					GameController.extinguisherNumber = 10;
			}
			else if (itemName == "PlayerShield")
 	        {
				// PlayerController.Instance.TurnOnShield(30f);
			}
			else if (itemName == "GasBottle")
			{
				GameController.Instance.SwingByCharge();
			}
			else if (itemName == "Rocket")
			{
//				PlayerController.fireRate -= 0.08f;
//
//				if(PlayerController.fireRate < 0.15f)
//					PlayerController.fireRate = 0.15f;

			}
		
			gameObject.SetActive(false);
		}
	}
}
