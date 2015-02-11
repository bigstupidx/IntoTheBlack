using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

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

	private StringBuilder sb = new StringBuilder();

	// Update is called once per frame

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
			shot.text= "무기: Locked";
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{
			shot.text = "무기: MAX";
		}
		else
		{
			if(GameController.starPoints != lastStar || GameController.heroStars != lastHero || GameController.legendStars != lastLegend)
			{
				if(GameController.legendStars >= upgrade.legend)
				{
					sb.Length = 0;
					sb.AppendFormat("<color=orange>전설</color>: <color=cyan>{0}</color>",upgrade.legend);
					legendCost = sb.ToString();
					// legendCost = string.Format("<color=orange>전설</color>: <color=cyan>{0}</color>",upgrade.legend);
				}
				else
				{
					sb.Length = 0;
					sb.AppendFormat("<color=orange>전설</color>: <color=red>{0}</color>", upgrade.legend);
					legendCost = sb.ToString();

					//legendCost = string.Format("<color=orange>전설</color>: <color=red>{0}</color>", upgrade.legend);
				}

				if(GameController.heroStars >= upgrade.hero)
				{
					sb.Length=0;
					sb.AppendFormat("<color=#FF1E64>영웅</color>: <color=cyan>{0}</color>",upgrade.hero);
					heroCost = sb.ToString();
					
					//heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=cyan>{0}</color>",upgrade.hero);
				}
				else
				{
					sb.Length=0;
					sb.AppendFormat("<color=#FF1E64>영웅</color>: <color=red>{0}</color>", upgrade.hero);
					heroCost = sb.ToString();

					//heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=red>{0}</color>", upgrade.hero);
				}
			
				if(GameController.starPoints >= upgrade.common)
				{
					sb.Length=0;
					sb.AppendFormat("<color=yellow>일반</color>: <color=cyan>{0}</color>",upgrade.common);
					commonCost = sb.ToString();

					//commonCost = string.Format("<color=yellow>일반</color>: <color=cyan>{0}</color>",upgrade.common);
				}
				else
				{
					sb.Length=0;
					sb.AppendFormat("<color=yellow>일반</color>: <color=red>{0}</color>", upgrade.common);
					commonCost = sb.ToString();

					// commonCost = string.Format("<color=yellow>일반</color>: <color=red>{0}</color>", upgrade.common);
				}

				lastStar = GameController.starPoints;
				lastHero = GameController.heroStars;
				lastLegend = GameController.legendStars;
			}

			sb.Length = 0;
			sb.AppendFormat("무기\n{0}\n{1}\n{2}", legendCost, heroCost, commonCost);

			if(!string.Equals(sb.ToString(), shot.text))
			{
				shot.text = sb.ToString();
			}
		}


		upgrade = UpgradeManager.Instance.UpgradeCheck("engine");
		
		if(!upgrade.isUnlock)
		{
			engine.text = "속도: 잠김";
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{
			engine.text = "속도: MAX";
		}
		else
		{
			if(upgrade.ispossible)
			{
				sb.Length = 0;
				sb.AppendFormat("속도: <color=cyan>{0}</color>", upgrade.dust);

				if(!string.Equals(sb.ToString(), engine.text))
				{
					engine.text = sb.ToString();
				}
			}
			else
			{
				sb.Length = 0;
				sb.AppendFormat("속도: <color=red>{0}</color>", upgrade.dust);

				if(!string.Equals(sb.ToString(), engine.text))
				{
					engine.text = sb.ToString();
				}
			}
		}

		upgrade = UpgradeManager.Instance.UpgradeCheck("time");
		
		if(!upgrade.isUnlock)
		{
			time.text = "시간: 잠김";
		}
		else if(GameController.shotLevel == GameController.maxShotLevel)
		{
			time.text = "시간: MAX";
		}
		else
		{
			if(upgrade.ispossible)
			{
				sb.Length = 0;
				sb.AppendFormat("시간: <color=cyan>{0}</color>", upgrade.dust);

				if(!string.Equals(sb.ToString(),time.text))
				{
					time.text = sb.ToString();
				}
			}
			else
			{
				sb.Length = 0;
				sb.AppendFormat("시간: <color=red>{0}</color>", upgrade.dust);

				if(!string.Equals(sb.ToString(),time.text))
				{
					time.text = sb.ToString();
				}
			}
		}

	}
}
