using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public float vel=-1;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		transform.Translate(0,0,vel);
	}
}
