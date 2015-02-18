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
	
	// Update is called once per frame
	void Update () {
	
	}
}
