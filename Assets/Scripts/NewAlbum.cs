using UnityEngine;
using System.Collections;

public class NewAlbum : MonoBehaviour 
{

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void OnDisable()
	{
		Time.timeScale = 1;
	}
}
