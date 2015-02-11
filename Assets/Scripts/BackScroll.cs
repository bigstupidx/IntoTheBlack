using UnityEngine;
using System.Collections;

public class BackScroll : MonoBehaviour 
{
	public float scrollSpeed;
	private float offset;
	private float addOffset;
	public bool applyBooster;

	void Update()
	{

		if(applyBooster)
		{
			if(GameController.engineLevel < 10 && GameController.timeLevel < 10)
			{
				addOffset = Time.deltaTime * scrollSpeed * (GameController.engineLevel + GameController.timeLevel + GameController.scrollModifier);
			}
			else 
			{
				addOffset = Time.deltaTime * scrollSpeed * (20 + GameController.scrollModifier);
			}

			offset += addOffset;
		}
		else
		{
			if(GameController.engineLevel < 10 && GameController.timeLevel < 10)
			{
				offset += Time.deltaTime * scrollSpeed * (GameController.engineLevel + GameController.timeLevel);
			}
			else
			{
				offset += Time.deltaTime * scrollSpeed * (20);
			}
		}

		if (offset > 1 ) 
		{
			offset = 1 - offset;
		}

		renderer.material.mainTextureOffset = new Vector2(0,offset);
	}


}
