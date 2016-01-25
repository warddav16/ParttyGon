using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Powerup : MonoBehaviour {


	public enum choices
	{
		Immune,
		Speed,
		Invisible,
		None = 0
	}
	public choices choice;
	// Use this for initialization
	void Start () {
		choice = choices.None;
	}
	
	// Update is called once per frame
	public void PowerType (int pType, GameObject powerUp) {
		
		choice = (choices)pType;
			switch(choice)
			{
			case choices.Immune: 
				//add whatever to powerup go, or have each state its own class.
				Debug.Log("Immune");
				break;
			case choices.Speed:
				Debug.Log("Speed");
				break;
			case choices.Invisible:
				Debug.Log("Invisible");
				break;
			default:
				Debug.Log("Nothing");
				break;
			}
	}
}
