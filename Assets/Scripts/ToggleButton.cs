using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {

	public Sprite toggleSprite;
	private Image toggleButtonImage;

	private Button button;
	private bool isToggle;

	void Start()
	{
		Button button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(() => Toggle());
	}

	void Toggle()
	{
		isToggle = !isToggle;

		Image toggleButtonImage = gameObject.GetComponent<Image>();

		if(isToggle)
		{
			toggleButtonImage.overrideSprite = toggleSprite;
		}
		else
		{
			toggleButtonImage.overrideSprite = null;
		}

	}
}


