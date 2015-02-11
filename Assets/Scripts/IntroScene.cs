using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class IntroScene : MonoBehaviour
{

	public void StartGame()
	{
		Application.LoadLevel("LoadingScene");
	}

}
