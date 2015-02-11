using UnityEngine;
using System.Collections;
using PathologicalGames;

public class DestroyByTimePool : MonoBehaviour 
{
	public float lifeTime;

	void Awake()
	{
	//	this.gameObject.SetActive(false);
	}

	void OnEnable()
	{
		StartCoroutine(DespawnTimer());
	}

	IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(lifeTime);
//
		PoolManager.Pools["EffectPool"].Despawn(this.transform);
	}

//	void OnDisable()
//	{
////		if(PoolManager.Pools.ContainsKey("EffctPool"))
//			PoolManager.Pools["EffectPool"].Despawn(this.transform);
//	}

}
	
