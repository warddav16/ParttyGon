using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Teleport : MonoBehaviour {


	public List<GameObject> teleportPoints = new List<GameObject>();

	void Start () {
		
		//just store the removed teleporter point then add it back instead?
		//teleportPoints.AddRange(GameObject.FindGameObjectsWithTag ("teleporters"));
	}

	void TeleportObject(GameObject theTeleporter, GameObject theObjectThatCollidedWithTheTeleporter ){

		foreach (GameObject GO in teleportPoints) {
			if (GO == theTeleporter) {
				teleportPoints.Remove (GO);
			}
		}

		int telePick = Random.Range (0, teleportPoints.Count);
		Vector3 moveTo;
		moveTo = teleportPoints [telePick].transform.position;
		theObjectThatCollidedWithTheTeleporter.transform.position = moveTo;

		teleportPoints.Clear ();
		teleportPoints.AddRange(GameObject.FindGameObjectsWithTag ("teleporters"));
	}
}
