using UnityEngine;
using System.Collections;
using PathologicalGames;

public class TestProjectile : MonoBehaviour {

	public Transform[] shotPrefab;

	public float fireRate;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(SpawnProjectile());
	}
	
	// Update is called once per frame

	IEnumerator SpawnProjectile()
	{
		while (true)
		{

			for (int i =0; i < shotPrefab.Length; i++)
			{
				Transform spawner;
				Vector3 pos =  new Vector3 (-2f, 0, -2f);
				spawner = PoolManager.Pools["PlayerPool"].Spawn(shotPrefab[i], pos, Quaternion.identity);

				yield return new WaitForSeconds(fireRate);


				pos =  new Vector3 (2f, 0, -2f);
				spawner = PoolManager.Pools["PlayerPool"].Spawn(shotPrefab[i], pos, Quaternion.identity);

				yield return new WaitForSeconds(0.2f);
			}
		}
	}


}
