using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float maxSpeed = 3;
	public float speed =50f;
	public float jumpPower = 300f;
	public float attackRate = 0.5f;
	float coolDown;

	public bool grounded;
	public bool wallsliding;
	public bool facingRight = true;
	public bool canDoubleJump;
	public bool isUnBeatTime = false;
	public Rigidbody2D arrow;

	//stats
	public int curHealth;
	public int maxHealth = 5;

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer renderer2;
	private GameMaster gm;
	private Player player;

	public Transform wallCheckPoint;
	public bool wallCheck;
	public LayerMask wallLayerMask;
	public AudioClip Coin;
	public AudioClip Jump;
	public AudioClip Gun;



	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
	
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		renderer2 = gameObject.GetComponent<SpriteRenderer> ();
		anim = gameObject.GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();

		curHealth = maxHealth;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();

	}


	// Update is called once per frame
	void Update () {
		anim.SetBool ("Grounded",grounded);
		anim.SetFloat ("Speed", Mathf.Abs(Input.GetAxis ("Horizontal")));

		//move player
		if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			transform.localScale = new Vector3 (-1, 1, 1);
			//renderer2.flipX = true;
			facingRight = false;
	
		}
		if (Input.GetAxis ("Horizontal") > 0.1f)
		{
			transform.localScale = new Vector3 (1, 1, 1);
			//renderer2.flipX = false;
			facingRight = true;
		}
		if (Input.GetButtonDown ("Jump") && !wallsliding) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower );
				canDoubleJump = true;
				PlayAudioClip (Jump);
			} else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower / 1.5f);				
					PlayAudioClip (Jump);
				}

			}
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		if (curHealth <= 0) {
			Die ();
		}

		if (!grounded) 
		{
			wallCheck = Physics2D.OverlapCircle (wallCheckPoint.position, 0.1f, wallLayerMask);

			if (facingRight && Input.GetAxis ("Horizontal") > 0.1f || !facingRight && Input.GetAxis ("Horizontal") < 0f) 
			{
				if (wallCheck) 
				{
					HandleWallSliding ();
				}
			}
				

		}

		if (wallCheck == false || grounded) 
		{
			wallsliding = false;
		
		}
		if (Time.time >= coolDown) {
			if (Input.GetKeyDown ("g")) {
				BulletAttack ();
			}
		}

	}

	void HandleWallSliding()
	{
		rb2d.velocity = new Vector2 (rb2d.velocity.x, -0.7f);
		canDoubleJump = true;
		wallsliding = true;

		if (Input.GetButtonDown ("Jump")) 
		{
			if (facingRight) 
			{
				rb2d.AddForce (new Vector2 (-3f, 1f) * jumpPower);
			
			} 
			else 
			{
				rb2d.AddForce (new Vector2 (3f, 1f) * jumpPower);
			
			}
			PlayAudioClip (Jump);
		}



	}

	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = Input.GetAxis ("Horizontal");

		rb2d.AddForce ((Vector2.right * speed) * h);

		//Fake friction / Easing thex speed of our player
		if (grounded) 
		{
			rb2d.velocity = easeVelocity;
		}


		if (grounded) 
		{
			rb2d.AddForce ((Vector2.right * speed) * h);

		} else 
		{
			rb2d.AddForce ((Vector2.right * speed / 2) * h);
		
		}

		//Move player limiting the speed of the player
		if (rb2d.velocity.x > maxSpeed) 
		{
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		
		}
		if (rb2d.velocity.x < -maxSpeed) 
		{
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}


	}
	void Die(){
		
		//restart
		if (PlayerPrefs.HasKey ("Highscore")) 
		{
			if (PlayerPrefs.GetInt ("Highsocre") < gm.score) 
			{
				PlayerPrefs.SetInt ("Highscore", gm.score);
			} 
			else
			{
				PlayerPrefs.SetInt ("Highscore", gm.score);
			}
		}
		Application.LoadLevel (Application.loadedLevel);
	
	}
	public void Damage(int dmg)
	{
		if (isUnBeatTime == false) {

			if (curHealth < dmg) {
				dmg = curHealth;
			}
			curHealth -= dmg;


			Vector2 attackedVelocity = Vector2.zero;
			if (facingRight) {
				attackedVelocity = new Vector2 (-17f, 5f);
			} else
				attackedVelocity = new Vector2 (17f, 5f);
			

			rb2d.AddForce (attackedVelocity, ForceMode2D.Impulse);

			

			isUnBeatTime = true;
			StartCoroutine ("UnBeatTime");
		
		}

	}

	public IEnumerator Knockback(float knockDur, float KnockbackPwr, Vector3 KnockbackDir)
	{
		float timer = 0;
		while (knockDur > timer) {
			timer += Time.deltaTime;

			rb2d.AddForce (new Vector3 (KnockbackDir.x * -1000,  KnockbackPwr, transform.position.z));

		}
		yield return 0;

	}
	public IEnumerator UnBeatTime()
	{
		int countTime = 0;

		while (countTime < 10) 
		{
			if (countTime%2 == 0)
				renderer2.color = new Color32 (255, 255, 255, 90);

				else
				renderer2.color = new Color32(255,255,255,180);

				yield return new WaitForSeconds(0.2f);

				countTime++;

			}
			renderer2.color = new Color32(255,255,255,255);

			isUnBeatTime = false;

			yield return null;
		}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Coin")) {
			Destroy (col.gameObject);
			gm.score += 1;

			PlayAudioClip(Coin);
		} 
		if (col.CompareTag ("Block")) {
			Vector2 upVelocity = new Vector2 (0, 10);
			canDoubleJump = false;
			rb2d.AddForce (upVelocity, ForceMode2D.Impulse);
		} 
	}
	void PlayAudioClip(AudioClip clip)
	{if (audioSource != null && clip != null)	
		{
			//if (!audioSource.isPlaying) 
				audioSource.PlayOneShot(clip);	
		}
	}
	void BulletAttack ()
	{
		if (facingRight) {
			Rigidbody2D arrowInstance = Instantiate (arrow, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
			arrowInstance.AddForce (Vector3.right * 300);
			coolDown = Time.time + attackRate;
			PlayAudioClip(Gun);
		}
		else{
			Rigidbody2D arrowInstance = Instantiate (arrow, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
		arrowInstance.AddForce (-Vector3.right * 300);
			coolDown = Time.time + attackRate;
			PlayAudioClip(Gun);
		}


	}
}
