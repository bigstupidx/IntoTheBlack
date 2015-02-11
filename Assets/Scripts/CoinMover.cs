using UnityEngine;
using System.Collections;
using PathologicalGames;

public class CoinMover : MonoBehaviour 
{
	public float initialLifeTime;
	private bool initialized = false;
	private float lifeTime;
	public Vector3 targetPos;
	private Transform xform;

	private void Awake() 
	{
		this.xform = this.transform;
	}
	void OnEnable()
	{
		lifeTime = Random.Range (initialLifeTime, initialLifeTime + 0.5f);

	}
	// Use this for initialization
	void Update () 
	{
		if (!initialized)
		{
			initialized = true;
			iTween.MoveTo(gameObject, 
			              iTween.Hash(
				"position", targetPos,
				"time", lifeTime, 
				"easetype",iTween.EaseType.easeInQuad
				));
		}
	}

	void OnDisable()
	{
		iTween.Stop(gameObject);
		initialized = false;
//
		if(PoolManager.Pools.ContainsKey("Pool"))
			PoolManager.Pools["Pool"].Despawn(xform);
	}

}
