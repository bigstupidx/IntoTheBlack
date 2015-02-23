using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using SmartLocalization;

public struct DialogData 
{
	public int id;
	public int pilot;
	public string talk;
}

public struct GoalData
{
	public int id;
	public string name;
	public float distance;
	public int reward;
	public string rewardText;
}

public class Dialog
{
	[XmlAttribute("id")]
	public int id;
	[XmlAttribute("pilot")]
	public int pilot;
	public string Talk;
}

[XmlRoot("DialogCollection")]
public class DialogContainer
{
	[XmlArray("Dialogs"),XmlArrayItem("Dialog")]
	public Dialog[] Dialogs;
}

public class Goal
{
	[XmlAttribute("id")]
	public int id;
	[XmlAttribute("name")]
	public string name;
	[XmlAttribute("distance")]
	public float distance;
	[XmlAttribute("reward")]
	public int reward;
	
	public string RewardText;
}



[XmlRoot("GoalCollection")]
public class GoalContainer
{
	[XmlArray("Goals"),XmlArrayItem("Goal")]
	public Goal[] Goals;
	
}


public class DialogManager : MonoBehaviour
{
	private static DialogManager _instance;
	
	public static DialogManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(DialogManager)) as DialogManager;
				if(!_instance)
				{
					//					GameObject container = new GameObject();
					//					container.name = "GameControllerContainer";
					//					_instance = container.AddComponent(typeof(GameController)) as GameController;
				}
			}
			
			return _instance;
		}
	}

	public static DialogData[] m_Dialogs;
	public static GoalData[] m_Goals;
	public static DialogData[] colonyDialogs;

	public Sprite[] pilotSprite;
	public GameObject dialogWindow;

	public static bool isDialogPop;
	public int colonyCount = 0;

	void Awake()
	{
		TextAsset dialogAsset;
		TextAsset goalAsset;
		TextAsset colonyAsset;

		isDialogPop = false;
		colonyCount = PlayerPrefs.GetInt("colonyCount");

		LanguageManager thisLanguageManager = LanguageManager.Instance;
		SmartCultureInfo cultureInfo = thisLanguageManager.GetSupportedSystemLanguage();
		
		if(thisLanguageManager.IsLanguageSupportedEnglishName(cultureInfo.englishName))
		{
			thisLanguageManager.ChangeLanguage(cultureInfo.languageCode);
			//thisLanguageManager.ChangeLanguage("fr");
		}
		else
		{
			Debug.Log("Language is not supported");
			thisLanguageManager.ChangeLanguage("en");
		}

		dialogAsset = Resources.Load("dialogCollection") as TextAsset;

		XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer));
		StringReader stringReader = new StringReader(dialogAsset.text);
		XmlTextReader xmlReader = new XmlTextReader(stringReader);

		var container = serializer.Deserialize(xmlReader) as DialogContainer;

		// suck -- already too many static m_dialogs used in other script 
		m_Dialogs = new DialogData[container.Dialogs.Length];


		for (int i = 0; i < container.Dialogs.Length; i++)
		{
			m_Dialogs[i].id = container.Dialogs[i].id;
			m_Dialogs[i].pilot = container.Dialogs[i].pilot;
			m_Dialogs[i].talk = container.Dialogs[i].Talk;
		}


		goalAsset = Resources.Load("goalCollection") as TextAsset;
		
		serializer = new XmlSerializer(typeof(GoalContainer));
		stringReader = new StringReader(goalAsset.text);
		xmlReader = new XmlTextReader(stringReader);
		
		var g_container = serializer.Deserialize(xmlReader) as GoalContainer;

		m_Goals = new GoalData[g_container.Goals.Length];

		for (int i = 0; i < g_container.Goals.Length; i++)
		{
			m_Goals[i].id = g_container.Goals[i].id;
			m_Goals[i].name = g_container.Goals[i].name;
			m_Goals[i].distance = g_container.Goals[i].distance;
			m_Goals[i].reward = g_container.Goals[i].reward;
			m_Goals[i].rewardText = g_container.Goals[i].RewardText;
		}
	
		colonyAsset = Resources.Load("colonyCollection") as TextAsset;

		serializer = new XmlSerializer(typeof(DialogContainer));
		stringReader = new StringReader(colonyAsset.text);
		xmlReader = new XmlTextReader(stringReader);

		var c_container = serializer.Deserialize(xmlReader) as DialogContainer;
		
		// suck -- already too many static m_dialogs used in other script 
		colonyDialogs = new DialogData[c_container.Dialogs.Length];
		
		
		for (int i = 0; i < container.Dialogs.Length; i++)
		{
			colonyDialogs[i].id = c_container.Dialogs[i].id;
			colonyDialogs[i].pilot = c_container.Dialogs[i].pilot;
			colonyDialogs[i].talk = c_container.Dialogs[i].Talk;
		}

		//Debug.Log ("dialog : " + m_Dialogs[1].talk.ToString());
		//Debug.Log (colonyDialogs[1].talk);

		dialogWindow.SetActive(false);

		g_container = null;
		serializer = null;
		stringReader = null;
		xmlReader = null;
		goalAsset = null;
		dialogAsset = null;


	}


	public void ColonyEvent()
	{
		if (colonyCount <= 35)
		{
			SetDialog(DialogManager.colonyDialogs[colonyCount].pilot, DialogManager.colonyDialogs[colonyCount].talk);
			colonyCount += 1;
			PlayerPrefs.SetInt("colonyCount",colonyCount);
		}
	}
	
	public void SetDialog(int _pilotNum, string _dialog)
	{
		EventDialog window = dialogWindow.GetComponent<EventDialog>();

		window.dialog.text = _dialog;
		window.character.overrideSprite = pilotSprite[_pilotNum-1];

		dialogWindow.SetActive(true);
	
	}

	public void CloseDialog()
	{
		isDialogPop = false;
		dialogWindow.SetActive(false);
	}
}




