using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour
{
	public float direction=1f;
	public float spd=2f;
	void Start()
	{
		Invoke("morre",5f);
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D>().velocity=new Vector2(direction*spd,GetComponent<Rigidbody2D>().velocity.y);
		transform.localScale=new Vector3(direction,1,1);
	}
	void morre()
	{
		Destroy(gameObject);
	}
}
