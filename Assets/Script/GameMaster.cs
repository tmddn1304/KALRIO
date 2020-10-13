using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public int score;
	public int highscore = 0;

	public Text pointsText;
	public Text inputText;



	void Start()
	{
		
		if (PlayerPrefs.HasKey ("Score")) 
		{
			if (Application.loadedLevel == 0) {
				PlayerPrefs.DeleteKey ("Score");
				score = 0;
			} 
			else 
			{
				score = PlayerPrefs.GetInt ("Score");
			}
		}

		if (PlayerPrefs.HasKey ("Highscore")) 
		{
			highscore = PlayerPrefs.GetInt ("Highscore");
		}
	}
	void Update()
	{
		pointsText.text = ("Gems: " + score);

	}
}
