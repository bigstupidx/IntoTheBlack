using UnityEngine;
using System.Collections;


public class DestroyByTime : MonoBehaviour 
{
	public float lifeTime;
//
//	void Awake()
//	{
//		gameObject.SetActive(false);
//	}

	void OnEnable()
	{
		StartCoroutine(DespawnTimer());
	}

	IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);
	}

}
	
