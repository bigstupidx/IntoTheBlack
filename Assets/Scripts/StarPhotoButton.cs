using UnityEngine;
using System.Collections;

public class StarPhotoButton : MonoBehaviour 
{
	public int id;
	public bool unlocked;

	public void ShowBigPhoto()
	{
		GameObject temp = GameObject.Find("Photo Panel");
		StarPhoto script = temp.GetComponent<StarPhoto>();

//		GameObject BigPhoto = GameObject.Find("BigPhotoPanel");
//		if(BigPhoto !=null)
//		{
//			BigPhoto.SetActive(true);
//		}
//		else
//		{
//			Debug.Log ("Can Find...");
//		}


		//BigPhoto BigPhotoScript = BigPhoto.GetComponent<BigPhoto>();


		if (unlocked)
		{
			script.OpenBigPhoto(id);

		}	
		else
		{
			SoundManager.Instance.PlaySound(4);
		}
	}
}
