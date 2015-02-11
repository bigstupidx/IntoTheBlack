using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelUpgrade : MonoBehaviour 
{
	public GameObject shotUpgrade;
	public GameObject engineUpgrade;
	public GameObject timeUpgrade;
	public Sprite unlockSprite;
	private int lastShotLevel = -1;
	private int lastEngineLevel = -1;
	private int lastTimeLevel = -1;

	// Use this for initialization
	void OnEnable () 
	{
		UpdateButton();
	}

	void Update()
	{
		if(lastShotLevel != GameController.shotLevel || lastEngineLevel != GameController.engineLevel || lastTimeLevel != GameController.timeLevel)
		{
			UpdateButton();
			lastShotLevel = GameController.shotLevel;
			lastEngineLevel = GameController.engineLevel;
			lastTimeLevel = GameController.timeLevel;
		}
	}

	// Update is called once per frame
	public void UpdateButton ()
	{
		UpgradeButton shotUpgradeButton = shotUpgrade.GetComponent<UpgradeButton>();
		UpgradeButton engineUpgradeButton = engineUpgrade.GetComponent<UpgradeButton>();
		UpgradeButton timeUpgradeButton = timeUpgrade.GetComponent<UpgradeButton>();

		Upgrade upgrade;

		upgrade = UpgradeManager.Instance.UpgradeCheck("shot");
		if (!upgrade.isUnlock)
		{
			shotUpgradeButton.lableText.text = "[locked]";
			shotUpgradeButton.levelText.text = GameController.shotLevel.ToString();

			//shotUpgradeButton.costText.text = "외계인 공략";
			//shotUpgradeButton.button.enabled = false;
			shotUpgradeButton.icon.overrideSprite = unlockSprite;
		}
		else if (GameController.shotLevel == GameController.maxShotLevel)
		{
			shotUpgradeButton.lableText.text = "무기가 모두 업그레이드 되었습니다.";
			shotUpgradeButton.levelText.text = GameController.shotLevel.ToString();
			
			shotUpgradeButton.costText.text = "MAX";
			shotUpgradeButton.button.enabled = false;
			shotUpgradeButton.icon.overrideSprite = null;
			
		}
		else
		{
			shotUpgradeButton.levelText.text = GameController.shotLevel.ToString();

			shotUpgradeButton.lableText.text = "무기 대미지가 증가 합니다. (별자리를 모아야 합니다.)";

			string legendCost="";
			string heroCost="";
			string commonCost="";

			if (upgrade.legend !=0)
			{
				if(GameController.legendStars >= upgrade.legend)
				{
					legendCost = string.Format("<color=orange>전설</color>: <color=cyan>{0}</color>",upgrade.legend);
				}
				else
				{
					legendCost = string.Format("<color=orange>전설</color>: <color=red>{0}</color>", upgrade.legend);
				}
			}
			
			if (upgrade.hero !=0)
			{
				if(GameController.heroStars >= upgrade.hero)
				{
					heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=cyan>{0}</color>",upgrade.hero);
				}
				else
				{
					heroCost = string.Format("<color=#FF1E64>영웅</color>: <color=red>{0}</color>", upgrade.hero);
				}
			}
			
			if (upgrade.common !=0)
			{
				if(GameController.starPoints >= upgrade.common)
				{
					commonCost = string.Format("<color=yellow>일반</color>: <color=cyan>{0}</color>",upgrade.common);
				}
				else
				{
					commonCost = string.Format("<color=yellow>일반</color>: <color=red>{0}</color>", upgrade.common);
				}
			}
			shotUpgradeButton.costText.text = string.Format("{0} {1} {2}", legendCost, heroCost, commonCost);

			shotUpgradeButton.button.enabled = true;
			shotUpgradeButton.icon.overrideSprite = null;
		}


		upgrade = UpgradeManager.Instance.UpgradeCheck("engine");
		if (!upgrade.isUnlock)
		{
	
			engineUpgradeButton.levelText.text = GameController.engineLevel.ToString();
			engineUpgradeButton.costText.color = Color.yellow;

			engineUpgradeButton.lableText.text = "행성의 인력을 이용해 탐사선의 속도를 향상 시킵니다.\n [잠겨있음]";

			engineUpgradeButton.costText.text = string.Concat(StarLoader.unlockLevel + " 레벨 별자리 모두 수집");

			engineUpgradeButton.button.enabled = false;
			engineUpgradeButton.icon.overrideSprite = unlockSprite;
		}
		else if (GameController.engineLevel == GameController.maxEngineLevel)
		{
			engineUpgradeButton.lableText.text = "속도가 모두 업그레이드 되었습니다.";
			engineUpgradeButton.levelText.text = GameController.engineLevel.ToString();

			engineUpgradeButton.costText.text = "MAX";
			engineUpgradeButton.button.enabled = false;
			engineUpgradeButton.icon.overrideSprite = null;
		}
		else
		{
			engineUpgradeButton.levelText.text = GameController.engineLevel.ToString();
			engineUpgradeButton.lableText.text = "탐사선의 속도가 빨라 집니다.";

			if (GameController.dustPoints >= upgrade.dust)
			{
				engineUpgradeButton.costText.text = string.Format("<color=cyan>{0}</color>", upgrade.dust);
			}
			else
			{
				engineUpgradeButton.costText.text = string.Format("<color=red>{0}</color>", upgrade.dust);
			}
				engineUpgradeButton.button.enabled = true;
				engineUpgradeButton.icon.overrideSprite = null;
		}


		upgrade = UpgradeManager.Instance.UpgradeCheck("time");
		if (!upgrade.isUnlock)
		{
			timeUpgradeButton.lableText.text = "시간 가속을 개발하지 못했습니다.[잠겨있음]";
			timeUpgradeButton.levelText.text = GameController.timeLevel.ToString();

			timeUpgradeButton.costText.text = "달에 도착해야 합니다.";
			//timeUpgradeButton.costText.text = string.Concat(StarLoader.unlockLevel + " 레벨 별자리 모두 수집");
			timeUpgradeButton.button.enabled = false;
			timeUpgradeButton.icon.overrideSprite = unlockSprite;
		}		
		else if (GameController.timeLevel == GameController.maxTimeLevel)
		{
			timeUpgradeButton.levelText.text = GameController.timeLevel.ToString();
			timeUpgradeButton.lableText.text = "시간가속이 모두 업그레이드 되었습니다.";
			timeUpgradeButton.costText.text = "MAX";
			timeUpgradeButton.button.enabled = false;
			timeUpgradeButton.icon.overrideSprite = null;
		}
		else
		{

			timeUpgradeButton.levelText.text = GameController.timeLevel.ToString();
			timeUpgradeButton.lableText.text = "시간을 빠르게 가속 합니다.\n 실제 시간 보다 빠르게 진행 됩니다.";

			if (GameController.dustPoints >= upgrade.dust)
			{
				timeUpgradeButton.costText.text = string.Format("<color=cyan>{0}</color>", upgrade.dust);
			}
			else
			{
				timeUpgradeButton.costText.text = string.Format("<color=red>{0}</color>", upgrade.dust);
			}
			timeUpgradeButton.button.enabled = true;
			timeUpgradeButton.icon.overrideSprite = null;
		}

	}
}
