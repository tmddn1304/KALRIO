using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burster : MonoBehaviour {
	public int dmg = 10;
	void OnTriggerEnter2D(Collider2D col)
	{



		if (col.isTrigger != true && col.CompareTag ("Enemy")) 
			{ 
				col.SendMessageUpwards ("Damage", dmg);
				Destroy (gameObject);
			}
		if (col.gameObject.tag != "Player" && col.gameObject.tag != "Enemy" ) {
			Destroy (gameObject);
		}

	}
		
}
