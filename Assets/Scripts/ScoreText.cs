using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathologicalGames;


public class ScoreText : MonoBehaviour 
{
	public Text scoreText;
	public float lifeTime;
	// Use this for initialization


	void OnEnable()
	{
		StartCoroutine(DespawnTimer());
	}

	public void SetScore(string _score)
	{
		scoreText.text = _score;
	}

	public void SetDamage(string _damage)
	{
		scoreText.text = _damage;
		scoreText.color = new Vector4 (1,0.18f,0.18f,1);
	}


	IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
	
	void OnDisable()
	{		
		if(PoolManager.Pools.ContainsKey("EffectPool"))
			PoolManager.Pools["EffectPool"].Despawn(this.transform);
	}

}
