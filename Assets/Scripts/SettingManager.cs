using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour 
{
	public Image effectIcon;
	public Image bgmIcon;

	public Sprite mute;
	public GameObject credit;
	public GameObject panelBottom;
	public GameObject panelRestart;
	public GameObject panelGoogleCloud;


	public void effectSoundMute()
	{
		if (GameController.isEffectSound == 0) // true false is reversed cuz prefs default value is zero and default volume is on . sorry
		{
			SoundManager.Instance.PlaySound(4);
			GameController.isEffectSound = 1;
			effectIcon.overrideSprite = mute;
			PlayerPrefs.SetFloat("isEffectSound", 1);
		}
		else
		{
			SoundManager.Instance.PlaySound(8);
			GameController.isEffectSound = 0;
			PlayerPrefs.SetFloat("isEffectSound", 0);
			effectIcon.overrideSprite = null;
		}
	}
	
	public void bgmMute()
	{
		if (GameController.isBgm == 0)
		{
			SoundManager.Instance.PlaySound(4);
			GameController.isBgm = 1;
			PlayerPrefs.SetFloat("isBgm", 1);
			bgmIcon.overrideSprite = mute;
		}
		else
		{
			SoundManager.Instance.PlaySound(8);
			GameController.isBgm = 0;
			PlayerPrefs.SetFloat("isBgm", 0);
			bgmIcon.overrideSprite = null;
		}
		Debug.Log (GameController.isBgm);
	}

	public void showCredit()
	{
		if (credit.activeSelf == false)
		{
			panelBottom.GetComponent<PanelBottm>().PopMenu(5);
			SoundManager.Instance.PlaySound(8);
			credit.SetActive(true);
		}
	}

	public void ShwoRestart()
	{

		if(panelRestart.activeSelf == false)
		{
			SoundManager.Instance.PlaySound(0);
			panelRestart.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public void RestartGame()
	{
		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetInt("dustPoints", GameController.dustPoints);
		PlayerPrefs.SetInt("starPoints", GameController.starPoints);
		PlayerPrefs.SetInt("legendStars", GameController.legendStars);
		PlayerPrefs.SetInt("heroStars", GameController.heroStars);
//		PlayerPrefs.SetInt("mercury", GameController.wingMercury);
//		PlayerPrefs.SetInt("venus", GameController.wingVenus);

//		PlayerPrefs.SetInt("earth", 0);
//		PlayerPrefs.SetInt("moon", 0);
//		PlayerPrefs.SetInt("mars", 0);
//		PlayerPrefs.SetInt("jupiter", 0);
//		PlayerPrefs.SetInt("saturn", 0);
//		PlayerPrefs.SetInt("uranus", 0);
//		PlayerPrefs.SetInt("neptune", 0);
//		PlayerPrefs.SetFloat("distanceToNext", 0);
//		PlayerPrefs.SetFloat("distanceFromEarth", 0);
//		PlayerPrefs.SetFloat("journeyTime", 0);
//		PlayerPrefs.SetInt("shotLevel", 1);
//		PlayerPrefs.SetInt("engineLevel", 1);
//		PlayerPrefs.SetInt("timeLevel", 1);

		Time.timeScale = 1f;
		GameController.Instance.RefreshShip();
		Application.LoadLevel("main");
	}

	public void CloseRestart()
	{
		SoundManager.Instance.PlaySound(4);
		panelRestart.SetActive(false);
		Time.timeScale = 1;
	}

	
	public void LoadGoogleCloud()
	{
		Debug.Log ("Google");
		//SoundManager.Instance.PlaySound(0);
//		if(panelGoogleCloud.activeSelf == false)
//			panelGoogleCloud.SetActive(true);
	}

	
	public void ShwoGoogleCloud()
	{
		SoundManager.Instance.PlaySound(0);
		if(panelGoogleCloud.activeSelf == false)
			panelGoogleCloud.SetActive(true);
	}
	
	public void CloseGoogleCloud()
	{
		SoundManager.Instance.PlaySound(4);
		panelGoogleCloud.SetActive(false);
	}

	
	
}
