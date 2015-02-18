using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using SmartLocalization;

public class shotCutText : MonoBehaviour 
{
	public Text shot;
	public Text engine;
	public Text time;
	private Upgrade upgrade;

	private int lastLegend = -1;
	private int lastHero = -1;
	private int lastStar = -1;
	private int lastDusts = -1;

	private string legendCost="";
	private string heroCost="";
	private string commonCost="";


	private string shotString;
	private string engineString;
	private string timeString;
	private string legend;
	private string hero;
	private string common;
	private string locked;

	private StringBuilder sb = new StringBuilder();

	// Update is called once per frame

	void Start()
	{
		LanguageManager thisLanguageManager = LanguageManager.Instance;
//		SmartCultureInfo cultureInfo = thisLanguageManager.GetSupportedSystemLanguage();

		shotString = thisLanguageManager.GetTextValue("UI.Weapon");
		engineString = thisLanguageManager.GetTextValue("UI.Engine");
		timeString = thisLanguageManager.GetTextValue("UI.Time");
		legend = thisLanguageManager.GetTextValue("UI.Legend");
		hero = thisLanguageManager.GetTextValue("UI.Hero");
		common = thisLanguageManager.GetTextValue("UI.Normal");
		locked = thisLanguageManager.GetTextValue("UI.Locked");
	}

	void Update()
	{
		if(GameController.starPoints != lastStar || GameController.heroStars != lastHero || GameController.legendStars != lastLegend || GameController.dustPoints != lastDusts)
		{ 
			UpdateShotCut();
			lastLegend = GameController.legendStars;
			lastHero = GameController.heroStars;
			lastStar = GameController.starPoints;
			lastDusts = GameController.dustPoints;
		}
	}

	void UpdateShotCut () 
	{
		upgrade = UpgradeManager.Instance.UpgradeCheck("shot");

		if(!upgrade.isUnlock)
		{
			sb.Length = 0;
			sb.AppendFormat("{0}: {1}", shotString, locked);
			shot.text= sb.ToString();
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{
			sb.Length = 0;
			sb.AppendFormat("{0}: MAX", shotString);
			shot.text= sb.ToString();
//			shot.text = "무기: MAX";
		}
		else
		{
			if(GameController.starPoints != lastStar || GameController.heroStars != lastHero || GameController.legendStars != lastLegend)
			{
				if(GameController.legendStars >= upgrade.legend)
				{
					sb.Length = 0;
					sb.AppendFormat("<color=orange>{0}</color>: <color=cyan>{1}</color>", legend, upgrade.legend);
					legendCost = sb.ToString();
					// legendCost = string.Format("<color=orange>전설</color>: <color=cyan>{0}</color>",upgrade.legend);
				}
				else
				{
					sb.Length = 0;
					sb.AppendFormat("<color=orange>{0}</color>: <color=red>{1}</color>", legend, upgrade.legend);
					legendCost = sb.ToString();

					//legendCost = string.Format("<color=orange>전설</color>: <color=red>{0}</color>", upgrade.legend);
				}

				if(GameController.heroStars >= upgrade.hero)
				{
					sb.Length=0;
					sb.AppendFormat("<color=#FF1E64>{0}</color>: <color=cyan>{1}</color>",hero ,upgrade.hero);
					heroCost = sb.ToString();
					
					//heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=cyan>{0}</color>",upgrade.hero);
				}
				else
				{
					sb.Length=0;
					sb.AppendFormat("<color=#FF1E64>{0}</color>: <color=red>{1}</color>",hero ,upgrade.hero);
					heroCost = sb.ToString();

					//heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=red>{0}</color>", upgrade.hero);
				}
			
				if(GameController.starPoints >= upgrade.common)
				{
					sb.Length=0;
					sb.AppendFormat("<color=yellow>{0}</color>: <color=cyan>{1}</color>", common, upgrade.common);
					commonCost = sb.ToString();

					//commonCost = string.Format("<color=yellow>일반</color>: <color=cyan>{0}</color>",upgrade.common);
				}
				else
				{
					sb.Length=0;
					sb.AppendFormat("<color=yellow>{0}</color>: <color=red>{1}</color>", common, upgrade.common);
					commonCost = sb.ToString();

					// commonCost = string.Format("<color=yellow>일반</color>: <color=red>{0}</color>", upgrade.common);
				}

				lastStar = GameController.starPoints;
				lastHero = GameController.heroStars;
				lastLegend = GameController.legendStars;
			}

			sb.Length = 0;
			sb.AppendFormat("{0}\n{1}\n{2}\n{3}", shotString, legendCost, heroCost, commonCost);

			if(!string.Equals(sb.ToString(), shot.text))
			{
				shot.text = sb.ToString();
			}
		}


		upgrade = UpgradeManager.Instance.UpgradeCheck("engine");
		
		if(!upgrade.isUnlock)
		{
			sb.Length = 0;
			sb.AppendFormat("{0}: {1}", engineString, locked);
			engine.text = sb.ToString();
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{
			sb.Length = 0;
			sb.AppendFormat("{0}: MAX", engineString);
			engine.text = sb.ToString();
		}
		else
		{
			if(upgrade.ispossible)
			{
				sb.Length = 0;
				sb.AppendFormat("{0}: <color=cyan>{0}</color>",engineString, upgrade.dust);

				if(!string.Equals(sb.ToString(), engine.text))
				{
					engine.text = sb.ToString();
				}
			}
			else
			{
				sb.Length = 0;
				sb.AppendFormat("{0}: <color=red>{0}</color>",engineString, upgrade.dust);

				if(!string.Equals(sb.ToString(), engine.text))
				{
					engine.text = sb.ToString();
				}
			}
		}

		upgrade = UpgradeManager.Instance.UpgradeCheck("time");
		
		if(!upgrade.isUnlock)
		{
			sb.Length = 0;
			sb.AppendFormat("{0}: {1}", timeString, locked);
			time.text = sb.ToString();
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{			
			sb.Length = 0;
			sb.AppendFormat("{0}: MAX", timeString);
			time.text = sb.ToString();

		}
		else
		{
			if(upgrade.ispossible)
			{
				sb.Length = 0;
				sb.AppendFormat("{0}: <color=cyan>{1}</color>", timeString, upgrade.dust);

				if(!string.Equals(sb.ToString(),time.text))
				{
					time.text = sb.ToString();
				}
			}
			else
			{
				sb.Length = 0;
				sb.AppendFormat("{0}: <color=red>{1}</color>", timeString, upgrade.dust);

				if(!string.Equals(sb.ToString(),time.text))
				{
					time.text = sb.ToString();
				}
			}
		}

	}
}
