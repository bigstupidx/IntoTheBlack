using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RapidShot : MonoBehaviour 
{
	public float coolTime;
	public float timer;
	public Slider coolTimeSlider;
	public Button skillButton;
	
	void Awake()
	{
		skillButton.interactable = false;
		coolTimeSlider.maxValue = coolTime;
		timer = 100f;
	}
	
	void Update()
	{
		timer += Time.deltaTime;
		coolTimeSlider.value = timer;
		
		if (timer >= coolTime && !skillButton.interactable)
		{
			skillButton.interactable = true;
		}

		if(GameController.autoFight == true && skillButton.interactable)
			SkillFire();
	}
	
	public void SkillFire()
	{
		skillButton.interactable = false;
		PlayerController.Instance.RapidShot(3f);
		timer = 0;
	}
}
