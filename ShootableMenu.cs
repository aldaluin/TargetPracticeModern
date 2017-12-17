using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;

namespace CharlieAssets.Prod
{

	// Edited cmcb 2017/05/27

	/*
 *  Requires input button "Pause" to be defined
 */

	public class Shootable : MonoBehaviour
	{
		private bool menu = false;

		public Canvas pauseMenu;
		public bool localInputModule = false;

		void Start ()
		{
			// Debug.Log ("Pauser started, cursor should be off");
			menu_off ();
		}

		void Update ()
		{
			if (Input.GetButtonDown ("Pause")) {
				if (!menu) {
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

			Application.Quit ();
			#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}

		public void DoSettings ()
		{
			Debug.Log ("Settings button pushed");
		}

		void menu_on ()// if open menu
		{
			menu = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true; 
			Time.timeScale = 0;// time stopped
			pauseMenu.enabled = true;
			pauseMenu.gameObject.SetActive (true);

		}

		void menu_off ()// if menu close
		{
			menu = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;// time is normal
			pauseMenu.enabled = false;
			pauseMenu.gameObject.SetActive (false);


		}
	}
}
