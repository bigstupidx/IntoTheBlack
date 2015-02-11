using UnityEngine;
using System.Collections;
using PathologicalGames;

public class VIPWing : MonoBehaviour 
{
	public float fireDelay;
	private float nextFire;
	public Transform shotPrefab;
	private Transform xForm;
	
	void OnEnable()
	{
		xForm = this.transform;
	}
	
	void Update()
	{
		nextFire += Time.deltaTime;

		if(nextFire > fireDelay)
		{
			Vector3 realShotPos = xForm.position;
			realShotPos.y = 0;

			Transform spawner = PoolManager.Pools["PlayerPool"].Spawn(shotPrefab, realShotPos, Quaternion.identity);

			nextFire = 0;
		}
	}

}