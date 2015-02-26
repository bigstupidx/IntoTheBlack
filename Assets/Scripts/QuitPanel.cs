using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;


public class QuitPanel : MonoBehaviour 
{
	public Text areYouSure;
	public Text yourShip;
	public Text Yes;
	public Text No;

	// Use this for initialization
	void Start () 
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;

		areYouSure.text = thisLanguageManager.GetTextValue("UI.QuitGame");
		yourShip.text = thisLanguageManager.GetTextValue("UI.Keepgoing");
		Yes.text = thisLanguageManager.GetTextValue("UI.Yes");
		No.text = thisLanguageManager.GetTextValue("UI.No");
	}

	void OnEnable()
	{
		Time.timeScale = 0;
	}

	void Update()
	{
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				gameObject.SetActive(false);
			}
		}
	}


	void OnDisable()
	{
		Time.timeScale = 1;
	}
	// Update is called once per frame

}
