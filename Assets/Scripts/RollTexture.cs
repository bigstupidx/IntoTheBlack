using UnityEngine;
using System.Collections;

public class RollTexture : MonoBehaviour {

	public float scrollSpeed;

	// Use this for initialization

	// Update is called once per frame
	void Update () 
	{
		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
	}
}
