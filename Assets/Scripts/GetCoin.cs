using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetCoin : MonoBehaviour
{
	public GameObject coinIcon;
	public GameObject vipIcon;
	void Start()
	{
		if (GameController.isVip == 1)
		{
			vipIcon.SetActive(true);
			coinIcon.SetActive(false);
		}
		else
		{
			vipIcon.SetActive(false);
			coinIcon.SetActive(true);
		}

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Coin"))
		{
			if (GameController.isVip == 1)
			{
				GameController.dustPoints += 20;

			}
			else
			{
				GameController.dustPoints += 10;
			}

			PlayerPrefs.SetInt("dustPoints",GameController.dustPoints);
			other.gameObject.SetActive(false);
			SoundManager.Instance.PlaySound(6);
		}
		else if(other.CompareTag("Star"))
		{
			GameController.starPoints +=1;
			SoundManager.Instance.PlaySound(4);
			PlayerPrefs.SetInt("starPoints",GameController.starPoints);
			other.gameObject.SetActive(false);
		}
		else if(other.CompareTag("StarHero"))
		{
			GameController.heroStars +=1;
			PlayerPrefs.SetInt("heroStars",GameController.heroStars);
			SoundManager.Instance.PlaySound(4);
			other.gameObject.SetActive(false);
		}
		else if(other.CompareTag("StarLegend"))
		{
			GameController.legendStars +=1;
			PlayerPrefs.SetInt("legendStars",GameController.legendStars);
			SoundManager.Instance.PlaySound(4);
			other.gameObject.SetActive(false);
		}

	}		
}
