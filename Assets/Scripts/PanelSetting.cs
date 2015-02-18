using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

public class PanelSetting : MonoBehaviour 
{
	public Text title;
	public Text effectSound;
	public Text bgm;
	public Text credits;
	public Text restart;

	// Use this for initialization
	void Start () 
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		title.text = thisLanguageManager.GetTextValue("UI.SettingTitle");
		effectSound.text = thisLanguageManager.GetTextValue("UI.EffectSound");
		bgm.text = thisLanguageManager.GetTextValue("UI.BGM");
		credits.text = thisLanguageManager.GetTextValue("UI.Credit");
		restart.text = thisLanguageManager.GetTextValue("UI.Restart");
	}
	

}
