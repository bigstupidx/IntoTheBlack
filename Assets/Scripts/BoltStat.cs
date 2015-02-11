using UnityEngine;
using System.Collections;
using PathologicalGames;

public class BoltStat : MonoBehaviour
{
	public int damage;
	public float speed;
	private Vector3 movement;
	public float tumble;
	public ParticleSystem explosionPrefab;
//	public ParticleSystem trailPrefab;
	
//	private ParticleSystem explosion;
	private Transform xform;

	private void Awake() 
	{
		this.xform = this.transform;
	}


	void Update()
	{
		movement = new Vector3 (0,0,1f) * Time.deltaTime * speed;
		xform.position += movement;
	}

	void OnEnable()
	{
		rigidbody.velocity = Random.insideUnitSphere * tumble;
		damage = (GameController.shotLevel-1) * 7 + 5;
	}

	void OnDisable()
	{

		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		if(PoolManager.Pools.ContainsKey("PlayerPool"))
			PoolManager.Pools["PlayerPool"].Despawn(xform);
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Asteroid"))
		{
			PoolManager.Pools["EffectPool"].Spawn(explosionPrefab, xform.position, xform.localRotation);
		}

	}
	
}
