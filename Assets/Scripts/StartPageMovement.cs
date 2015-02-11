using UnityEngine;
using System.Collections;

public class StartPageMovement : MonoBehaviour 
{
	public float time;
	// Use this for initialization
	void Start()
	{
		Invoke("IntroMove", 7);
		Invoke("PlayBeep", 55);
	}

	void IntroMove()
	{
		iTween.MoveTo(gameObject, 
		              iTween.Hash(
			"time", time, 
			"path", iTweenPath.GetPath ("StartPath"),
			"easetype",iTween.EaseType.easeInOutQuad,
			"looptype",iTween.LoopType.pingPong
			));
	}

	void PlayBeep()
	{
		audio.Play();
	}

}
