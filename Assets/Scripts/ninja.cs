using UnityEngine;
using System.Collections;

public class ninja : MonoBehaviour {
	Animator anim;
	public bool isAttack=false;
	public float maxSpeed=10f;
	public bool animAttack=false;
	public float jumpForce=2f;
	bool facingRight=true;
	bool grounded=false;
	bool canFlip=true;
	bool isDead=false;
	float groundRadius=0.2f;
	public LayerMask whatisGround;
	public bool invencible=false;
	gameController gc;
	// Use this for initialization
	void Start () {
		anim=gameObject.GetComponent<Animator>();
		gc=GameObject.FindWithTag("logics").GetComponent<gameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && isAttack==false && grounded && !isDead)
		{
			StartCoroutine(Attack());
		}else if(Input.GetButtonDown("Fire1") && isAttack==false && !grounded && !isDead)
		{
			StartCoroutine(JumpAttack());
		}
		if(grounded && Input.GetButtonDown("Jump") && isAttack==false && !isDead)
		{
			anim.SetBool("ground",false);
			anim.Play("ninja_jump");
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce));
		}
	}
	void FixedUpdate()
	{
		grounded= Physics2D.OverlapCircle(transform.position, groundRadius, whatisGround);
		anim.SetBool("ground",grounded);
		anim.SetFloat("vSpeed",GetComponent<Rigidbody2D>().velocity.y);

		float move=Input.GetAxis("Horizontal");
		anim.SetFloat("speed",Mathf.Abs(move));

		if(!animAttack && !isDead)
			GetComponent<Rigidbody2D>().velocity= new Vector2(move*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if(move>0 && !facingRight && canFlip && !isDead || move<0 && facingRight && canFlip && !isDead)
			Flip();

	}

	void Flip()
	{
		facingRight=!facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *=-1;
		transform.localScale=theScale;
	}

	IEnumerator Attack()
	{
		canFlip=false;
		isAttack=true;
		animAttack=true;
		//move um pouco pra frente
		GetComponent<Rigidbody2D>().velocity= (facingRight==true)? new Vector2(1,GetComponent<Rigidbody2D>().velocity.y): new Vector2(-1,GetComponent<Rigidbody2D>().velocity.y);
		anim.SetTrigger("atk");
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if(currentBaseState.IsName("ninja_atk1"))
		{
			yield return new WaitForSeconds(0.4f);
		}
		else
		{
			yield return new WaitForSeconds(0.2f);
		}
		canFlip=true;
		isAttack=false;
	}

	IEnumerator JumpAttack()
	{
		canFlip=false;
		isAttack=true;
		anim.Play("ninja_jump_atk",1);
		anim.SetLayerWeight(1,1f);
		yield return new WaitForSeconds(0.4f);
		anim.SetLayerWeight(1,0f);
		isAttack=false;
		canFlip=true;
	}

	void restoreAnimAttack()
	{
		animAttack=false;
	}

	public void NinjaDeath()
	{
		isDead=true;
		StartCoroutine(Death());

	}
	IEnumerator Death()
	{
		anim.Play("ninja_death",1);
		anim.SetLayerWeight(1,1f);
		yield return new WaitForSeconds(1.5f);
		Animator fd=GameObject.Find("fade").GetComponent<Animator>();
		fd.Play("fadeOUT");
		yield return new WaitForSeconds(0.8f);
		gc.LooseGame();
	}
	public void NinjaHurt()
	{
		anim.Play("ninja_hurt");
		StartCoroutine(Invencible());
	}

	IEnumerator Invencible()
	{
		animAttack=true; //utilizando animAttack pra ganahr tempo
		invencible=true;
		yield return new WaitForSeconds(0.2f);
		animAttack=false;
		int i=0;
		SpriteRenderer spr=gameObject.GetComponent<SpriteRenderer>();
		do{
			spr.enabled=!spr.enabled;
			yield return new WaitForSeconds(0.2f);
			i++;
		}while(i<10);
		spr.enabled=true;
		invencible=false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag=="sight")
		{
			soldier tp=col.transform.parent.gameObject.GetComponent<soldier>();
			tp.DetectNinja();
		}
		if(col.tag=="bullet" && !invencible)
		{
			gc.looseLife();
			Destroy(col.gameObject);
		}
	}
}
