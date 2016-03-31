using UnityEngine;
using System.Collections;

public class soldier : MonoBehaviour {
	public enum soldierStates{idle, walk, pursue, attack, death};
	public soldierStates state;
	Animator anim;
	gameController gc;
	public bool morto=false;
	public float maxDistance=2f;
	public float shotDistance=1f;
	public float maxShotTime=1f;
	public float spd=0.5f;
	public GameObject bullet;
	public Transform shotPoint;
	bool attacking=false;
	bool facingRight=true;
	// Use this for initialization
	void Start () {
		anim=gameObject.GetComponent<Animator>();
		gc=GameObject.FindGameObjectWithTag("logics").GetComponent<gameController>();
		StartCoroutine(Idle());
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag=="sword" && !morto)
		{
			print ("colidiu com espada");
			StartCoroutine(morre());
		}
	}

	IEnumerator morre()
	{
		morto=true;
		anim.SetLayerWeight(1,1f);
		anim.Play("guard_death",1);
		yield return new WaitForSeconds(1.05f);
		gc.AddPoints();
		Destroy(gameObject);
	}

	public void DetectNinja()
	{
		if(!morto)Pursue();
	}

	void Pursue()
	{
		state=soldierStates.pursue;
		float dist=Vector2.Distance(transform.position,gc.character.transform.position);
		if(dist<shotDistance && !attacking)
		{
			StartCoroutine(Attack());
		}
		print ("distancia"+Mathf.Abs(dist));
		if(Mathf.Abs(dist)>0.8f)
		{
			StopPursue();
		}
	}

	IEnumerator Idle()
	{
		state=soldierStates.idle;
		int i =Random.Range(2,6);
		anim.Play("guard_idle");
		yield return new WaitForSeconds(i);
		if(!morto)StartCoroutine(Walk());
	}
	IEnumerator Attack()
	{
		state=soldierStates.attack;
		attacking=true;
		anim.Play("guard_atk");
		GameObject obj=(GameObject)Instantiate(bullet, shotPoint.position, Quaternion.identity);

		if(!facingRight)
		{
			bullet bul=obj.GetComponent<bullet>();
			bul.direction=-1f;
		}
		yield return new WaitForSeconds(maxShotTime);
		attacking=false;
		if(!morto)Pursue();
	}
	IEnumerator Walk()
	{
		state = soldierStates.walk;
		int i = Random.Range(3,6);
		int o = Random.Range(0,2);
		if(o==0)
		{
			facingRight=true;
			transform.localScale=Vector3.one;
		}else
		{
			facingRight=false;
			transform.localScale=new Vector3(-1,1,1);
		}
		anim.Play("guard_walk");
		yield return new WaitForSeconds(i);
		if(!morto)StartCoroutine(Idle());
	}
	void FixedUpdate()
	{

		if(state==soldierStates.walk && !morto && !morto){
			int direction=(facingRight==true)?1:-1;
			GetComponent<Rigidbody2D>().velocity=new Vector2(spd*direction,GetComponent<Rigidbody2D>().velocity.y);
		}
	}
	public void StopPursue()
	{
		int i=Random.Range(0,2);
		if(i==0)
		{
			if(!morto)StartCoroutine(Idle());
		}else
		{
			if(!morto)StartCoroutine(Walk());
		}
	}
}
