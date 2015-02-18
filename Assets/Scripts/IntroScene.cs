using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SmartLocalization;

public class IntroScene : MonoBehaviour
{
	public Text presstoStart;

	void Start()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		SmartCultureInfo cultureInfo = thisLanguageManager.GetSupportedSystemLanguage();
		
		if(thisLanguageManager.IsLanguageSupportedEnglishName(cultureInfo.englishName))
		{
			thisLanguageManager.ChangeLanguage(cultureInfo.languageCode);
			//thisLanguageManager.ChangeLanguage("ja");
		}
		else
		{
			Debug.Log("Language is not supported");
			thisLanguageManager.ChangeLanguage("en");
		}

		presstoStart.text = thisLanguageManager.GetTextValue("UI.PresstoStart");
	}

	public void StartGame()
	{
		Application.LoadLevel("LoadingScene");
	}

}
