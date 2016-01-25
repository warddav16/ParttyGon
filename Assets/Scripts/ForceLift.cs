using UnityEngine;
using System.Collections;

public class ForceLift : MonoBehaviour {
	public float power;

	void OnTriggerEnter( Collider other){

		other.GetComponent<Rigidbody>().AddForce( transform.up * power, ForceMode.Impulse );
	}
}
