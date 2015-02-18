using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

public class PanelSNS : MonoBehaviour 
{
	public Text title;
	public Text facebook;
	public Text googleLeader;
	public Text googleAchievement;

	// Use this for initialization
	void Start () 
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		title.text = thisLanguageManager.GetTextValue("UI.SNSTitle");
		facebook.text = thisLanguageManager.GetTextValue("UI.SNSFacebook");
		googleLeader.text = thisLanguageManager.GetTextValue("UI.GoogleLeader");
		googleAchievement.text = thisLanguageManager.GetTextValue("UI.GoogleAchievement");
	}
	
	// Update is called once per frame

}
