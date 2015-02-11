using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour
{
	public int maxHealth;
	private int _health;
	private int _maxHealth;
	public GameObject healPrefab;
	public GameObject deathPrefab;

	// spawn Position
//	public Vector3 startPos = new Vector3 (8f, -4f, 10f);


//	void Start()
//	{
//		gameObject.SetActive(false);
//	}

	void OnEnable()
	{

		transform.position = new Vector3 (9f, -13.0f, 9f);

		iTween.MoveTo(gameObject, 
		              iTween.Hash(
			"time", 25f, 
			"path", iTweenPath.GetPath ("WhalePath2"),
			"easetype",iTween.EaseType.easeInOutQuad,
			"looptype",iTween.LoopType.none,
			"looktime", 1.5f,
			//"axis", "z",
			"orientToPath", true
			));
	}


}
