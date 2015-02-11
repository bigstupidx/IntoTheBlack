using UnityEngine;
using System.Collections;

public class GuiMenu : MonoBehaviour {
	public GUISkin GUISkins;
	//public Rect GridButton;
	public GUIStyle StyleGridButton;
	public Texture2D lineTexture,Star,Panel2Texture,Panel3Texture,Panel4Texture,login,passw,barBg,bar0,barTitle;
	private string Text="Over 70% percent of Earth's \nsurface is covered with water, with the remainder consisting of continents and islands which together have many lakes and other sources of water that contribute to\nthe hydrosphere.";
	public float vSliderValue = 0.0F;
	public bool RememberMe=false;
	private int selGridInt = 0;
	private string[] selStrings=new string[8];
	private float loading=-217f,MiniLoading=-217f,MaxLoading=0,one=1f;


	private string loginInput="login",loginPassword="********";
	// Use this for initialization
	void Start () {

	}
	void OnGUI(){
		GUI.skin=GUISkins;
		GUI.DrawTexture(new Rect(40,150,300,300),Star);
	//Panel 2
		GUI.DrawTexture(new Rect(35,35,260,350),Panel2Texture);
		GUI.Label(new Rect(60,90,100,20),"Earth");
		GUI.DrawTexture(new Rect(60,115,180,1),lineTexture);
		GUI.TextArea(new Rect(60,120,180,240), Text, 400);
		GUI.DrawTexture(new Rect(60,250,180,1),lineTexture);
		vSliderValue = GUI.VerticalSlider(new Rect(256,114,14,137.2f), vSliderValue, 0.0F, 10.0F);
		
	//Panel 3
		GUI.DrawTexture(new Rect(295,269,235,130),Panel3Texture);
		selGridInt = GUI.SelectionGrid(new Rect(307,282,217,104.5f), selGridInt, selStrings, 4,StyleGridButton);

	//Panel 4
		GUI.DrawTexture(new Rect(300,45,243,174),Panel4Texture);
		loginInput = GUI.TextField(new Rect(350,80,150,22),loginInput);
		GUI.DrawTexture(new Rect(355,86,6,11),login);
		loginPassword = GUI.TextField(new Rect(350,110,150,22),loginPassword);
		GUI.DrawTexture(new Rect(355,115,9,12),passw);
		RememberMe = GUI.Toggle(new Rect(349, 139, 24, 24), RememberMe, "");
		GUI.Label(new Rect(375, 143, 100, 24),"Remember me");
		GUI.Button (new Rect (431,170,70,25), "Next");
		GUI.Button (new Rect (349,170,70,25), "Back");

		GUI.DrawTexture(new Rect(300,230,220,25),barBg);
		GUI.DrawTexture(new Rect(519,231.5f,loading,22),bar0);
		GUI.DrawTexture(new Rect(380,237,56,15),barTitle);
	}

	void Update(){

		if(loading>MaxLoading){
			one=-1;
		}
		if(loading<MiniLoading){
			one=1;
		}
		loading+=1.085f*one;

	}
}
