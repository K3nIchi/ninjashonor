using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {
	public Vector4 limite;
	public float speed=2f;
	public GameObject nave;
	public float smooth = 2.0f;
	public float tiltAngle = 30.0f;
	public float vel=5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horiz=Input.GetAxis("Horizontal")*speed;
		float vert=Input.GetAxis("Vertical")*speed;
		horiz*=Time.deltaTime;
		vert*=Time.deltaTime;
		float spd=vel*Time.deltaTime;
		transform.Translate(horiz,vert,spd);
		float h=Mathf.Clamp(transform.position.x,limite.x,limite.y);
		float v=Mathf.Clamp(transform.position.y,limite.z,limite.w);
		transform.position=new Vector3(h,v,transform.position.z);

		float tiltAroundZ = Input.GetAxis("Horizontal") * -tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * -tiltAngle;

		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
		nave.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
}
