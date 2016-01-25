using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	//make mesh disappear
	public GameObject[] objToTrigger;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter( Collider other){
		if (other.tag == "player") {
			foreach (GameObject Go in objToTrigger) {
				Go.SetActive (false);

			}
		}
	}
}
