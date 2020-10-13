using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;

	private float attackTimer = 0;
	private float attackCd = 0.3f;

	public Collider2D attackTrigger;
	public AudioClip attack;

	private AudioSource audioSource;
	private Animator anim;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if(Input.GetKeyDown("f") && !attacking)
			{
				attacking = true;
				attackTimer = attackCd;

				attackTrigger.enabled = true;
			PlayAudioClip(attack);

			}

			if(attacking)
			{
				if(attackTimer > 0)
				{
					attackTimer -= Time.deltaTime;
				}
				else
				{
					attacking = false;
					attackTrigger.enabled = false;
				}
			}
		anim.SetBool ("Attacking", attacking);



	}
	void PlayAudioClip(AudioClip clip)
	{if (audioSource != null && clip != null)	
		{
			//if (!audioSource.isPlaying) 
			audioSource.PlayOneShot(clip);	
		}
	}

}
