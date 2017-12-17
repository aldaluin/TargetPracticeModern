using UnityEngine;
using System.Collections;
using Goldraven.Prod;
using UnityEngine.UI;

namespace Goldraven.Dev
{

	public class VictoryConditions : MonoBehaviour
	{
		/*
		 * cmcb  2017/11/12
		 * 
		 * First hack, first victory condition built into base
		 * 
		 */

		public Goldraven.Dev.PauseGame pg;
		public Scoreboard scoreboard;
		public SceneEndSplash  recapScript;
		public Canvas recapCanvas;
		public int victoryType;

		// Use this for initialization
		void Start ()
		{
			if (pg == null) Debug.LogError("Pause menu has no link to game pause");

		}
	
		// Update is called once per frame
		void Update ()
		{
			switch (victoryType) {
			case 1:
				{
					if (scoreboard.ScoreCount >= 20) {
						doVictory ();
					}
					break;
				}
			case 2:
				{
					if (scoreboard.ScoreCount >= 20) {
						doVictory ();
					} else if (scoreboard.AmmoCount >= 40) {
						doDefeat ();
					}
					break;
				}
			}
		}

		void doVictory ()
		{
			scoreboard.Victory = true;
			recapScript.setVictory (true);
			recapCanvas.gameObject.SetActive (true);
		}

		void doDefeat() {
			scoreboard.Defeat = true;
			recapScript.setVictory (false);
			recapCanvas.gameObject.SetActive (true);
			}

		void onContinue() {
			pg.doQuit ();
		}
	}
}
