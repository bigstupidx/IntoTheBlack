using UnityEngine;
using System.Collections;
using PathologicalGames;

public class SoundManager : MonoBehaviour 
{
	private static SoundManager _instance;
	public static SoundManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
				if(!_instance)
				{
					//					GameObject container = new GameObject();
					//					container.name = "GameControllerContainer";
					//					_instance = container.AddComponent(typeof(GameController)) as GameController;
				}
			}
			
			return _instance;
		}
	}

//	public AudioClip[] fxSound;

	public AudioSource[] prefab;

	public GameObject AudioPool;
	private SpawnPool pool;
	//private AudioSource current;

	void Start()
	{
		this.pool = AudioPool.GetComponent<SpawnPool>();
	}

	public void PlaySound(int _number)
	{

		if(GameController.isEffectSound == 0)
		{
			this.pool.Spawn
				(
					this.prefab[_number], 
					this.transform.position, 
					this.transform.rotation
					);
		}

//		if (fxSound[_number] != null && GameController.isEffectSound == 0)
//		{
//
////			if(!audio.isPlaying)
////				audio.PlayOneShot(fxSound[_number]);
//		}
	}
}
