using UnityEngine;
using System.Collections;
using PathologicalGames;

public class WingShot : MonoBehaviour
{

	public float fireDelay;
	private float nextFire;
	public Transform shotPrefab;
	public Transform xForm;

	void Awake()
	{
		xForm = this.transform;
	}

	public void FireShot()
	{
		StartCoroutine(MoonLightShot());
	}

	IEnumerator MoonLightShot()
	{

		for (int i =0; i < 10; i++)
		{
			Vector3 realShotPos = xForm.position;
			realShotPos.y = 0;
			
			Transform spawner = PoolManager.Pools["PlayerPool"].Spawn(shotPrefab, realShotPos, Quaternion.identity);

			yield return new WaitForSeconds(fireDelay);
		}
	}
	
}
