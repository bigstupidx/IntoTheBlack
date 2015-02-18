using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

public class NewAlbum : MonoBehaviour 
{
	public Text title;
	public Text description;

	void Start()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
		title.text = thisLanguageManager.GetTextValue("UI.AlbumTitle");
		description.text = thisLanguageManager.GetTextValue("UI.AlbumDesc");
	}

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void OnDisable()
	{
		Time.timeScale = 1;
	}
}
