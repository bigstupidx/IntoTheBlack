using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EMPButton : MonoBehaviour 
{

	public GameObject emp;
	public float coolTime;
	public float timer;
	public Slider coolTimeSlider;
	public Button skillButton;

	void Awake()
	{
		skillButton.interactable = false;
		coolTimeSlider.maxValue = coolTime;
		emp.SetActive(false);
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
		timer = 0;
		SoundManager.Instance.PlaySound(12);
		emp.SetActive(true);
		
	}
}
