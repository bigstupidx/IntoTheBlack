using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarShotPanel : MonoBehaviour {

	public Image photo;
	public Text title;
	
	void Awake()
	{
		gameObject.SetActive(false);
	}
	// Use this for initialization

	public void SetPhoto(int _id)
	{
		title.text = StarLoader.stars[_id].name;
		photo.overrideSprite = StarLoader.stars[_id].sprite;
	}

	public void SetDisable()
	{
		gameObject.SetActive(false);
	}
	

}
