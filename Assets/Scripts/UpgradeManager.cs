using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public struct Upgrade
{
	public bool isUnlock;
	public int legend;
	public int hero;
	public int common;
	public int dust;
	public bool ispossible;
}

public class UpgradeManager : MonoBehaviour 
{
	private static UpgradeManager _instance;
	
	public static UpgradeManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = UpgradeManager.FindObjectOfType(typeof(UpgradeManager)) as UpgradeManager;
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

	public static int unlockShot;
	public static int unlockEngine;
	public static int unlockTime;


	// Use this for initialization
	void Start()
	{
		unlockShot = PlayerPrefs.GetInt("unlockShot");
		unlockShot = 31; // don't use shot unlock system now

		unlockEngine = PlayerPrefs.GetInt("unlockEngine");;
		if(unlockEngine < 10) unlockEngine = 10;

		unlockTime = PlayerPrefs.GetInt("unlockTime");

#if UNITY_EDITOR
//		unlockEngine = 101;
//		unlockTime = 301;
#endif
	
	
	}

	
	public Upgrade UpgradeCheck(string _type)
	{
		Upgrade upgrade;
		
		upgrade.isUnlock = false;
		upgrade.ispossible = false;
		upgrade.legend = 0;
		upgrade.hero = 0;
		upgrade.common = 0;
		upgrade.dust = 0;
		
		switch (_type)
		{
		case "shot" :
			if(unlockShot > GameController.shotLevel)
			{
				upgrade.isUnlock = true;
			}
			else
			{
				upgrade.isUnlock = false;
			}
			
			upgrade.legend = (int)(0.15f * Mathf.Pow(GameController.shotLevel, 2f) - 0.15f * GameController.shotLevel + 0.3f);
			if (GameController.shotLevel <10)
				upgrade.legend = 0;
			upgrade.hero = (int)(0.25f * Mathf.Pow(GameController.shotLevel, 2f) - 0.25f * GameController.shotLevel + 0.5f);
			if (GameController.shotLevel <5)
				upgrade.hero = 0;
			upgrade.common = (int)(0.5f * Mathf.Pow(GameController.shotLevel, 2f) - 0.5f * GameController.shotLevel + 1f);
			upgrade.dust = 0;
			
			if(GameController.legendStars >= upgrade.legend && GameController.heroStars >= upgrade.hero && GameController.starPoints >= upgrade.common && GameController.dustPoints >= upgrade.dust)
			{
				upgrade.ispossible = true;
			}
			else
			{
				upgrade.ispossible = false;
			}
			return upgrade;
			
		case "engine" :
			if(unlockEngine > GameController.engineLevel)
			{
				upgrade.isUnlock = true;
			}
			else
			{
				upgrade.isUnlock = false;
			}
			
			upgrade.legend = 0;
			upgrade.hero = 0;
			upgrade.common = 0;
			upgrade.dust = (GameController.engineLevel-1) * 70 + 250;
			
			if(GameController.legendStars >= upgrade.legend && GameController.heroStars >= upgrade.hero && GameController.starPoints >= upgrade.common && GameController.dustPoints >= upgrade.dust)
			{
				upgrade.ispossible = true;
			}
			else
			{
				upgrade.ispossible = false;
			}
			return upgrade;
			
		case "time" :
			if(unlockTime > GameController.timeLevel)
			{
				upgrade.isUnlock = true;
			}
			else
			{
				upgrade.isUnlock = false;
			}
			
			upgrade.legend = 0;
			upgrade.hero = 0;
			upgrade.common = 0;
			upgrade.dust = (GameController.timeLevel-1) * 230 + 500;
			
			if(GameController.legendStars >= upgrade.legend && GameController.heroStars >= upgrade.hero && GameController.starPoints >= upgrade.common && GameController.dustPoints >= upgrade.dust)
			{
				upgrade.ispossible = true;
			}
			else
			{
				upgrade.ispossible = false;
			}
			return upgrade;
		}
		
		return upgrade;
		
	}
	public void ShotUpgrade()
	{
		Upgrade upgrade;
		upgrade = UpgradeCheck("shot");

		if(upgrade.isUnlock)
		{
			if(upgrade.ispossible)
			{
				GameController.legendStars -= upgrade.legend;
				GameController.heroStars -= upgrade.hero;
				GameController.starPoints -= upgrade.common;
				GameController.dustPoints -= upgrade.dust;

				GameController.shotLevel +=1;
				PlayerPrefs.SetInt("shotLevel", GameController.shotLevel);

				UpgradeSuccess();
			}
			else
			{
				// you need to more resource;
				UpgradeFail();
			}
		}
		else
		{
			// you need to unlock;
			UpgradeFail();
		}
	}

	public void EngineUpgrade()
	{
		Upgrade upgrade;
		upgrade = UpgradeCheck("engine");
		
		if(upgrade.isUnlock)
		{
			if(upgrade.ispossible)
			{
				GameController.legendStars -= upgrade.legend;
				GameController.heroStars -= upgrade.hero;
				GameController.starPoints -= upgrade.common;
				GameController.dustPoints -= upgrade.dust;
				
				GameController.engineLevel +=1;
				PlayerPrefs.SetInt("engineLevel", GameController.engineLevel);

				UpgradeSuccess();
			}
			else
			{
				// you need to more resource;
				UpgradeFail();
			}
		}
		else
		{
			// you need to unlock;
			UpgradeFail();
		}
	}

	public void TimeUpgrade()
	{
		Upgrade upgrade;
		upgrade = UpgradeCheck("time");

		if(upgrade.isUnlock)
		{
			if(upgrade.ispossible)
			{
				GameController.legendStars -= upgrade.legend;
				GameController.heroStars -= upgrade.hero;
				GameController.starPoints -= upgrade.common;
				GameController.dustPoints -= upgrade.dust;
				
				GameController.timeLevel +=1;
				PlayerPrefs.SetInt("timeLevel", GameController.timeLevel);
				UpgradeSuccess();
			}
			else
			{
				// you need to more resource;
				UpgradeFail();
			}
		}
		else
		{
			// you need to unlock;
			UpgradeFail();
		}
	}

	public void UpgradeSuccess()
	{
		GameController.Instance.RefreshShip();
		GameController.Instance.SavePlayerData();
		SoundManager.Instance.PlaySound(11);
	}

	public void UpgradeFail()
	{
		SoundManager.Instance.PlaySound(4);
	}


}
