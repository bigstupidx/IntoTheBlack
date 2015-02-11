using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwingByButton : MonoBehaviour 
{
	public Button button;

//	public Text swingByLevel;
	public Text swingByTime;

	// Use this for initialization
	void Start ()
	{

		Button button = gameObject.GetComponent<Button>();
		button.interactable = false;

	}
	
	// Update is called once per frame
	void Update ()
	{

		if(GameController.autoFight && button.interactable && !GameController.ingSwing)
		{
			GameController.Instance.SwingBy();
		}

		if (GameController.isSwingByCharged | GameController.ingSwing)
		{
			swingByTime.text = string.Concat (GameController.swingByTime.ToString("N0"), " 초");
		}

		if (!GameController.isSwingByCharged && !GameController.ingSwing)
		{
//			swingByLevel.text = ""; 
			swingByTime.text = "";

		}
	}
}
