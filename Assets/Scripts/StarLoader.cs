using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using SmartLocalization;

// starcard contains level,type,sprite
public struct StarData
{
	public int id;
	public int level;
	public string name;
	public string desc;
	public Sprite sprite;
}

public class Star
{
	[XmlAttribute("id")]
	public int id;
	[XmlAttribute("level")]
	public int level;
	[XmlAttribute("filename")]
	public string filename;
	[XmlAttribute("name")]
	public string name;
	public string Desc;
}

[XmlRoot("StarCollection")]
public class StarContainer
{
	[XmlArray("Stars"),XmlArrayItem("Star")]
	public Star[] stars; // it is possible to use List here
}

[Serializable]
public struct StarAlbum
{
	public bool isCommon;
	public bool isEpic;
	public bool isLegend;
}


public class StarLoader : MonoBehaviour
{
	private static StarLoader _instance;
	public static StarLoader Instance
	{
		get
		{
			if(_instance == null)
			{
				// _instance = GameObject.FindObjectOfType(typeof(SpawnController)) as SpawnController;
				
				_instance = GameObject.FindObjectOfType<StarLoader>();
				
				if(!_instance)
				{
					//					GameObject container = new GameObject();
					//					container.name = "SpawnControllerContainer";
					//					_instance = container.AddComponent(typeof(SpawnController)) as SpawnController;
				}
			}
			
			return _instance;
		}
	}


	private TextAsset starAsset;
	public static StarData[] stars;
	public GameObject scrollAlbum;

	public static StarAlbum[] starAlbum;
	public static int unlockLevel;
	public static int albumCount;
	public static int sessionAlbumCount;

	void Awake()
	{
		starAlbum = new StarAlbum[88];

		unlockLevel = PlayerPrefs.GetInt("unlockLevel");

		if(unlockLevel == 0)
		{
			unlockLevel = 1;
		}

		var data = PlayerPrefs.GetString("starAlbum");

		if(!String.IsNullOrEmpty(data))
		{
			var b = new BinaryFormatter();
			var m = new MemoryStream(Convert.FromBase64String(data));

			starAlbum = b.Deserialize(m) as StarAlbum[];
		}

		LanguageManager thisLanguageManager = LanguageManager.Instance;
		SmartCultureInfo cultureInfo = thisLanguageManager.GetSupportedSystemLanguage();
		
//		if(thisLanguageManager.IsLanguageSupportedEnglishName(cultureInfo.englishName))
//		{
//			thisLanguageManager.ChangeLanguage(cultureInfo.languageCode);
//			//thisLanguageManager.ChangeLanguage("fr");
//		}
//		else
//		{
//			Debug.Log("Language is not supported");
//			thisLanguageManager.ChangeLanguage("en");
//		}
		
		if(cultureInfo.englishName == "Korean")
		{
			starAsset = Resources.Load("starCollection") as TextAsset;
		}
		else
		{
			starAsset = Resources.Load("starCollection_en") as TextAsset;
		}

		//starAsset = Resources.Load("starCollection") as TextAsset;
		
		XmlSerializer serializer = new XmlSerializer(typeof(StarContainer));
		StringReader stringReader = new StringReader(starAsset.text);
		XmlTextReader xmlReader = new XmlTextReader(stringReader);
		
		var container = serializer.Deserialize(xmlReader) as StarContainer;
		stringReader.Close();

		stars = new StarData[container.stars.Length];
		
		for (int i = 0; i < container.stars.Length; i++)
		{
			stars[i].id = container.stars[i].id;
			stars[i].level = container.stars[i].level;
			stars[i].name = container.stars[i].name;
			stars[i].sprite = Resources.Load<Sprite>("Constellation/" + container.stars[i].filename);
			stars[i].desc = container.stars[i].Desc;
		}

		serializer = null;
		stringReader = null;
		xmlReader = null;
		container = null;
		starAsset = null;

	}

	public void UpdateAlbum(int _id, int _type)
	{
		switch(_type)
		{
		case 1:
			starAlbum[_id].isCommon = true;
			break;
		case 2:
			starAlbum[_id].isEpic = true;
			break;
		case 3:
			starAlbum[_id].isLegend = true;
			break;
		default:
			break;
		}

		albumCount = 0;
		sessionAlbumCount +=1;

		for (int i = 0; i < StarLoader.stars.Length; i++)
		{
			if (starAlbum[i].isCommon == true)
			{	
				albumCount += 1;
			}
		}

		if (unlockLevel < 9)
		{
			if (albumCount >= unlockLevel * 10)
			{
				unlockLevel += 1;
				UpgradeManager.unlockEngine = unlockLevel * 10;

				sessionAlbumCount = 0; // modify star album correct prob
				NoticeManager.Instance.SetNotice("현재 레벨의 별자리를 모두 획득하셨습니다. 앞으로 새로운 별자리가 출현 합니다.\n  엔진 업그레이드 제한이 해제되었습니다.",5);
				GameController.Instance.SavePlayerData();
			}
		}
		else if (unlockLevel == 9)
		{
			if (albumCount == StarLoader.starAlbum.Length)
			{
				UpgradeManager.unlockEngine = 101;
			}
		}


		SaveAlbum();
	}

	void SaveAlbum()
	{

		var b = new BinaryFormatter();
		var m = new MemoryStream();
		
		b.Serialize(m,starAlbum);
		
		PlayerPrefs.SetString("starAlbum", Convert.ToBase64String(m.GetBuffer()));
		PlayerPrefs.SetInt ("unlockLevel", unlockLevel);

	}

}
