using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarShotCanvas : MonoBehaviour 
{
	public Image photo;
	public Text title;

	void Awake()
	{
		gameObject.SetActive(false);
	}
	// Use this for initialization

	public void SetDisable()
	{
		gameObject.SetActive(false);
	}


}
