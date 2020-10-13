using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {


	public Transform Background1;
	public Transform Background2;

	private bool whichone = true;
	private bool whichtwo = true;
	public Transform cam;
		
	private float currentheight = 1;
	private float currentwidth = 15;


	
	// Update is called once per frame
	void Update () {

		/*if (currentwidth < cam.position.x) 
		{
			if (whichone)
				Background1.localPosition = new Vector3 (Background1.localPosition.x + 30, Background1.localPosition.y,10);
			else
				Background2.localPosition = new Vector3 (Background2.localPosition.x + 30, Background2.localPosition.y ,10);

			currentwidth += 15;

			whichone = !whichone;
		}
		if (currentwidth > cam.position.x + 15) 
		{
			if (whichone)
				Background2.localPosition = new Vector3 (Background2.localPosition.x - 30, Background2.localPosition.y ,10);
			else
				Background1.localPosition = new Vector3 (Background1.localPosition.x - 30, Background1.localPosition.y ,10);

			currentwidth -= 15;

			whichone = !whichone;
		
		}*/

		if (currentheight < cam.position.y) 
		{
			if (whichtwo) {
				Background1.localPosition = new Vector3 (Background1.localPosition.x, Background1.localPosition.y + 15, 10);
				Background2.localPosition = new Vector3 (Background2.localPosition.x, Background2.localPosition.y + 15, 10);
			}
			currentheight += 15;

			whichtwo = !whichtwo;
		}
		if (currentheight > cam.position.y + 15) 
		{
			if (whichtwo)
			{
				Background2.localPosition = new Vector3 (Background2.localPosition.x, Background2.localPosition.y - 15 ,10);
				Background1.localPosition = new Vector3 (Background1.localPosition.x, Background1.localPosition.y - 15 ,10);

			currentheight -= 15;
			}
			whichtwo = !whichtwo;

		}


	}
}
