using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class UpgradeLable : MonoBehaviour 
{

	Text text;
	public String upgradeType;

	void Awake()
	{
		text = GetComponent <Text> ();
	}
	
	void Update () 
	{
		switch (upgradeType)
		{
			case "shot":
				text.text = GameController.shotLevel.ToString("N0");
				break;
			case "engine":
				text.text = GameController.engineLevel.ToString("N0");
				break;
			case "time":
				text.text = GameController.timeLevel.ToString("N0");
				break;

			default:
				text.text = "XX";
			break;
		}
	}
}
