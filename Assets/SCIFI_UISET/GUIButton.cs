using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIButton : MonoBehaviour {

	public Rect buttonPosition;
	public List<Texture2D> ListTextureButton = new List<Texture2D>();
	//public GUITexture
	public GUIStyle ButtonStyle;
	private int id,maxId;


	private float TimerDown=0f,Timer=0.5f;

	void Start(){
		TimerDown = Timer; //Задаем временной переменной значение которое нужно отсчитать
		maxId=ListTextureButton.Count;
		ButtonStyle.normal.background=ListTextureButton[0];
	}

	void OnGUI(){
		GUI.Button(buttonPosition,"",ButtonStyle);

	}

	void Update ()
	{
		if(TimerDown > 0) TimerDown -= Time.deltaTime; //Если время которое нужно отсчитать еще осталось убавляем от него время обновления экрана (в одну секунду будет убавляться полная единица)
		if(TimerDown < 0) TimerDown = 0; //Если временная переменная ушла в отрицательное число (все возможно) то приравниваем ее к нулю
		if(TimerDown == 0)
		{
			TimerDown = Timer; //Благодаря этой строке таймер запустится заново после выполнения всех действий в скобках
			ButtonStyle.normal.background=ListTextureButton[id];
			id++;
			if(id==maxId) id=0;
		}

}
}
