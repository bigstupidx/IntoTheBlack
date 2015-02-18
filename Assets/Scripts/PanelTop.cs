using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using SmartLocalization;

public class PanelTop : MonoBehaviour 
{

	public Text dustText;
	public Text starText;
	public Text shipSpeedText;
	public Text distanceText;
	public Text journeyTimeText;
	public Text nextGoalText;
	public Text nextGoalDistanceText;

	public Animator panelAni;
	private float leftDistance;

	private string _speedFormat;
	private string _timeFormat1;
	private string _timeFormat2;
	private string _distanceFormat;
	private string _unit;
	private string _next;

//	private float lastDust;
	private string timerFormatted;
	private double years =0, days=0;

//	private float tempDustPoints;

	public Text colonyText;
	public Slider colonySlider;

	public Slider nextSlider;

	private int lastDustPoints = -1;
	private int lastLegend = -1;
	private int lastHero = -1;
	private int lastStar = -1;
	private float lastSpeed = -1f;

	private string lastGoalName = "";

	private StringBuilder sb = new StringBuilder();
	private System.TimeSpan t;

	void Start()
	{
//		tempDustPoints = 0;
		LanguageManager thisLanguageManager = LanguageManager.Instance;

		sb.Length = 0;
		sb.AppendFormat(" Km/h [{0}] ", thisLanguageManager.GetTextValue("UI.ShipSpeed"));
		_speedFormat = sb.ToString();

		sb.Length = 0;
		sb.AppendFormat("[{0}] [", thisLanguageManager.GetTextValue("UI.JourneyTime"));
		_timeFormat1 = sb.ToString();

		sb.Length = 0;
		sb.AppendFormat(" {0}]", thisLanguageManager.GetTextValue("UI.Accel"));
		_timeFormat2 = sb.ToString();

		sb.Length = 0;
		sb.AppendFormat("[{0}] ", thisLanguageManager.GetTextValue("UI.JourneyDistance"));
		_distanceFormat = sb.ToString();

		_unit = " Km";

		sb.Length = 0;
		sb.AppendFormat("[{0}] ", thisLanguageManager.GetTextValue("UI.NextDestination"));
		_next = sb.ToString();

		colonyText.text = thisLanguageManager.GetTextValue("UI.ColonyProgress");
	}

	void Update () 
	{
		if(lastDustPoints != GameController.dustPoints)
		{
			dustText.text = GameController.dustPoints.ToString("N0");
			lastDustPoints = GameController.dustPoints;
		}

		if(lastLegend != GameController.legendStars || lastHero != GameController.heroStars || lastStar != GameController.starPoints)
		{
			starText.text = string.Format("<color=orange>{0}</color>/<color=#FF1E64> {1}</color>/<color=yellow> {2}</color>", GameController.legendStars, GameController.heroStars, GameController.starPoints);
			lastLegend = GameController.legendStars;
			lastHero = GameController.heroStars;
			lastStar = GameController.starPoints;
		}


		float speed = GameController.engineSpeed;
		// speed += Random.Range (0,10); // to display more realistic

		if (speed != lastSpeed)
		{

			if(GameController.fireLevel > 0 )
			{
				shipSpeedText.text = string.Format("<color=red>{0}</color>{1}", speed.ToString("N0"), _speedFormat);
			}
			else
			{
				shipSpeedText.text = string.Format("<color=#00ff00>{0}</color>{1}", speed, _speedFormat);
			}

			lastSpeed = GameController.engineSpeed;
		}

		// display distance
		sb.Length = 0;
		sb.AppendFormat("<color=cyan>{0}</color>{1}",GameController.distanceFromEarth.ToString("N0"), _unit, _distanceFormat);
		distanceText.text = sb.ToString();

		//distanceText.text = string.Format("<color=cyan>{0}</color>{1}",GameController.distanceFromEarth.ToString("N0"), _unit, _distanceFormat);

		// display time
		t = System.TimeSpan.FromSeconds(GameController.journeyTime);

		sb.Length = 0;
		sb.AppendFormat("{0:N0}Days {1:D}h:{2:D}m:{3:D}s{4}{5}{6}", days,t.Hours, t.Minutes, t.Seconds, _timeFormat1, GameController.timeLevel, _timeFormat2);
		journeyTimeText.text = sb.ToString();

		// timerFormatted = string.Format("{0:N0}Days {1:D}h:{2:D}m:{3:D}s{4}{5}{6}", days,t.Hours, t.Minutes, t.Seconds, _timeFormat[0], GameController.timeLevel, _timeFormat[1]);
		//journeyTimeText.text =  timerFormatted;

		// display NextGoal
		if(!string.Equals(lastGoalName, GameController.nextGoalName))
		{
			nextGoalText.text = string.Concat (GameController.nextGoalName, _next);
		}

		leftDistance = GameController.nextGoalDistance - GameController.distanceFromEarth;

		sb.Length = 0;
		sb.AppendFormat("{0}{1}",leftDistance.ToString("N0"), _unit);
		nextGoalDistanceText.text = sb.ToString();

		//nextGoalDistanceText.text = string.Format("{0}{1}",leftDistance.ToString("N0"), _unit);


		// it is overwork in update cycle.. i wish to improve it.
		nextSlider.value = 1 - (leftDistance / GameController.distanceToNext);
		colonySlider.value = GameController.colonyProgress;

	}

	public void ToggleWindow()
	{
		if(panelAni.GetBool("isHidden"))
		{
			panelAni.SetBool("isHidden", false);
		}
		else
		{
			panelAni.SetBool("isHidden", true);
		}
	}
}
