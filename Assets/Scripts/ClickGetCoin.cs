using UnityEngine;
using System.Collections;

public class ClickGetCoin : MonoBehaviour 
{

	// Use this for initialization
	private Transform xform;

	void Awake()
	{
		xform = this.transform;
	}

	void Update()
	{
		ClickCheck();
	}

	void ClickCheck()
	{
		if (Input.GetMouseButtonDown(0))
		{	
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast (ray,out hit)) 
			{			
				if (hit.transform.name == xform.name)
				{					
					SoundManager.Instance.PlaySound(8);
					//Handheld.Vibrate();
					SpawnManager.Instance.PopCoins(1,hit.transform.position);
				}
			}
		}
	}
}
