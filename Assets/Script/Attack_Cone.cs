using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {

	public TurretAi turretAI;

	public bool isLeft = false;
	void Awake()
	{
		turretAI = gameObject.GetComponentInParent<TurretAi> ();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			if (isLeft)
			{
				
				turretAI.Attack (false);
			} 
			else 
			{
				turretAI.Attack (true);
			}
		}
	}
}
