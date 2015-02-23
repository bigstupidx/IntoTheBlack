using UnityEngine;
using System.Collections;

public class ClickTeleport : MonoBehaviour 
{
	// Use this for initialization
	private Transform xform;

	public Animator animator;
	
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
					SoundManager.Instance.PlaySound(14);
					//Handheld.Vibrate();
					GameController.distanceFromEarth += (GameController.engineSpeed * 10);
					DialogManager.Instance.ColonyEvent();
					animator.SetTrigger("blackhole");
				}
			}
		}
	}
}