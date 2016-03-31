using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	public Transform target;
	public float jumpHeight=2f;
	public float smooth=3f;
	public float smoothX=4f;
	public float minX;
	public float maxX;

	void LateUpdate () {			
		float posX=Mathf.Clamp(target.position.x,minX,maxX);
		transform.position=new Vector3(Mathf.Lerp(transform.position.x,posX,Time.deltaTime*smoothX),
		                               Mathf.Lerp(transform.position.y,target.position.y,Time.deltaTime*smooth),
		                               transform.position.z);
	}
}
