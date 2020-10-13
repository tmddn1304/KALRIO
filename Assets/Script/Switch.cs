using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour {
	private GameMaster gm;
	public Sprite Sp_SwithchON;

	private AudioSource audioSource;
	public AudioClip OpenStage;


	public bool SwitchOn = false;
	GameObject[] hurdle;

	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		hurdle = GameObject.FindGameObjectsWithTag ("hurdle");
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && !SwitchOn) 
		{
			gm.inputText.text = ("[E] to Swithch ON");
			if (Input.GetKeyDown("e")) 
			{
				this.GetComponent<SpriteRenderer> ().sprite = Sp_SwithchON;
				foreach (GameObject i in hurdle)
					i.SetActive (false);
				//hurdle.SetActive (false);
				SwitchOn = true;
				PlayAudioClip(OpenStage);


			}

		}
		else if (col.CompareTag ("Player") && SwitchOn) 
		{
			gm.inputText.text = ("Next Stage is Open");
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && Input.GetKeyDown("e")) 
		{
			this.GetComponent<SpriteRenderer> ().sprite = Sp_SwithchON;
			foreach (GameObject i in hurdle)
				i.SetActive (false);
			SwitchOn = true;
			gm.inputText.text = ("Next Stage is Open");
			PlayAudioClip(OpenStage);

		}

	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.CompareTag("Player"))
		{
			gm.inputText.text = (" ");
		}

	}
	void PlayAudioClip(AudioClip clip)
	{if (audioSource != null && clip != null)	
		{
			//if (!audioSource.isPlaying) 
			audioSource.PlayOneShot(clip);	
		}
	}

		
}

