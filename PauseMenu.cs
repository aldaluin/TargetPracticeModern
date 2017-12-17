using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;
using Goldraven.Prod;

namespace Goldraven.Prod
{

	// Edited cmcb 2017/05/27

	/*
	 * cmcb 2017/10/18
	 * 
	 * 
 	 *  Requires input button "Pause" to be defined
 	 * 
 	 * TODO: Don't assume the cursor locked and invisible in regular play
 	 * 
 	 */

	public class PauseMenu : MonoBehaviour
	{
		private bool menu = false;

		public Goldraven.Dev.PauseGame pg;
		public Canvas pauseMenu;
		public bool localInputModule = false;
		public bool doSceneEnd = false;
		public Canvas sceneEndSplash;

		void Start ()
		{
			// Debug.Log ("Pauser started, cursor should be off");
			if (pg == null) Debug.LogError("Pause menu has no link to game pause");

			menu_off ();
		}

		void Update ()
		{
			if (Input.GetButtonDown ("Pause")) {
				if (!pg.Paused) {
					menu_on ();

				} else {
					menu_off ();
				}


			}
		}

		public void DoContinue ()
		{
			menu_off ();
		}

		public void DoQuit ()
		{
			pg.doQuit ();
		}

		public void DoFinish () {
			sceneEndSplash.gameObject.SetActive(true);
			pauseMenu.gameObject.SetActive (false);
		}

		public void DoSettings ()
		{
			Debug.Log ("Settings button pushed");
		}

		void menu_on ()// if open menu
		{
			menu = true;
			pg.doPause ();
			pauseMenu.enabled = true;
			pauseMenu.gameObject.SetActive (true);

		}

		void menu_off ()// if menu close
		{
			menu = false;
			pg.doResume();
			pauseMenu.enabled = false;
			pauseMenu.gameObject.SetActive (false);


		}
	}
}
