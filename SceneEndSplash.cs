using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Goldraven.Prod;

namespace Goldraven.Dev
{

	/*
	 * cmcb 2017/11/12
	 * 
	 * 
 	 * 
 	 */


	public class SceneEndSplash : MonoBehaviour
	{
		private bool isVictory = false;

		public Text victoryText, defeatText, scoreText;
		public Scoreboard scoreboard;


		void Update ()
		{

			if (isVictory) {
				victoryText.gameObject.SetActive (true);
				defeatText.gameObject.SetActive (false);
			} else {
				defeatText.gameObject.SetActive (true);
				victoryText.gameObject.SetActive (false);
			}
			scoreText.text = scoreboard.Score;
		}

		public void setVictory (bool isWin)
		{
			isVictory = isWin;

		}

	}
}
