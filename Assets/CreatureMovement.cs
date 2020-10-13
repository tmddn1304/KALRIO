using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour {

	public float movePower =1f;
	public int curHealth;
	public int maxHealth;
	public bool Dead =false;
	public int EnemyType;

	private Player player;
	private GameMaster gm;
	private Rigidbody2D rb2d;

	Animator animator;
	Vector3 movement;
	int movementFlag = 0;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		animator = gameObject.GetComponentInChildren<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

	
	}

	void Start () {
		
		StartCoroutine ("ChangeMovement");
		curHealth = maxHealth;
	}
	void Update()
	{
		if (curHealth <= 0 && Dead == false) 
		{
			
				Die ();
				gm.score += 10;
			
		
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!Dead)
		Move ();

	}

	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;

		if (movementFlag == 1) {
			moveVelocity = Vector3.left;
			transform.localScale = new Vector3 (1, 1, 1);

		
		} 
		else if (movementFlag == 2) {
			moveVelocity = Vector3.right;
			transform.localScale = new Vector3 (-1, 1, 1);
		
		}
		transform.position += moveVelocity * movePower * Time.deltaTime;

	}

	IEnumerator ChangeMovement()
	{
		if (EnemyType == 0) {
			movementFlag = Random.Range (0, 3);

			if (movementFlag == 0)
				animator.SetBool ("isMoving", false);
			else
				animator.SetBool ("isMoving", true);

			yield return new WaitForSeconds (3f);

			StartCoroutine ("ChangeMovement");
		}
		if (EnemyType == 1) {
			if (movementFlag == 1) {

				yield return new WaitForSeconds (2f);

				StartCoroutine ("ChangeMovement");
				movementFlag = 2;
			} 
			else {
				yield return new WaitForSeconds (2f);

				StartCoroutine ("ChangeMovement");
				movementFlag = 1;
			}
		
		}
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && Dead == false) {

			player.Damage (1);

		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && Dead == false) {

			player.Damage (1);
		}
	}

	public void Damage(int damage)
	{

		curHealth -= damage;
		gameObject.GetComponent<Animation>().Play ("Player_Attacked");
	}


	public void Die()
	{
		animator.Play ("CatDie");
		Dead = true;



		gameObject.GetComponent<Collider2D>().enabled = false;
		movementFlag = 0;
		rb2d.isKinematic = true;
		Destroy (gameObject,1); 

		
	}

}
