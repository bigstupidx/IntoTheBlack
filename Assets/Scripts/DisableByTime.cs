using UnityEngine;
using System.Collections;

public class DisableByTime : MonoBehaviour 
{
	public float lifeTime;

	void OnEnable()
	{
		StartCoroutine(DespawnTimer());
	}
	
	IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}

}
