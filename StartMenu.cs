using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;

namespace Goldraven.Prod
{

	/*
	 * cmcb 2017/10/29
	 * 
	 * 
 	 * 
 	 */

	public class StartMenu : MonoBehaviour
	{
		private bool menu = false;

		public Canvas startMenu, champsMenu, timeMenu, targetMenu, difficultyMenu, arenaMenu, quitMenu;
		public Goldraven.Dev.PauseGame pg;

		void Awake ()
		{
			// menu_off ();
			if (pg == null) Debug.LogError("Start menu has no link to game pause");

		}

		void Update ()
		{
			if (Input.GetButtonDown ("Menu")) {
				if (!menu) {
					menu_on ();

				} else {
					menu_off ();
				}
			}
		}

		public void DoChamps ()
		{
			champsMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);
		}

		public void DoTime ()
		{
			timeMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);
		}

		public void DoTarget ()
		{
			targetMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);
		}

		public void DoDifficulty ()
		{
			difficultyMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);
		}

		public void DoArena ()
		{
			arenaMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);
		}

		public void DoQuit ()
		{
			quitMenu.gameObject.SetActive (true);
			startMenu.gameObject.SetActive (false);

			Application.Quit ();
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}

		public void DoContinue ()
		{
			menu_off ();
		}


		public void DoSettings ()
		{
			Debug.Log ("Settings button pushed");
		}

		void menu_on ()// if open menu
		{
			menu = true;
			//Debug.Log ("Start menu: menu_on");
			pg.doPause ();
			startMenu.enabled = true;
			startMenu.gameObject.SetActive (true);

		}

		void menu_off ()// if menu close
		{
			menu = false;
			//Debug.Log ("Start menu: menu_off");
			pg.doResume();
			startMenu.gameObject.SetActive (false);


		}
	}
}
