using UnityEngine;
using System.Collections;

public class spaceDust : MonoBehaviour 
{
	public ParticleSystem dustParticle;
	// Use this for initialization
	// Update is called once per frame
	void Update () 
	{
		dustParticle.startSpeed = Random.Range (1 * GameController.scrollModifier * 0.5f + 0.5f, 8 * GameController.scrollModifier * 0.5f);
	}
}
